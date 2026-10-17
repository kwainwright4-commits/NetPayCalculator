using System;

namespace NetPayCalculator
{
    // Concrete employee implementation for hourly employees
    public class UserInfo : Employee
    {
        public UserInfo(string name, decimal hoursWorked, decimal payRate) : base(name, hoursWorked, payRate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (hoursWorked < 0)
                throw new ArgumentException("Hours worked cannot be negative.");
            if (payRate < 0)
                throw new ArgumentException("Pay rate cannot be negative.");
        }

        // Calculate gross pay with overtime at 1.5x
        public override decimal CalculateGrossPay()
        {
            const decimal OVERTIME_RATE = 1.5m;
            const decimal REGULAR_HOURS = 40m;

            decimal regularHours = Math.Min(HoursWorked, REGULAR_HOURS);
            decimal overtimeHours = Math.Max(0, HoursWorked - REGULAR_HOURS);

            decimal regularPay = regularHours * PayRate;
            decimal overtimePay = overtimeHours * PayRate * OVERTIME_RATE;

            return regularPay + overtimePay;
        }
    }
}
