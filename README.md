NetPayCalculator - WinForms Net 10 sample
========================================

Overview
--------
This is a Windows Forms native C#/.NET application targeting .NET 10 (net10.0-windows). It provides a simple payroll calculator UI and demonstrates object-oriented design, inheritance, interfaces, collections, error handling, input validation and async file I/O.

Design
------
- Employee (abstract): base class for employees (Id, Name, HoursWorked, PayRate, GrossPay, CalculateGrossPay())
- UserInfo (concrete): hourly employee implementation of Employee
- TaxRates: implements IDeductionCalculator (FederalTaxRate, StateTaxRate, BenefitDeductionsPercent)
- PayrollManager: manages a collection of UserInfo objects, provides async SaveAsync/LoadAsync to JSON
- MainForm: Windows Forms UI that lets users add employees, show payslips, and save/load the employee list

Modern .NET feature
-------------------
Async file I/O using System.Text.Json with async/await for SaveAsync and LoadAsync in PayrollManager.

Testing
-------
Unit tests are provided in the NetPayCalculator.Tests project (MSTest). They validate gross pay calculation, tax calculations, and constructor validations.

Running
-------
Open the solution in Visual Studio and set NetPayCalculator as the startup project. Run the solution (F5) to launch the WinForms UI. Use the Save/Load buttons to persist employee lists to JSON.
