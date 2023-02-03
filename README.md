# HabrProxy
Задание 2:
Решение представлено в виде .NET 7 Мinimal Api, основная библиотека для парсинга сайта - HtmlAgilityPack
Решение подменяет все ссылки исходного сайта на указанный (в данный момент это localhost) и добовляет во все (почти) слова длинной 6 символов окончание ™



Задание 1:
-- Create the Trains table
CREATE TABLE Trains (
    train_id INT IDENTITY(1,1) PRIMARY KEY,
    train_type VARCHAR(255) NOT NULL,
    route_id INT NOT NULL,
    FOREIGN KEY (route_id) REFERENCES Routes(route_id)
);

-- Create the Routes table
CREATE TABLE Routes (
    route_id INT IDENTITY(1,1) PRIMARY KEY,
    start_station_id INT NOT NULL,
    end_station_id INT NOT NULL,
    duration INT NOT NULL,
    FOREIGN KEY (start_station_id) REFERENCES Stations(station_id),
    FOREIGN KEY (end_station_id) REFERENCES Stations(station_id)
);

-- Create the Stations table
CREATE TABLE Stations (
    station_id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    address VARCHAR(255) NOT NULL,
    contact_info VARCHAR(255) NOT NULL
);

-- Create the Schedules table
CREATE TABLE Schedules (
    schedule_id INT PRIMARY KEY,
    departure_time DATETIME NOT NULL,
arrival_time DATETIME NOT NULL,
train_id INT NOT NULL,
FOREIGN KEY (train_id) REFERENCES Trains(train_id)
);

-- Create the Tickets table
CREATE TABLE Tickets (
ticket_id INT PRIMARY KEY,
schedule_id INT NOT NULL,
customer_id INT NOT NULL,
class_id INT NOT NULL,
discount_id INT NOT NULL,
price DECIMAL(10, 2) NOT NULL,
FOREIGN KEY (schedule_id) REFERENCES Schedules(schedule_id),
FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
FOREIGN KEY (class_id) REFERENCES Classes(class_id),
FOREIGN KEY (discount_id) REFERENCES Discounts(discount_id)
);

-- Create the Customers table
CREATE TABLE Customers (
customer_id INT PRIMARY KEY,
first_name VARCHAR(255) NOT NULL,
last_name VARCHAR(255) NOT NULL,
contact_info VARCHAR(255) NOT NULL,
passport_data VARCHAR(255) NOT NULL
);

-- Create the Classes table
CREATE TABLE Classes (
class_id INT PRIMARY KEY,
name VARCHAR(255) NOT NULL,
amenities VARCHAR(255) NOT NULL,
price DECIMAL(10, 2) NOT NULL
);

-- Create the Discounts table
CREATE TABLE Discounts (
discount_id INT PRIMARY KEY,
name VARCHAR(255) NOT NULL,
amount DECIMAL(10, 2) NOT NULL,
conditions VARCHAR(255) NOT NULL
);
