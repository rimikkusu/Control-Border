using System;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class PassportForm : Form
    {
        public PassportForm() { InitializeComponent(); }

        public PassportForm(Traveler traveler, InspectionController inspection) : this()
        {
            Passport passport = traveler.Passport;
            inspection.BindField(lblNameValue, FieldKind.Name, passport.FullName);
            inspection.BindField(lblNationalityValue, FieldKind.Nationality, passport.Nationality);
            inspection.BindField(lblBirthValue, FieldKind.BirthDate, passport.DateOfBirth.ToString("yyyy-MM-dd"));
            inspection.BindField(lblNumberValue, FieldKind.PassportNumber, passport.Number);
            inspection.BindField(lblSexValue, FieldKind.Sex, passport.Sex);
            inspection.BindField(lblExpiryValue, FieldKind.Expiry, passport.Expires.ToString("yyyy-MM-dd"));
            GameArt.Attach(picPortrait, @"Characters\TravelerPlaceholder.png", Placeholder.Traveler, traveler.PortraitStyle);
        }

        private void btnClose_Click(object sender, EventArgs e) { Close(); }
    }
}
