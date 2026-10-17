using System;

namespace NetPayCalculator
{
    // Abstract base class representing an employee
    public abstract class Employee
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; } = string.Empty;
        public decimal HoursWorked { get; protected set; }
        public decimal PayRate { get; protected set; }

        // Computed gross pay, implemented by concrete classes
        public decimal GrossPay => CalculateGrossPay();

        protected Employee()
        {
            Id = Guid.NewGuid();
        }

        protected Employee(string name, decimal hoursWorked, decimal payRate) : this()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            HoursWorked = hoursWorked;
            PayRate = payRate;
        }

        // Concrete classes must implement how gross pay is calculated
        public abstract decimal CalculateGrossPay();

        public override string ToString()
        {
            return $"{Name} ({Id}): {GrossPay:C}";
        }
    }
}
