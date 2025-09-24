# 📖 BlogPost - Full Stack Web Application

A complete **Full Stack Blog Application** built with **Angular** (Frontend) and **ASP.NET Core Web API** (Backend).  
This project demonstrates how to build a modern, scalable web application from scratch using **C#, Entity Framework Core, JWT Authentication, SQL Server, and Angular**.

---

## 🚀 Features

### 🌐 Frontend (Angular)
- Component-based architecture.
- Angular Routing & Navigation.
- Angular Services for API communication.
- Role-based Authorization using **Auth Guards**.
- HTTP Interceptors for attaching JWT tokens.
- File upload support (e.g., uploading images).
- RxJs (Observables, Subjects, Subscriptions) for reactive programming.

### ⚙️ Backend (ASP.NET Core Web API)
- RESTful APIs with CRUD operations using `GET`, `POST`, `PUT`, `DELETE`.
- Authentication & Authorization with **JWT Tokens**.
- Role-based Authorization for secure access.
- Dependency Injection for cleaner and scalable code.
- Entity Framework Core for data persistence.
- SQL Server database integration.
- Swagger & Postman support for API testing.

---

## 🛠️ Technologies Used
- **Frontend:** Angular, RxJs, TypeScript, HTML, SCSS  
- **Backend:** ASP.NET Core Web API, C#  
- **Database:** SQL Server with Entity Framework Core  
- **Authentication:** JWT Tokens  
- **Tools:** Swagger, Postman, Visual Studio, VS Code  

---

## 📂 Project Structure

BlogPost-Fullstack/
│
├── backend/ # ASP.NET Core Web API
│ ├── Controllers/
│ ├── Models/
│ ├── Services/
│ ├── Data/
│ └── Program.cs
│
├── frontend/ # Angular Application
│ ├── src/
│ │ ├── app/
│ │ ├── assets/
│ │ └── environments/
│ └── angular.json
│
└── README.md


---

## ▶️ Getting Started

### 1️⃣ Clone the repository
```bash
git clone https://github.com/username/BlogPost-Fullstack.git
cd BlogPost-Fullstack
```
###2️⃣ Setup Backend (ASP.NET Core API)
```bash
cd backend
dotnet restore
dotnet run
```
###3️⃣ Setup Frontend (Angular)
```bash
cd frontend
npm install
ng serve -o
```
---
## 🔐 Authentication & Authorization

Users can register and log in with JWT-based authentication.

Different roles (e.g., Admin, User) supported.

Angular Auth Guards protect routes.

HTTP Interceptors attach JWT tokens automatically to requests.
---
## 📸 Image Upload

Users can upload an image file from Angular UI.

The image is sent to the ASP.NET Core Web API and stored securely.
---
##✅ Best Practices

Clean architecture with Dependency Injection.

Separation of concerns (Controllers, Services, Repositories).

Reusable Angular components and services.

Following SOLID principles for scalable backend design.
