namespace Border_Control
{
    partial class ShiftReportForm
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
            this.txtReport = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Location = new System.Drawing.Point(18, 16);
            this.lblTitle.Size = new System.Drawing.Size(484, 30);
            this.lblTitle.Text = "SHIFT REPORT";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            // txtReport
            this.txtReport.Name = "txtReport";
            this.txtReport.TabIndex = 1;
            this.txtReport.Location = new System.Drawing.Point(18, 60);
            this.txtReport.Size = new System.Drawing.Size(484, 445);
            this.txtReport.Text = "";
            this.txtReport.Multiline = true;
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReport.BackColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.txtReport.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.txtReport.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtReport.TabStop = false;
            // btnContinue
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Location = new System.Drawing.Point(18, 520);
            this.btnContinue.Size = new System.Drawing.Size(484, 28);
            this.btnContinue.Text = "CONTINUE TO NEXT DAY";
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(105, 111, 86);
            this.btnContinue.ForeColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.btnContinue.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.txtReport);
            this.Controls.Add(this.lblTitle);
            // ShiftReportForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular);
            this.Name = "ShiftReportForm";
            this.ClientSize = new System.Drawing.Size(520, 560);
            this.BackColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 48, 39);
            this.Text = "SHIFT REPORT";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ControlBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.Button btnContinue;
    }
}
