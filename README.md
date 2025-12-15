# Modular Full-Stack Web Application (ASP.NET Core)

This project is a multi-feature **full-stack web application** built with **ASP.NET Core MVC**, created to explore and practice real-world development concepts.  
It includes several modular pages, each demonstrating different technologies such as API integrations, SQL databases, authentication, email services, and real-time communication.

---

## 🚀 Features

### 🔹 **API Integrations**
Modules that fetch and display data from external REST APIs:
- Weather API (OpenWeather)
- Chuck Norris jokes API
- Cocktail recipes API
- Spacecraft information API  
Each module handles JSON responses, asynchronous HTTP requests, and error handling.

### 🔹 **Database Modules (SQL Server + EF Core)**
- Real Estate listing system (CRUD)
- Spaceship inventory management  
Uses **Entity Framework Core**, migrations, and LINQ queries.

### 🔹 **Authentication & User Management**
- User registration and login  
- Email confirmation flow  
- Secure password hashing  
- Account management pages

### 🔹 **Email Service (SMTP)**
- Sends confirmation and notification emails  
- Configurable SMTP settings (host, port, SSL, credentials)

### 🔹 **Real-Time Communication (SignalR)**
- Live chat module demonstrating WebSocket-based messaging  
- Updates messages instantly without page reloads

### 🔹 **Modular Navigation**
Each feature is separated into its own section but unified under one MVC solution.

---

## 🛠️ Tech Stack

| Category | Technologies |
|----------|--------------|
| **Backend** | C#, ASP.NET Core MVC, Entity Framework Core |
| **Database** | SQL Server, SSMS, Migrations, LINQ |
| **Frontend** | Razor Views, HTML/CSS, Bootstrap |
| **Real-Time** | SignalR |
| **APIs** | RESTful integrations, JSON |
| **Email** | SMTP + MailKit / System.Net.Mail |
| **Tools** | Visual Studio, Git, SSMS |
