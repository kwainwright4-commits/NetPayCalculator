using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetPayCalculator
{
    // Manager class that keeps track of employees and persistence
    public class PayrollManager
    {
        public List<UserInfo> Employees { get; private set; } = new List<UserInfo>();
        public Dictionary<Guid, UserInfo> EmployeeMap { get; private set; } = new Dictionary<Guid, UserInfo>();

        public int Count => Employees.Count;

        public void AddEmployee(UserInfo user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            Employees.Add(user);
            EmployeeMap[user.Id] = user;
        }

        public bool RemoveEmployee(Guid id)
        {
            if (EmployeeMap.TryGetValue(id, out var user))
            {
                EmployeeMap.Remove(id);
                return Employees.Remove(user);
            }
            return false;
        }

        public UserInfo? GetEmployee(Guid id)
        {
            EmployeeMap.TryGetValue(id, out var user);
            return user;
        }

        // Async save to JSON file
        public async Task SaveAsync(string filePath)
        {
            using var fs = File.Create(filePath);
            var options = new JsonSerializerOptions { WriteIndented = true };
            await JsonSerializer.SerializeAsync(fs, Employees, options);
            await fs.FlushAsync();
        }

        // Async load from JSON file (replaces current collection)
        public async Task LoadAsync(string filePath)
        {
            using var fs = File.OpenRead(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var loaded = await JsonSerializer.DeserializeAsync<List<UserInfo>>(fs, options);
            Employees = loaded ?? new List<UserInfo>();
            EmployeeMap = new Dictionary<Guid, UserInfo>();
            foreach (var e in Employees)
                EmployeeMap[e.Id] = e;
        }

        public IEnumerable<string> GetPayslips(IDeductionCalculator calculator)
        {
            foreach (var user in Employees)
            {
                var totalDeductions = calculator.CalculateTotalDeductions(user.GrossPay);
                var netPay = calculator.CalculateNetPay(user.GrossPay);

                yield return "--- PAYSLIP ---";
                yield return $"Employee Name: {user.Name}";
                yield return $"Hours Worked:  {user.HoursWorked}";
                yield return $"Pay Rate:      {user.PayRate:C}";
                yield return $"Gross Pay:     {user.GrossPay:C}";
                yield return $"Federal Tax (10%): {(user.GrossPay * ((TaxRates)calculator).FederalTaxRate):C}";
                yield return $"State Tax:     {(user.GrossPay * ((TaxRates)calculator).StateTaxRate):C}";
                yield return $"Benefit Deductions ({((TaxRates)calculator).BenefitDeductionsPercent}%): {(user.GrossPay * (((TaxRates)calculator).BenefitDeductionsPercent / 100m)):C}";
                yield return $"Total Deductions: {totalDeductions:C}";
                yield return $"Net Pay:       {netPay:C}";
                yield return "----------------";
                yield return string.Empty;
            }
        }
    }
}
