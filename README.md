README for the WPF Machine Status Tracker 🚀
📋 Project Overview
Welcome to the Machine Status Tracker! This is a user-friendly WPF application designed for managing the operational statuses of machines in a manufacturing factory. With this tool, you can easily track machine details, add or edit statuses, and filter machines based on their current state.

🎯 Purpose and Goals
This project is aimed at:

Simplifying the process of tracking machine statuses in a factory.
Allowing easy addition, updating, and deletion of machine information.
Providing a clean and intuitive interface for managing operational statuses such as Running, Idle, and Offline.
Delivering quick feedback and ensuring smooth error handling for all operations.
🛠️ Technologies Used
WPF (Windows Presentation Foundation) 🖼️ for creating the desktop application and its user interface.
SQL Server 🗄️ for managing and storing machine status data.
📦 Features
View Machines: Display machines in visually appealing card-style blocks with icons representing their statuses.
Add/Edit/Delete: Manage machine details easily.
Filter Machines: Filter the machine list by their operational status.
Validation: Ensure required fields like machine names are filled and statuses are valid.
Feedback: Notify users of successful actions like adding or updating machine statuses.
Error Handling: Handle issues gracefully and inform the user about problems.
🚀 Getting Started
Prerequisites
Install Visual Studio with the required .NET desktop development workload.
Install SQL Server or ensure access to an existing SQL Server instance.
Steps to Run
Clone the Repository:

bash
Copy
Edit
git clone <repository-url>
Restore NuGet Packages:

Open the solution in Visual Studio.
Restore NuGet packages by rebuilding the project.
Database Setup:

Create a SQL Server database using the provided schema.
Update the connection string in the application configuration file (App.config or appsettings.json).
Run the Application:

Press F5 in Visual Studio to build and run the application.
Start managing your machines! 🎉
📖 Additional Notes
The application provides a friendly UI with clear feedback for every action.
Filtering machines helps users focus on specific operational statuses.
All changes are saved in the SQL Server database for consistency.
💡 Future Improvements
Add support for more machine attributes like location or maintenance schedules.
Implement role-based access control for secure usage.
Enhance the filtering options for advanced search.
