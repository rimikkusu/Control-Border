using System;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class WorkPermitForm : Form
    {
        public WorkPermitForm() { InitializeComponent(); }

        public WorkPermitForm(WorkPermit permit, InspectionController inspection) : this()
        {
            inspection.BindField(lblNameValue, FieldKind.Name, permit.FullName);
            inspection.BindField(lblOccupationValue, FieldKind.Occupation, permit.Occupation);
            inspection.BindField(lblEmployerValue, FieldKind.Employer, permit.Employer);
            inspection.BindField(lblPurposeValue, FieldKind.Purpose, permit.Purpose);
            inspection.BindField(lblExpiryValue, FieldKind.Expiry, permit.Expires.ToString("yyyy-MM-dd"));
        }

        private void btnClose_Click(object sender, EventArgs e) { Close(); }
    }
}
