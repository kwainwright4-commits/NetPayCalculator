using System;

namespace NetPayCalculator
{
    // Interface representing a deductions calculator
    public interface IDeductionCalculator
    {
        decimal CalculateTotalDeductions(decimal grossPay);
        decimal CalculateNetPay(decimal grossPay);
    }

    // Class to store tax rates and calculate deductions
    public class TaxRates : IDeductionCalculator
    {
        public decimal FederalTaxRate { get; private set; } = 0.10m; // Fixed at 10%
        public decimal StateTaxRate { get; private set; }
        public int BenefitDeductionsPercent { get; private set; } // integer percentage

        // Allow optional override for federal rate for testing/demo
        public TaxRates(decimal state, int benefitPercent, decimal? federalOverride = null)
        {
            if (state < 0)
                throw new ArgumentException("State tax rate cannot be negative.");
            if (benefitPercent < 0)
                throw new ArgumentException("Benefit deductions cannot be negative.");

            if (federalOverride.HasValue)
                FederalTaxRate = federalOverride.Value;

            if (FederalTaxRate + state + (benefitPercent / 100m) > 1)
                throw new ArgumentException("Total deduction rate cannot exceed 100%.");

            StateTaxRate = state;
            BenefitDeductionsPercent = benefitPercent;
        }

        public decimal CalculateTotalDeductions(decimal grossPay)
        {
            return grossPay * (FederalTaxRate + StateTaxRate + (BenefitDeductionsPercent / 100m));
        }

        public decimal CalculateNetPay(decimal grossPay)
        {
            return grossPay - CalculateTotalDeductions(grossPay);
        }
    }
}

