using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetPayCalculator
{
    public partial class MainForm : Form
    {
        private PayrollManager payroll = new PayrollManager();

        public MainForm()
        {
            InitializeComponent();
            UpdateUiState();
        }

        private void BtnAddEmployee_Click(object? sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Enter a name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal hours = numHours.Value;
                decimal payRate = numPayRate.Value;

                var user = new UserInfo(name, hours, payRate);
                payroll.AddEmployee(user);
                lstEmployees.Items.Add($"{user.Name} - {user.GrossPay:C}");

                txtName.Clear();
                numHours.Value = 0;
                numPayRate.Value = 0;

                UpdateUiState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnShowPayslips_Click(object? sender, EventArgs e)
        {
            txtOutput.Clear();

            decimal stateRate = numStateRate.Value;
            int benefitPercent = (int)numBenefitPercent.Value;

            TaxRates taxes;
            try
            {
                taxes = new TaxRates(stateRate, benefitPercent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var line in payroll.GetPayslips(taxes))
            {
                txtOutput.AppendText(line + Environment.NewLine);
            }
        }

        private async System.Threading.Tasks.Task Save_ClickAsync()
        {
            using var dlg = new SaveFileDialog { Filter = "JSON files|*.json", DefaultExt = "json" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                await payroll.SaveAsync(dlg.FileName);
                MessageBox.Show("Saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task Load_ClickAsync()
        {
            using var dlg = new OpenFileDialog { Filter = "JSON files|*.json", DefaultExt = "json" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                await payroll.LoadAsync(dlg.FileName);
                lstEmployees.Items.Clear();
                foreach (var e in payroll.Employees)
                    lstEmployees.Items.Add($"{e.Name} - {e.GrossPay:C}");
                UpdateUiState();
                MessageBox.Show("Loaded successfully.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateUiState()
        {
            bool hasEmployees = payroll.Count > 0;
            btnShowPayslips.Enabled = hasEmployees;
        }
    }
}
