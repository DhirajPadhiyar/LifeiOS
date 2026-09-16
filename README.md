# 🚀 LifeiOS

### Your Personal Life Management System

**LifeiOS** is a modern personal productivity and life-management web application built with **ASP.NET Core MVC**. It brings tasks, notes, goals, habits, finances, calendar events, search, and reports together in one organized dashboard.

The goal of LifeiOS is simple:

> **Organize your life. Track your progress. Build better habits.**

---
## ✨ Screenshots
<img width="2560" height="4516" alt="localhost-Dashboard-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/d72c3652-28c9-4ecb-b050-4ef4b0c4c3ba" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="1648" alt="localhost-Profile-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/0e8630a4-9129-479e-a2cc-a7835514688e" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="2576" alt="localhost-Search-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/07d3d5dc-1c8c-45bc-8ffa-3c4a6f06fc35" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="3104" alt="localhost-Tasks-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/b4595768-65fe-487c-ba5a-2e660820461e" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="2994" alt="localhost-Notes-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/48cef027-c590-405e-9800-62ee62f8b0c1" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="2632" alt="localhost-Habits-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/9bf0733b-58de-4b14-bc8c-1e55697c7f12" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="2558" alt="localhost-Goals-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/0d458bdd-9f9c-4f6a-8094-00780a3ae1f8" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="2824" alt="localhost-Expenses-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/e3c85a46-efac-43d2-8bfe-783582646667" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="2992" alt="localhost-Calendar-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/044247fc-6585-4b01-b8cb-e10b59db19a3" /> --------------------------------------------------------------------------------------------------------------------------------------------------------- <img width="2560" height="4106" alt="localhost-Reports-Index(Nest Hub Max)" src="https://github.com/user-attachments/assets/aceebff1-a8a6-4c04-83d0-28f991a76f87" />

---

## ✨ Features

### 🔐 Authentication & User Management

* User Registration
* User Login
* Logout
* User Profile
* ASP.NET Core Identity
* Role-based Identity infrastructure
* Secure authentication

### ✅ Task Management

* Create tasks
* Edit tasks
* Delete tasks
* View task details
* Mark tasks as completed
* Pending and completed task tracking
* Due dates
* Priority-based filtering
* Search tasks
* Due Today filter
* Pagination

### 📝 Notes

* Create personal notes
* Edit notes
* Delete notes
* Note categories
* Created and updated dates
* Personal notebook-style interface

### 🎯 Goals

* Create long-term goals
* Track target values
* Track current progress
* Calculate goal progress
* Monitor goal completion

### 🔥 Habit Tracking

* Create daily or weekly habits
* Complete habits
* Current streak tracking
* Last completed date
* Pending habits shown first
* AJAX-based habit completion without page reload

### 💰 Finance Management

* Track expenses
* Expense categories
* Expense amounts
* Expense dates
* Monthly expense tracking
* Financial reports

### 📅 Calendar

* Create events
* Edit events
* Delete events
* Event descriptions
* Start and end dates
* All-day events
* Event colors
* Month view
* List view
* Upcoming events

### 🔎 Global Search

Search across:

* Tasks
* Notes
* Goals
* Habits
* Expenses
* Calendar Events

One search system for your entire LifeOS.

### 📊 Reports & Analytics

LifeOS provides visual reports for:

* Completed Tasks
* Pending Tasks
* Total Notes
* Goal Progress
* Habit Completion
* Monthly Expenses
* Upcoming Events

Charts are powered by **Chart.js**.

### 🎨 Modern UI

* Responsive design
* Custom design system
* Sidebar navigation
* Top navbar
* Dashboard cards
* Custom buttons
* Custom form controls
* Empty states
* Loading spinner
* Responsive tables
* Mobile-friendly layout

### 🛡️ Professional Features

* Data validation
* Logging
* Exception handling
* Custom exception middleware
* HTTP status-code handling
* Pagination
* Clean MVC structure

---

# 🛠️ Technology Stack

| Technology            | Usage                     |
| --------------------- | ------------------------- |
| ASP.NET Core MVC      | Web application framework |
| .NET 8                | Application runtime       |
| C#                    | Programming language      |
| Entity Framework Core | ORM                       |
| SQL Server            | Database                  |
| ASP.NET Core Identity | Authentication            |
| Razor Views           | Frontend rendering        |
| Bootstrap             | Responsive UI             |
| Custom CSS            | UI design system          |
| Chart.js              | Reports & charts          |
| Visual Studio         | Development               |
| Git & GitHub          | Version control           |
| Render                | Deployment                |

---

# 🏗️ Project Architecture

```text
LifeiOS
│
├── Controllers
│   ├── AccountController
│   ├── HomeController
│   ├── TaskController
│   ├── NotesController
│   ├── GoalsController
│   ├── HabitsController
│   ├── ExpenseController
│   ├── CalendarController
│   ├── SearchController
│   └── ReportsController
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Middleware
│   └── ExceptionHandlingMiddleware
│
├── Models
│   ├── ApplicationUser
│   ├── TaskItem
│   ├── Note
│   ├── Goal
│   ├── Habit
│   ├── Expense
│   └── CalendarEvent
│
├── ViewModels
│   ├── CalendarViewModel
│   ├── ReportViewModel
│   └── ProfileViewModel
│
├── Views
│   ├── Account
│   ├── Home
│   ├── Tasks
│   ├── Notes
│   ├── Goals
│   ├── Habits
│   ├── Expenses
│   ├── Calendar
│   ├── Search
│   └── Reports
│
├── wwwroot
│   ├── css
│   ├── js
│   └── images
│
├── Program.cs
├── appsettings.json
└── Properties
    └── launchSettings.json
```

