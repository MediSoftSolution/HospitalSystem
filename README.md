# Hospital System

This project is a part of a hospital platform web application that includes ASP.NET Core MVC and ASP.NET Core API. The platform provides a comprehensive system for managing hospital operations, user interactions, doctor panels, and administrative functionalities. 

🚧 **The project is currently under development, and new features and improvements are being added continuously.** 🚧

## Table of Contents

- [Project Overview](#project-overview)
- [Key Functionalities](#key-functionalities)
- [User Workflow](#user-workflow)
- [Role-Based Pages](#role-based-pages)
- [Technologies Used](#technologies-used)
- [Development Status](#development-status)
- [Setup and Installation](#setup-and-installation)
  
## Project Overview

The Hospital System is a comprehensive web application designed to manage hospital operations. The platform is built using ASP.NET Core MVC and ASP.NET Core API, aiming to provide a seamless user experience for patients, doctors, and administrators.

The project consists of three main components:

- **Client Web Page**: The front-facing website for patients to manage appointments and view lab results.
- **Doctor Panel**: An interface tailored for doctors to view patient information and manage appointments and lab results.
- **Admin Panel**: A backend interface for administrators to oversee hospital operations and manage roles.

**Note:** The project is currently under development, with ongoing additions of features and improvements.

## Key Functionalities

- **User Registration**: Allows users to register with OTP verification and includes functionalities for password reset and forgot password.
- **Appointment Booking**: Users can book appointments with doctors directly through the platform.
- **View Lab Results**: Patients can access their lab results online and receive results via email.
- **Special Offers on Birthdays**: Registered users receive discounts and gifts on their birthdays.
- **Scenario-Based Medical Processes**: All medical operations, including examinations, lab tests, and treatment planning, are integrated into the system.

## User Workflow

1. The user visits the hospital or accesses the website.
2. If unregistered, the user creates an account; otherwise, they can directly book an appointment with a doctor.
3. The doctor reviews the user’s information on the system and calls the user for an examination.
4. After the examination, necessary lab tests are booked via the system.
5. Lab technicians view the user’s information, conduct the tests, and record the results in the system.
6. Users can view their results online or receive them via email.

## Role-Based Pages

- **AppUser**: Access to home, doctors, departments, laboratory (analysis info and results), and branches.
- **Doctor**: Access to patients and laboratory pages.
- **Admin**: Access to the admin panel.

## Technologies Used

- **ASP.NET Core MVC and API**: Framework for creating the application’s web and API layers.
- **SQL Server**: Database solution for storing all relevant data.
- **FluentValidation**: Validation framework used for ensuring data integrity and user input validation.
- **Unit of Work Pattern**: Employed to manage database transactions effectively and maintain data consistency.

## Development Status

The Hospital Platform API is currently in the development phase. Features are being actively implemented, and continuous improvements are underway. The project may contain incomplete functionalities or bugs, with future updates planned for enhancements and fixes.

## Setup and Installation

### Prerequisites

- .NET SDK
- SQL Server
- An IDE that supports .NET Core (e.g., Visual Studio)

### Steps to Run the Application

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Abulfazfa/HospitalPlatformAPI.git
