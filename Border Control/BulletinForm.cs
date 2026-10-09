using System;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class BulletinForm : Form
    {
        public BulletinForm() { InitializeComponent(); }

        public BulletinForm(DailyRules rules) : this()
        {
            Text = "Daily Bulletin / Day " + rules.Day;
            // Native Windows TextBoxes need CRLF even for the authored blank lines.
            txtRules.Text = rules.BulletinText().Replace("\r\n", "\n").Replace("\n", "\r\n");
            txtRules.SelectionStart = 0;
        }

        private void btnClose_Click(object sender, EventArgs e) { Close(); }
    }
}
