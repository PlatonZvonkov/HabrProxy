using Proxy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/{**path}", async context =>
                {
                    var (contentType, content) = await HabrProxy.GetContentWithModificationsAsync(context.Request.Path);
                    context.Response.ContentType = contentType;
                    await context.Response.WriteAsync(content);
                });
            });
app.Run();