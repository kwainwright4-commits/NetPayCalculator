using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetPayCalculator
{
    partial class MainForm
    {
        private Label lblStateRate;
        private Panel pnlStateRate;
        private NumericUpDown numStateRate;
        private Label lblBenefitPercent;
        private Panel pnlBenefitPercent;
        private NumericUpDown numBenefitPercent;

        private GroupBox grpEmployee;
        private Label lblName;
        private Panel pnlName;
        private TextBox txtName;
        private Label lblHours;
        private Panel pnlHours;
        private NumericUpDown numHours;
        private Label lblPayRate;
        private Panel pnlPayRate;
        private NumericUpDown numPayRate;
        private Button btnAddEmployee;

        private ListBox lstEmployees;
        private Button btnShowPayslips;
        private Button btnSave;
        private Button btnLoad;
        private TextBox txtOutput;

        private void InitializeComponent()
        {
            this.Text = "Net Pay Calculator";
            this.Size = new Size(700, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblStateRate = new Label { Text = "State Tax Rate (ex.0.05):", Location = new Point(12, 12), AutoSize = true };
            pnlStateRate = new Panel { Location = new Point(170, 8), Size = new Size(80, 23), Padding = new Padding(6), BackColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
            numStateRate = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 3, Increment = 0.005M, Minimum = 0, Maximum = 1, Value = 0M, BorderStyle = BorderStyle.None, TextAlign = HorizontalAlignment.Left };
            pnlStateRate.Controls.Add(numStateRate);

            lblBenefitPercent = new Label { Text = "Benefit % (ex. 3):", Location = new Point(270, 12), AutoSize = true };
            pnlBenefitPercent = new Panel { Location = new Point(380, 8), Size = new Size(60, 23), Padding = new Padding(6), BackColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
            numBenefitPercent = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 0, Increment = 1, Minimum = 0, Maximum = 100, Value = 0M, BorderStyle = BorderStyle.None, TextAlign = HorizontalAlignment.Left };
            pnlBenefitPercent.Controls.Add(numBenefitPercent);

            grpEmployee = new GroupBox { Text = "Add Employee", Location = new Point(12, 40), Size = new Size(420, 140) };

            lblName = new Label { Text = "Name:", Location = new Point(10, 24), AutoSize = true };
            pnlName = new Panel { Location = new Point(80, 20), Size = new Size(320, 23), Padding = new Padding(6), BackColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
            txtName = new TextBox { Dock = DockStyle.Fill, BorderStyle = BorderStyle.None };
            pnlName.Controls.Add(txtName);

            // Place the Hours label under the hours input control
            lblHours = new Label { Text = "Hours Worked:", Location = new Point(80, 76), AutoSize = true };
            pnlHours = new Panel { Location = new Point(80, 50), Size = new Size(120, 23), Padding = new Padding(6), BackColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
            numHours = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Increment = 0.25M, Minimum = 0, Maximum = 1000, BorderStyle = BorderStyle.None, TextAlign = HorizontalAlignment.Left };
            pnlHours.Controls.Add(numHours);

            // Place the Pay Rate label under the pay rate input control
            lblPayRate = new Label { Text = "Pay Rate:", Location = new Point(280, 76), AutoSize = true };
            pnlPayRate = new Panel { Location = new Point(280, 50), Size = new Size(120, 23), Padding = new Padding(6), BackColor = SystemColors.Window, BorderStyle = BorderStyle.FixedSingle };
            numPayRate = new NumericUpDown { Dock = DockStyle.Fill, DecimalPlaces = 2, Increment = 0.25M, Minimum = 0, Maximum = 10000, BorderStyle = BorderStyle.None, TextAlign = HorizontalAlignment.Left };
            pnlPayRate.Controls.Add(numPayRate);

            btnAddEmployee = new Button { Text = "Add", Location = new Point(320, 90), Width = 80 };
            btnAddEmployee.Click += BtnAddEmployee_Click;

            grpEmployee.Controls.Add(lblName);
            grpEmployee.Controls.Add(pnlName);
            grpEmployee.Controls.Add(lblHours);
            grpEmployee.Controls.Add(pnlHours);
            grpEmployee.Controls.Add(lblPayRate);
            grpEmployee.Controls.Add(pnlPayRate);
            grpEmployee.Controls.Add(btnAddEmployee);

            lstEmployees = new ListBox { Location = new Point(12, 190), Size = new Size(420, 220), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left };

            btnShowPayslips = new Button { Text = "Show Payslips", Location = new Point(12, 420), Width = 120 };
            btnShowPayslips.Click += BtnShowPayslips_Click;

            btnSave = new Button { Text = "Save...", Location = new Point(150, 420), Width = 80 };
            btnSave.Click += async (s, e) => await Save_ClickAsync();

            btnLoad = new Button { Text = "Load...", Location = new Point(240, 420), Width = 80 };
            btnLoad.Click += async (s, e) => await Load_ClickAsync();

            txtOutput = new TextBox { Location = new Point(460, 12), Size = new Size(420, 460), Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right };

            this.Controls.Add(lblStateRate);
            this.Controls.Add(pnlStateRate);
            this.Controls.Add(lblBenefitPercent);
            this.Controls.Add(pnlBenefitPercent);
            this.Controls.Add(grpEmployee);
            this.Controls.Add(lstEmployees);
            this.Controls.Add(btnShowPayslips);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnLoad);
            this.Controls.Add(txtOutput);
        }
    }
}
