using System;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class EntryPermitForm : Form
    {
        public EntryPermitForm() { InitializeComponent(); }

        public EntryPermitForm(EntryPermit permit, InspectionController inspection) : this()
        {
            inspection.BindField(lblNameValue, FieldKind.Name, permit.FullName);
            inspection.BindField(lblNumberValue, FieldKind.PassportNumber, permit.PassportNumber);
            inspection.BindField(lblPurposeValue, FieldKind.Purpose, permit.Purpose);
            inspection.BindField(lblStayValue, FieldKind.Stay, permit.StayDays + " days");
            inspection.BindField(lblExpiryValue, FieldKind.Expiry, permit.Expires.ToString("yyyy-MM-dd"));
        }

        private void btnClose_Click(object sender, EventArgs e) { Close(); }
    }
}
