using System;
using System.Text;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class ShiftReportForm : Form
    {
        public ShiftReportForm() { InitializeComponent(); }

        public ShiftReportForm(DailyReport report) : this()
        {
            var text = new StringBuilder();
            text.AppendLine("DAY " + report.Day + " / SHIFT ACCOUNT");
            text.AppendLine("------------------------------------");
            text.AppendLine("Travelers processed    " + report.Processed);
            text.AppendLine("Correct decisions      " + report.Correct);
            text.AppendLine("Incorrect decisions    " + report.Incorrect);
            text.AppendLine();
            text.AppendLine("Money earned          +" + report.Earnings + " cr");
            text.AppendLine("Penalties             -" + report.Penalties + " cr");
            text.AppendLine("Rent                  -20 cr");
            text.AppendLine("Food                  -10 cr");
            text.AppendLine("Heating                -5 cr");
            text.AppendLine("Daily expenses        -" + report.Expenses + " cr");
            text.AppendLine("------------------------------------");
            text.AppendLine("Remaining balance      " + report.Balance + " cr");
            text.AppendLine("\n" + report.Story);
            txtReport.Text = text.ToString().Replace("\r\n", "\n").Replace("\n", "\r\n");
            btnContinue.Text = report.Day == 10 ? "FINISH CAMPAIGN" : "CONTINUE TO DAY " + (report.Day + 1);
        }

        private void btnContinue_Click(object sender, EventArgs e) { DialogResult = DialogResult.OK; Close(); }
    }
}
