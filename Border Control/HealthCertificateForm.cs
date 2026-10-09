using System;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class HealthCertificateForm : Form
    {
        public HealthCertificateForm() { InitializeComponent(); }

        public HealthCertificateForm(HealthCertificate certificate, InspectionController inspection) : this()
        {
            inspection.BindField(lblNameValue, FieldKind.Name, certificate.FullName);
            inspection.BindField(lblNumberValue, FieldKind.CertificateNumber, certificate.Number);
            inspection.BindField(lblExamValue, FieldKind.ExamDate, certificate.Examined.ToString("yyyy-MM-dd"));
            inspection.BindField(lblExpiryValue, FieldKind.Expiry, certificate.ValidUntil.ToString("yyyy-MM-dd"));
            inspection.BindField(lblClearanceValue, FieldKind.Clearance, certificate.Cleared ? "CLEARED" : "NOT CLEARED");
        }

        private void btnClose_Click(object sender, EventArgs e) { Close(); }
    }
}
