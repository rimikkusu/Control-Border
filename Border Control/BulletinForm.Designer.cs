namespace Border_Control
{
    partial class BulletinForm
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
            this.txtRules = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Location = new System.Drawing.Point(18, 16);
            this.lblTitle.Size = new System.Drawing.Size(604, 30);
            this.lblTitle.Text = "DAILY BULLETIN";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            // txtRules
            this.txtRules.Name = "txtRules";
            this.txtRules.TabIndex = 1;
            this.txtRules.Location = new System.Drawing.Point(18, 60);
            this.txtRules.Size = new System.Drawing.Size(604, 475);
            this.txtRules.Text = "";
            this.txtRules.Multiline = true;
            this.txtRules.ReadOnly = true;
            this.txtRules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRules.BackColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.txtRules.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.txtRules.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtRules.TabStop = false;
            // btnClose
            this.btnClose.Name = "btnClose";
            this.btnClose.TabIndex = 2;
            this.btnClose.Location = new System.Drawing.Point(18, 550);
            this.btnClose.Size = new System.Drawing.Size(604, 28);
            this.btnClose.Text = "RETURN TO BOOTH";
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(105, 111, 86);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtRules);
            this.Controls.Add(this.lblTitle);
            // BulletinForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "BulletinForm";
            this.ClientSize = new System.Drawing.Size(640, 590);
            this.BackColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.Text = "DAILY BULLETIN";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ControlBox = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtRules;
        private System.Windows.Forms.Button btnClose;
    }
}
