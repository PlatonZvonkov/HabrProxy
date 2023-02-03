using HtmlAgilityPack;

namespace Proxy;

/// <summary>
/// Proxy for habr.com that changes content of the site and replaces path
/// </summary>
public static class HabrProxy
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private static readonly HtmlDocument htmlDocument = new HtmlDocument();
    /// <summary>
    /// Returns content of given site modifiyed by adding postfix to words with certain length
    /// </summary>
    /// <param name="adress"></param>
    /// <returns>Tuple representation of given context</returns>
    public static async Task<(string contentType, string content)> GetContentWithModificationsAsync(string adress)
    {        
        string url = $"{Constants.ORIGINAL_ADRESS}/{adress.TrimStart('/')}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        string contentType = response.Content.Headers.ContentType.ToString();
        string content = await response.Content.ReadAsStringAsync();

        if (!contentType.Contains("text/html"))
        {
            return (contentType, content);
        }

        htmlDocument.LoadHtml(content);
        IEnumerable<string> sixLetterWords = ExtractSixLettersWords(htmlDocument);

        foreach (var word in sixLetterWords){
            string modifiedWord = AddTrademarkSymbol(word);
            content = content.Replace(word, modifiedWord);
        }

        return (contentType, content);
    }

    /*
     * Finding all symbol to 6 letters words, but not toching any scripts and images
     */
    private static IEnumerable<string> ExtractSixLettersWords(HtmlDocument htmlDocument)
    {
        return htmlDocument.DocumentNode.DescendantsAndSelf()
                    .Where(n => n.NodeType == HtmlNodeType.Text && n.ParentNode.Name != "script" && n.ParentNode.Name != "img")
                    .SelectMany(n => n.InnerHtml.Split(' '))
                    .Where(w => w.Length == 6);
    }

    /*
     * adding symbol to 6 letters words and also preventing repetitions
     */
    private static string AddTrademarkSymbol(string word)
    {
        if (word.Length == 6 && !word.Contains('™')){
            return word + "™";
        }
        return word;
    }
}