---

# 🗄️ Database

LifeiOS uses **Entity Framework Core with SQL Server**.

The main application database contains data related to:

```text
Users
Tasks
Notes
Goals
Habits
Expenses
Calendar Events
```

Entity Framework Core migrations are used to create and update the database schema.

### Create Migration

```bash
dotnet ef migrations add InitialCreate
```

### Update Database

```bash
dotnet ef database update
```

---

# ⚙️ Getting Started

## 1. Clone the Repository

```bash
git clone https://github.com/YOUR-USERNAME/LifeiOS.git
```

## 2. Open the Project

Open:

```text
LifeiOS.sln
```

using Visual Studio.

## 3. Configure Database

Update the connection string in:

```text
appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LifeiOSDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## 4. Apply Migrations

Run:

```bash
dotnet ef database update
```

## 5. Run the Application

From Visual Studio:

```text
Ctrl + F5
```

or:

```text
F5
```

Then open the local URL shown by ASP.NET Core.

---

# 🔑 Authentication

LifeiOS uses:

```csharp
AddIdentity<ApplicationUser, IdentityRole>()
```

with Entity Framework Core.

Authentication includes:

* Registration
* Login
* Logout
* Profile
* Authorization

Protected pages use:

```csharp
[Authorize]
```

---

# 🎨 Design System

LifeiOS uses reusable custom CSS components rather than relying entirely on Bootstrap defaults.

Examples:

```css
.form-control-custom
.btn-custom
.btn-primary-custom
.btn-secondary-custom
.btn-outline-custom
.card-custom
.dashboard-card
.dashboard-card-icon
.dashboard-stats
.action-menu-btn
.action-dropdown
.task-completed
```

Core theme variables include:

```css
--primary: #2563EB;
--sidebar: #1E293B;
--navbar: #FFFFFF;
```

The interface is designed to remain clean, simple, responsive, and consistent throughout the application.

---

# 📊 Dashboard

The LifeOS dashboard provides a central overview of your life and productivity.

It includes information such as:

```text
Tasks
Notes
Goals
Habits
Expenses
Upcoming Events
Reports
```

The dashboard is designed to give users a quick understanding of their current progress without navigating through multiple pages.

---

# 🔎 Global Search

LifeOS includes a unified search system.

Users can search across multiple modules from one place:

```text
Tasks
Notes
Goals
Habits
Expenses
Calendar
```

This makes finding information inside the application faster and easier.

---

# 📈 Reports

The reporting system provides visual insights using **Chart.js**.

Example metrics:

```text
Completed Tasks
Pending Tasks
Goal Progress
Habit Completion %
Monthly Expenses
Upcoming Events
```

This helps users understand their productivity and financial activity over time.

---

# 🚀 Deployment

LifeOS is designed to support deployment to cloud hosting platforms such as **Render**.

The application detects the hosting environment's `PORT` variable and binds the application appropriately.

For local development, ASP.NET Core uses the configured `launchSettings.json` profile.

For production deployment, configure:

```text
Connection String
Environment Variables
Database
PORT
ASPNETCORE_ENVIRONMENT
```

> **Important:** LocalDB is intended for local development and should not be used as the production database on a cloud host.

---

# 🔒 Security Notes

Before deploying LifeOS publicly:

* Use a production database
* Never commit passwords or secrets
* Store sensitive configuration in environment variables
* Use HTTPS in production
* Configure secure authentication settings
* Review Identity password policies
* Keep NuGet packages updated

---

# 📱 Responsive Design

LifeOS is designed to work across:

```text
💻 Desktop
💻 Laptop
📱 Mobile
📱 Tablet
```

The navigation, dashboard, forms, tables, and cards adapt to different screen sizes.

---

# 🧪 Current Development Status

### LifeOS Version 1.0

```text
✅ Authentication
✅ Dashboard
✅ Tasks
✅ Notes
✅ Goals
✅ Habits
✅ Expenses
✅ Calendar
✅ Global Search
✅ Reports
✅ Pagination
✅ Logging
✅ Exception Handling
✅ Validation
✅ Responsive UI
✅ Loading States
✅ Empty States
✅ UI Polish
✅ Testing
✅ Refactoring
✅ GitHub Documentation
🚀 Deployment Ready
```

---

# 🗺️ Future Roadmap

Potential future improvements:

* 🔔 Notifications
* 📧 Email notifications
* 📱 PWA / mobile experience
* 📌 Advanced task priorities
* 💳 Income management
* 📊 Advanced financial analytics
* 📅 Recurring events
* 🔥 Advanced habit statistics
* 🎯 Goal milestones
* 🌙 Dark mode
* ⚙️ User preferences
* 📤 Data export
* 💾 Backup and restore
* 🔐 Enhanced security
* 🤖 Optional AI-powered productivity features

---

# 💡 Why LifeiOS?

Modern life is spread across many different applications.

One app for tasks.

Another for notes.

Another for habits.

Another for finances.

Another for calendars.

**LifeiOS brings these essential areas together into one personal system.**

The idea is not to make life more complicated.

It is to make life easier to understand.

> **Plan your day. Track your progress. Improve your life.**

---

# 👨‍💻 Developer

**Dhiraj Padhiyar**

Built with:

```text
C#
ASP.NET Core MVC
Entity Framework Core
SQL Server
Razor
Bootstrap
JavaScript
Chart.js
```

---

# ⭐ Support

If you find LifeiOS useful, consider giving the repository a ⭐ on GitHub.

Every star helps support the project and motivates further development.

---

## 📄 License

This project is currently intended as a personal/portfolio project.

Add your preferred license here if you decide to make the project open source.

---

### LifeiOS

**A simple system for managing the important parts of life.**
