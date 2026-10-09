namespace Border_Control
{
    partial class PassportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblOffice = new System.Windows.Forms.Label();
            this.picPortrait = new System.Windows.Forms.PictureBox();
            this.lblPortrait = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblNameValue = new System.Windows.Forms.Label();
            this.lblNationality = new System.Windows.Forms.Label();
            this.lblNationalityValue = new System.Windows.Forms.Label();
            this.lblBirth = new System.Windows.Forms.Label();
            this.lblBirthValue = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblNumberValue = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblSexValue = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblExpiryValue = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPortrait)).BeginInit();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Location = new System.Drawing.Point(18, 12);
            this.lblTitle.Size = new System.Drawing.Size(414, 28);
            this.lblTitle.Text = "PASSPORT";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            // lblOffice
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.TabIndex = 1;
            this.lblOffice.Location = new System.Drawing.Point(18, 43);
            this.lblOffice.Size = new System.Drawing.Size(414, 18);
            this.lblOffice.Text = "NATIONAL TRAVEL DOCUMENT";
            this.lblOffice.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblOffice.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // picPortrait
            this.picPortrait.Name = "picPortrait";
            this.picPortrait.TabIndex = 2;
            this.picPortrait.Location = new System.Drawing.Point(18, 78);
            this.picPortrait.Size = new System.Drawing.Size(114, 174);
            this.picPortrait.Text = "";
            this.picPortrait.BackColor = System.Drawing.Color.FromArgb(156, 168, 142);
            this.picPortrait.TabStop = false;
            this.picPortrait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // lblPortrait
            this.lblPortrait.Name = "lblPortrait";
            this.lblPortrait.TabIndex = 3;
            this.lblPortrait.Location = new System.Drawing.Point(18, 260);
            this.lblPortrait.Size = new System.Drawing.Size(114, 20);
            this.lblPortrait.Text = "PHOTOGRAPH";
            this.lblPortrait.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblPortrait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPortrait.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblName
            this.lblName.Name = "lblName";
            this.lblName.TabIndex = 4;
            this.lblName.Location = new System.Drawing.Point(150, 67);
            this.lblName.Size = new System.Drawing.Size(282, 16);
            this.lblName.Text = "FULL NAME";
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblName.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblNameValue
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.TabIndex = 5;
            this.lblNameValue.Location = new System.Drawing.Point(150, 84);
            this.lblNameValue.Size = new System.Drawing.Size(282, 23);
            this.lblNameValue.Text = "[ FULL NAME ]";
            this.lblNameValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblNameValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameValue.Padding = new System.Windows.Forms.Padding(4,0,0,0);
            this.lblNameValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNameValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblNationality
            this.lblNationality.Name = "lblNationality";
            this.lblNationality.TabIndex = 6;
            this.lblNationality.Location = new System.Drawing.Point(150, 110);
            this.lblNationality.Size = new System.Drawing.Size(282, 16);
            this.lblNationality.Text = "COUNTRY / NATIONALITY";
            this.lblNationality.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblNationality.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblNationalityValue
            this.lblNationalityValue.Name = "lblNationalityValue";
            this.lblNationalityValue.TabIndex = 7;
            this.lblNationalityValue.Location = new System.Drawing.Point(150, 127);
            this.lblNationalityValue.Size = new System.Drawing.Size(282, 23);
            this.lblNationalityValue.Text = "[ COUNTRY / NATIONALITY ]";
            this.lblNationalityValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblNationalityValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblNationalityValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNationalityValue.Padding = new System.Windows.Forms.Padding(4,0,0,0);
            this.lblNationalityValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNationalityValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblBirth
            this.lblBirth.Name = "lblBirth";
            this.lblBirth.TabIndex = 8;
            this.lblBirth.Location = new System.Drawing.Point(150, 153);
            this.lblBirth.Size = new System.Drawing.Size(282, 16);
            this.lblBirth.Text = "DATE OF BIRTH";
            this.lblBirth.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblBirth.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblBirthValue
            this.lblBirthValue.Name = "lblBirthValue";
            this.lblBirthValue.TabIndex = 9;
            this.lblBirthValue.Location = new System.Drawing.Point(150, 170);
            this.lblBirthValue.Size = new System.Drawing.Size(282, 23);
            this.lblBirthValue.Text = "[ DATE OF BIRTH ]";
            this.lblBirthValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblBirthValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblBirthValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBirthValue.Padding = new System.Windows.Forms.Padding(4,0,0,0);
            this.lblBirthValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBirthValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblNumber
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.TabIndex = 10;
            this.lblNumber.Location = new System.Drawing.Point(150, 196);
            this.lblNumber.Size = new System.Drawing.Size(282, 16);
            this.lblNumber.Text = "PASSPORT NUMBER";
            this.lblNumber.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblNumber.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblNumberValue
            this.lblNumberValue.Name = "lblNumberValue";
            this.lblNumberValue.TabIndex = 11;
            this.lblNumberValue.Location = new System.Drawing.Point(150, 213);
            this.lblNumberValue.Size = new System.Drawing.Size(282, 23);
            this.lblNumberValue.Text = "[ PASSPORT NUMBER ]";
            this.lblNumberValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblNumberValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblNumberValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNumberValue.Padding = new System.Windows.Forms.Padding(4,0,0,0);
            this.lblNumberValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNumberValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblSex
            this.lblSex.Name = "lblSex";
            this.lblSex.TabIndex = 12;
            this.lblSex.Location = new System.Drawing.Point(150, 239);
            this.lblSex.Size = new System.Drawing.Size(282, 16);
            this.lblSex.Text = "SEX";
            this.lblSex.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblSex.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblSexValue
            this.lblSexValue.Name = "lblSexValue";
            this.lblSexValue.TabIndex = 13;
            this.lblSexValue.Location = new System.Drawing.Point(150, 256);
            this.lblSexValue.Size = new System.Drawing.Size(282, 23);
            this.lblSexValue.Text = "[ SEX ]";
            this.lblSexValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblSexValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblSexValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSexValue.Padding = new System.Windows.Forms.Padding(4,0,0,0);
            this.lblSexValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSexValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblExpiry
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.TabIndex = 14;
            this.lblExpiry.Location = new System.Drawing.Point(150, 282);
            this.lblExpiry.Size = new System.Drawing.Size(282, 16);
            this.lblExpiry.Text = "EXPIRATION DATE";
            this.lblExpiry.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblExpiry.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblExpiryValue
            this.lblExpiryValue.Name = "lblExpiryValue";
            this.lblExpiryValue.TabIndex = 15;
            this.lblExpiryValue.Location = new System.Drawing.Point(150, 299);
            this.lblExpiryValue.Size = new System.Drawing.Size(282, 23);
            this.lblExpiryValue.Text = "[ EXPIRATION DATE ]";
            this.lblExpiryValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblExpiryValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblExpiryValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExpiryValue.Padding = new System.Windows.Forms.Padding(4,0,0,0);
            this.lblExpiryValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblExpiryValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblHint
            this.lblHint.Name = "lblHint";
            this.lblHint.TabIndex = 16;
            this.lblHint.Location = new System.Drawing.Point(18, 333);
            this.lblHint.Size = new System.Drawing.Size(414, 20);
            this.lblHint.Text = "Inspection: choose fields in two documents.";
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblHint.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.TabIndex = 17;
            this.btnClose.Location = new System.Drawing.Point(18, 359);
            this.btnClose.Size = new System.Drawing.Size(414, 27);
            this.btnClose.Text = "CLOSE DOCUMENT";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(105, 111, 86);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblExpiryValue);
            this.Controls.Add(this.lblExpiry);
            this.Controls.Add(this.lblSexValue);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblNumberValue);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblBirthValue);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.lblNationalityValue);
            this.Controls.Add(this.lblNationality);
            this.Controls.Add(this.lblNameValue);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPortrait);
            this.Controls.Add(this.picPortrait);
            this.Controls.Add(this.lblOffice);
            this.Controls.Add(this.lblTitle);
            // PassportForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "PassportForm";
            this.ClientSize = new System.Drawing.Size(450, 400);
            this.BackColor = System.Drawing.Color.FromArgb(190, 199, 166);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.Text = "Passport";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.picPortrait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.PictureBox picPortrait;
        private System.Windows.Forms.Label lblPortrait;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNameValue;
        private System.Windows.Forms.Label lblNationality;
        private System.Windows.Forms.Label lblNationalityValue;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.Label lblBirthValue;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblNumberValue;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblSexValue;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblExpiryValue;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnClose;
    }
}
