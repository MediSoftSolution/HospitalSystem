# Hospital Platform API

🚧 **This project is still under development, and new features are continuously being added.** 🚧

## 📖 Table of Contents
- [📘 About the Project](#about-the-project)
- [✨ Features](#features)
- [🔄 User Flow](#user-flow)
- [🛠 Technologies Used](#technologies-used)
- [🚧 Development Status](#development-status)
- [⚙️ Installation and Running](#installation-and-running)
- [🧩 CQRS and MediatR Usage](#cqrs-and-mediatr-usage)
  
---

## 📌 About the Project

The **Hospital Platform API** is a comprehensive web application designed to manage hospital operations. Built using **ASP.NET Core MVC** and **ASP.NET Core API**, it aims to provide a seamless experience for patients, doctors, and administrators.

### 🚀 Project Components
- **Patient Panel**: For booking appointments, viewing lab results, and accessing special offers.
- **Doctor Panel**: Allows doctors to view patient information and manage appointments.
- **Admin Panel**: Provides hospital administrators with user and operation management tools.

## 🔥 Features
- **User Registration**: OTP verification for registration, password reset, and forgot password handling.
- **Appointment System**: Enables users to book appointments with doctors directly.
- **Laboratory Results**: Patients can view their test results online or via email.
- **Birthday Special Offers**: Discounts and gifts for users on their birthdays.
- **Scenario-Based Medical Processes**: Integrated medical procedures like examination, lab tests, and treatment planning.
- **Global Exception Handling**: API errors are managed and logged.
- **Serilog Logging**: Detailed event and error logging.
- **Admin Access Token Management**: Admin users can disable specific user access tokens.

## 📌 User Flow
1. User visits the hospital website.
2. If not registered, they create an account; if registered, they log in.
3. User books an appointment with a doctor.
4. Doctor reviews the patient's information and initiates an examination.
5. Necessary tests are created in the system.
6. Laboratory technicians conduct tests and upload results.
7. User can view test results online or receive them via email.

## 🛠 Technologies Used
- **ASP.NET Core API** – For building the web and API components.
- **SQL Server** – For database management.
- **FluentValidation** – For input data validation.
- **Unit of Work Pattern** – Ensures data integrity during database operations.
- **Serilog** – For logging system events.
- **Docker & Redis** – For caching and performance improvements.

## 🧩 **CQRS and MediatR Usage**

This project follows **CQRS** (Command Query Responsibility Segregation) to separate read and write operations, providing a more maintainable and scalable structure.

- **Commands**: Handle write operations (e.g., creating or updating data).
- **Queries**: Handle read operations (e.g., retrieving data).

### **MediatR Integration**
MediatR is used to decouple the request (Command/Query) from its handler, making the system more modular and easier to maintain.

## 🚧 Development Status
The project is under active development, with new features being added continuously. Some features may be incomplete or under construction.

## ⚙️ Installation and Running
### 📌 Requirements
- **.NET SDK**
- **SQL Server**
- **IDE** (e.g., Visual Studio or Rider)

### 📌 Steps
1. **Clone the repository:**
```sh
git clone https://github.com/Abulfazfa/HospitalPlatformAPI.git
