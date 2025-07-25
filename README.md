# 📚 LMS API (.NET 8)

A complete **Learning Management System (LMS) RESTful API** built using **ASP.NET Core 8**, designed to handle students, instructors, courses, lessons, enrollments, and secure user authentication.  
This project was built with clean architecture principles and focuses on real-world backend development practices.

---

## 🚀 Features

- 🔐 **JWT Authentication** with role-based access (`Admin`, `Instructor`, `Student`)
- 👥 **User Management** with password hashing and secure login
- 🎓 **Course & Lesson System**:
  - Instructors can create, update, publish courses
  - Students can enroll in courses and complete lessons
- 📄 **DTOs** for clean request/response handling
- 🔄 **Service Layer** architecture for maintainability
- ✅ **Custom Response Wrapper** with `ServiceResult<T>`
- 🛡 **Authorization Guards** on sensitive endpoints
- 🧪 Tested with Swagger & Postman

---

## 🧱 Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server (via EF Core Migrations)
- JWT Tokens for Auth
- Password Hashing with Salt
- LINQ, Auto Mapping (via custom methods)
- Swagger (Swashbuckle)
- Postman for API Testing

---

## 📚 Modules Overview

### Users
- Register, login, logout, change email/password
- Admin can manage all users

### Courses
- Created by instructors, enrolled by students
- Lessons nested within courses
- Course publishing logic

### Lessons
- View, complete, or track progress
- Each lesson linked to a course

### Enrollments
- M:N relation between users and courses
- Completion tracking via flags

---

## 🧪 API Testing

Use:
- 🔐 Swagger UI: add Bearer token after login to access protected endpoints
- 🧪 Postman Collection (coming soon!)

---

## 🧠 What I Learned

- How to design a scalable API from scratch
- Role-based authorization & identity handling
- Managing relationships and edge cases with EF Core
- Handling logic in service layers
- Token generation and claims-based security
- Building habits like refactoring, validation, and testing early

---

## 📂 Project Structure (Simplified)

LmsApi/
├── Controllers/
├── Services/
│ ├── Interfaces/
│ └── Implementation/
├── Models/
│ ├── Entities/
│ └── DTOs/
├── Helpers/
├── Mappings/
├── Data/
├── Program.cs
└── appsettings.json
