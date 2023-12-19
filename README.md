**StaffRecord**

# Features
* Search Filters: Enable users to quickly search for employees based on various criteria such as company, department, name, etc.
* Employee Card: Detailed view of each employee, allowing users to edit employee information. 
* Dynamic Filters: Select filters to obtain information about employees, e.g., selecting a department and generating a report showing the total salary for that department.
* Export to TXT: Ability to export filtered data to a text file for external use or record-keeping.
* Tabular Display: View filtered data in a table within the application.

# Prerequisites
1. .NET 8
2. Microsoft SQL Server
3. Visual Studio

# Installation
1. Clone the repository:
*git clone https://github.com/RomanNoname/StaffRecords.git*
2. Open the project in Visual Studio (or any other compatible IDE).
3. Change the connection string to the MS SQL database in appsettings.Development.json or in the user secretes.
4. Build the project to restore dependencies and compile the application.

# Run the Application:
1. Configure Database Connection:
* Open appsettings.json and update the connection string with your SQL Server details.
2. Click on the solution file (.sln) to open the solution in Visual Studio.
3. Set the startup projects:
* Right-click on the solution in the Solution Explorer.
* Select "Set StartUp Projects."
* Choose "Multiple startup projects."
* Set the "Action" for StaffRecord.WEB and StaffRecord.Host to "Start."
* Click "OK" to save the changes.
9. Press F5 or click "Start" to run the application.

# Usage
1. Menu Navigation
Navigate through different sections of the application using the menu:
* Home: Access the home page with general information about the company
* Employee Overview: View and filter employee details
* Payroll Reporting: Generate reports based on various filters
2. Employee Overview
Filter by Salary:
* Use the salary range filters to display employees with salaries between specified values.
* Filter by Company:
* Search employees by company using the company filter
* Filter by Department
* Search by Last Name
* Enter a last name to search for a specific employee
3. Edit Employee:
Click on the "Edit" button next to an employee to modify their information.
4. Payroll Reporting
* View Total Salary
* Click the "Generate Report" button to see the total salary for the selected criteria
* Download Report
After generating a report, use the "Download" button to save the filtered data in a text file.



# License
**StaffRecord** is released under the MIT License. Feel free to modify and distribute the code as per the terms of the license.


# Disclaimer
StaffRecord is a sample application created for educational purposes and should not be considered as financial advice. 
