namespace Border_Control
{
    partial class WorkPermitForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblNameValue = new System.Windows.Forms.Label();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.lblOccupationValue = new System.Windows.Forms.Label();
            this.lblEmployer = new System.Windows.Forms.Label();
            this.lblEmployerValue = new System.Windows.Forms.Label();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.lblPurposeValue = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblExpiryValue = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Size = new System.Drawing.Size(384, 27);
            this.lblTitle.Text = "WORK PERMIT";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            // lblOffice
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.TabIndex = 1;
            this.lblOffice.Location = new System.Drawing.Point(18, 44);
            this.lblOffice.Size = new System.Drawing.Size(384, 18);
            this.lblOffice.Text = "DEPARTMENT OF LABOR";
            this.lblOffice.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblOffice.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular);
            // lblName
            this.lblName.Name = "lblName";
            this.lblName.TabIndex = 2;
            this.lblName.Location = new System.Drawing.Point(18, 75);
            this.lblName.Size = new System.Drawing.Size(132, 26);
            this.lblName.Text = "FULL NAME";
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblName.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblNameValue
            this.lblNameValue.Name = "lblNameValue";
            this.lblNameValue.TabIndex = 3;
            this.lblNameValue.Location = new System.Drawing.Point(154, 75);
            this.lblNameValue.Size = new System.Drawing.Size(248, 27);
            this.lblNameValue.Text = "[ FULL NAME ]";
            this.lblNameValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblNameValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNameValue.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblNameValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblOccupation
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.TabIndex = 4;
            this.lblOccupation.Location = new System.Drawing.Point(18, 114);
            this.lblOccupation.Size = new System.Drawing.Size(132, 26);
            this.lblOccupation.Text = "OCCUPATION";
            this.lblOccupation.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblOccupation.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblOccupation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblOccupationValue
            this.lblOccupationValue.Name = "lblOccupationValue";
            this.lblOccupationValue.TabIndex = 5;
            this.lblOccupationValue.Location = new System.Drawing.Point(154, 114);
            this.lblOccupationValue.Size = new System.Drawing.Size(248, 27);
            this.lblOccupationValue.Text = "[ OCCUPATION ]";
            this.lblOccupationValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblOccupationValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblOccupationValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOccupationValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOccupationValue.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblOccupationValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblEmployer
            this.lblEmployer.Name = "lblEmployer";
            this.lblEmployer.TabIndex = 6;
            this.lblEmployer.Location = new System.Drawing.Point(18, 153);
            this.lblEmployer.Size = new System.Drawing.Size(132, 26);
            this.lblEmployer.Text = "EMPLOYER";
            this.lblEmployer.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblEmployer.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblEmployer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblEmployerValue
            this.lblEmployerValue.Name = "lblEmployerValue";
            this.lblEmployerValue.TabIndex = 7;
            this.lblEmployerValue.Location = new System.Drawing.Point(154, 153);
            this.lblEmployerValue.Size = new System.Drawing.Size(248, 27);
            this.lblEmployerValue.Text = "[ EMPLOYER ]";
            this.lblEmployerValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblEmployerValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblEmployerValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEmployerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmployerValue.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblEmployerValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblPurpose
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.TabIndex = 8;
            this.lblPurpose.Location = new System.Drawing.Point(18, 192);
            this.lblPurpose.Size = new System.Drawing.Size(132, 26);
            this.lblPurpose.Text = "PURPOSE";
            this.lblPurpose.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblPurpose.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblPurpose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblPurposeValue
            this.lblPurposeValue.Name = "lblPurposeValue";
            this.lblPurposeValue.TabIndex = 9;
            this.lblPurposeValue.Location = new System.Drawing.Point(154, 192);
            this.lblPurposeValue.Size = new System.Drawing.Size(248, 27);
            this.lblPurposeValue.Text = "[ PURPOSE ]";
            this.lblPurposeValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblPurposeValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblPurposeValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPurposeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPurposeValue.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblPurposeValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblExpiry
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.TabIndex = 10;
            this.lblExpiry.Location = new System.Drawing.Point(18, 231);
            this.lblExpiry.Size = new System.Drawing.Size(132, 26);
            this.lblExpiry.Text = "VALID UNTIL";
            this.lblExpiry.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblExpiry.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblExpiry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblExpiryValue
            this.lblExpiryValue.Name = "lblExpiryValue";
            this.lblExpiryValue.TabIndex = 11;
            this.lblExpiryValue.Location = new System.Drawing.Point(154, 231);
            this.lblExpiryValue.Size = new System.Drawing.Size(248, 27);
            this.lblExpiryValue.Text = "[ VALID UNTIL ]";
            this.lblExpiryValue.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblExpiryValue.BackColor = System.Drawing.Color.FromArgb(230, 224, 199);
            this.lblExpiryValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblExpiryValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblExpiryValue.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblExpiryValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
            // lblHint
            this.lblHint.Name = "lblHint";
            this.lblHint.TabIndex = 12;
            this.lblHint.Location = new System.Drawing.Point(18, 278);
            this.lblHint.Size = new System.Drawing.Size(384, 30);
            this.lblHint.Text = "Inspection: choose fields in two documents.";
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblHint.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Regular);
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.TabIndex = 13;
            this.btnClose.Location = new System.Drawing.Point(18, 318);
            this.btnClose.Size = new System.Drawing.Size(384, 27);
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
            this.Controls.Add(this.lblPurposeValue);
            this.Controls.Add(this.lblPurpose);
            this.Controls.Add(this.lblEmployerValue);
            this.Controls.Add(this.lblEmployer);
            this.Controls.Add(this.lblOccupationValue);
            this.Controls.Add(this.lblOccupation);
            this.Controls.Add(this.lblNameValue);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblOffice);
            this.Controls.Add(this.lblTitle);
            // WorkPermitForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "WorkPermitForm";
            this.ClientSize = new System.Drawing.Size(420, 358);
            this.BackColor = System.Drawing.Color.FromArgb(192, 201, 175);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.Text = "WORK PERMIT";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNameValue;
        private System.Windows.Forms.Label lblOccupation;
        private System.Windows.Forms.Label lblOccupationValue;
        private System.Windows.Forms.Label lblEmployer;
        private System.Windows.Forms.Label lblEmployerValue;
        private System.Windows.Forms.Label lblPurpose;
        private System.Windows.Forms.Label lblPurposeValue;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblExpiryValue;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnClose;
    }
}
