# 🏨 Hotel ERP Management System

A complete Hotel ERP (Enterprise Resource Planning) system built using ASP.NET Core MVC. This application helps hotels manage room bookings, reservations, customers, rooms, roles, users, and hotel operations from a centralized dashboard.

## 🚀 Features

### 👤 User Management
- User Registration & Login
- Role-Based Authentication & Authorization
- Admin and Staff Access Control
- Session Management

### 🏨 Room Management
- Add New Rooms
- Update Room Details
- Delete Rooms
- Room Availability Tracking
- Room Category Management

### 📅 Booking Management
- Search Available Rooms
- Create New Reservations
- Booking Confirmation
- Customer Details Management
- Check-In & Check-Out Tracking

### 👥 Customer Management
- Customer Registration
- Identity Proof Upload
- Customer Booking History
- Customer Information Management

### 📊 Dashboard
- Total Rooms Overview
- Total Bookings Overview
- Available Rooms Statistics
- Occupancy Summary
- Quick Access Navigation

### 🔒 Security Features
- Authentication & Authorization
- Form Validation
- Session-Based Security
- File Upload Validation

---

## 🛠️ Technologies Used

### Backend
- ASP.NET Core MVC
- C#
- Entity Framework Core
- LINQ

### Frontend
- HTML5
- CSS3
- Bootstrap
- JavaScript
- jQuery

### Database
- SQL Server

### Development Tools
- Visual Studio 2022
- Git
- GitHub

---

## 📂 Project Structure

```text
Hotel_ERP/
│
├── Controllers/
├── Models/
├── Views/
├── ViewModels/
├── Data/
├── Services/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── uploads/
├── appsettings.json
└── Program.cs
```

---

## ⚙️ Installation

### 1. Clone Repository

```bash
git clone https://github.com/amiitmaurya/Hotel_ERP.git
```

### 2. Open Project

Open the solution in Visual Studio 2022.

### 3. Configure Database

Update the connection string in:

```json
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=HotelERPDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply Migrations

```powershell
Update-Database
```

### 5. Run Application

Press:

```text
Ctrl + F5
```

or

```text
F5
```

---

## 📸 Screenshots

### Dashboard
<img width="1918" height="866" alt="image" src="https://github.com/user-attachments/assets/c82631ec-4ab0-4715-8819-f5ae82c5ae5d" />


### Room Management
<img width="1918" height="867" alt="image" src="https://github.com/user-attachments/assets/d57ccbef-3a95-4124-b813-8c70ff0b45d3" />


### Booking Module

<img width="1918" height="866" alt="image" src="https://github.com/user-attachments/assets/bc4a0af7-7e3d-4f19-b334-eebf4e525780" />

### Customer Registration
<img width="1918" height="867" alt="image" src="https://github.com/user-attachments/assets/956814ec-2712-4b33-bb14-c9e49e1cac56" />


---

## 🎯 Future Enhancements

- Online Payment Integration
- Email Notifications
- SMS Notifications
- Reports & Analytics
- Multi-Hotel Support
- Invoice Generation
- QR Based Check-In

---

## 🤝 Contributing

Contributions are welcome.

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to GitHub
5. Create a Pull Request

---

## 📄 License

This project is developed for educational and learning purposes.

---

## 👨‍💻 Developer

**Amit Maurya**

GitHub:
https://github.com/amiitmaurya

---

⭐ If you like this project, don't forget to star the repository.
