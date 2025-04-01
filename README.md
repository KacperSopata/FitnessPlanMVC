# FitnessPlanMVC

FitnessPlanMVC is a full-stack ASP.NET Core MVC application designed to support users in achieving their fitness goals by planning workouts, monitoring progress, managing diet, and participating in challenges.

---

## 🚀 Key Features

- **Meal Journal**: Assign meals and products, edit quantities, track calories
- **Workout Plans**: Create, manage, and view training plans
- **Challenges**: Create and join challenges to track goal completion
- **User Panel**:
  - Registration / Login
  - Google Authentication
  - Role-based authorization
- **Admin Panel**: Manage meals, plans, and users
- **Clean, layered architecture** with clearly separated concerns
- **Unit Testing** with xUnit and Moq

---

## 🧱 Technologies Used

- **.NET 8.0**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **MS SQL Server**
- **ASP.NET Identity**
- **Google Authentication**
- **FluentValidation**
- **AutoMapper**
- **xUnit / Moq**
- **Razor Views**

---

## 🗂️ Project Structure

```
FitnessPlanMVC
├── FitnessPlanMVC.Domain           # Domain models and interfaces
├── FitnessPlanMVC.Application      # Business logic and services
├── FitnessPlanMVC.Infrastructure  # EF Core, repositories, DB context
├── FitnessPlanMVC                  # MVC Web App (UI layer)
├── FitnessPlanMVC.Test             # Unit tests with xUnit & Moq
├── wwwroot                         # Static files (CSS, JS, images)
└── FitnessPlanMVC.sln              # Solution file
```

---

## 🛠️ How to Run the Project

1. **Clone the repository**
```bash
git clone https://github.com/KacperSopata/FitnessPlanMVC.git
```

2. **Open `FitnessPlanMVC.sln` in Visual Studio**

3. **Set `FitnessPlanMVC` project as the startup project**

4. **Update database connection string** in `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "your-sql-connection-string"
}
```

5. **Apply migrations (optional)**
```bash
dotnet ef database update
```

6. **Run the project** using Visual Studio or CLI

---

## 👤 Author

**Kacper Sopata**  
Junior .NET Developer  
🔗 [GitHub](https://github.com/KacperSopata) • [LinkedIn](https://www.linkedin.com/in/kacper-sopata-61b505310/)

---

## 📄 License

This project is created for portfolio and educational purposes. Feel free to fork or build upon it.
