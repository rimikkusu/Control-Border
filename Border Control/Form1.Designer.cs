namespace Border_Control
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CloseRelatedWindows();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblDay = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lobbyPanel = new Border_Control.LobbyPanel();
            this.lblDialogue = new System.Windows.Forms.Label();
            this.lblMatchResult = new System.Windows.Forms.Label();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.btnNewCampaign = new System.Windows.Forms.Button();
            this.shutterTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlHeader.Location = new System.Drawing.Point(12, 6);
            this.pnlHeader.Size = new System.Drawing.Size(906, 42);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.ColumnCount = 3;
            this.pnlHeader.RowCount = 1;
            this.pnlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlHeader.Controls.Add(this.lblDay, 0, 0);
            this.pnlHeader.Controls.Add(this.lblClock, 1, 0);
            this.pnlHeader.Controls.Add(this.lblBalance, 2, 0);
            // lblDay
            this.lblDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDay.Name = "lblDay";
            this.lblDay.Text = "DAY 1 / 10\r\n1986-11-01";
            this.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblClock
            this.lblClock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClock.Name = "lblClock";
            this.lblClock.Text = "08:00\r\n0/6 PROCESSED";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // lblBalance
            this.lblBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Text = "BALANCE 50 cr\r\nPAY +0 / FINES -0";
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // lobbyPanel
            this.lobbyPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lobbyPanel.Location = new System.Drawing.Point(12, 56);
            this.lobbyPanel.Size = new System.Drawing.Size(906, 560);
            this.lobbyPanel.Name = "lobbyPanel";
            this.lobbyPanel.TabIndex = 0;
            // lblDialogue
            this.lblDialogue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblDialogue.Location = new System.Drawing.Point(12, 624);
            this.lblDialogue.Size = new System.Drawing.Size(906, 48);
            this.lblDialogue.Name = "lblDialogue";
            this.lblDialogue.Text = "The shutter opens automatically when the shift starts.";
            // lblMatchResult
            this.lblMatchResult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblMatchResult.Location = new System.Drawing.Point(12, 680);
            this.lblMatchResult.Size = new System.Drawing.Size(906, 29);
            this.lblMatchResult.Name = "lblMatchResult";
            this.lblMatchResult.Text = "FIELD MATCHER / Click CHECK on the desk to inspect.";
            // lblFeedback
            this.lblFeedback.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblFeedback.Location = new System.Drawing.Point(12, 716);
            this.lblFeedback.Size = new System.Drawing.Size(754, 30);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Font = new System.Drawing.Font("Consolas", 8.5F);
            // btnNewCampaign (shown only after the final report)
            this.btnNewCampaign.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnNewCampaign.Location = new System.Drawing.Point(782, 716);
            this.btnNewCampaign.Size = new System.Drawing.Size(136, 28);
            this.btnNewCampaign.Name = "btnNewCampaign";
            this.btnNewCampaign.Text = "NEW CAMPAIGN";
            this.btnNewCampaign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewCampaign.BackColor = System.Drawing.Color.FromArgb(84, 112, 77);
            this.btnNewCampaign.UseVisualStyleBackColor = false;
            this.btnNewCampaign.Visible = false;
            this.btnNewCampaign.Click += new System.EventHandler(this.btnNewCampaign_Click);
            // shutterTimer
            this.shutterTimer.Interval = 25;
            this.shutterTimer.Tick += new System.EventHandler(this.shutterTimer_Tick);
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 756);
            this.MinimumSize = new System.Drawing.Size(500, 520);
            this.BackColor = System.Drawing.Color.FromArgb(32, 33, 30);
            this.ForeColor = System.Drawing.Color.FromArgb(217, 210, 181);
            this.Font = new System.Drawing.Font("Consolas", 9F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Border Control / Valdora";
            this.Name = "Form1";
            this.KeyPreview = true;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lobbyPanel);
            this.Controls.Add(this.lblDialogue);
            this.Controls.Add(this.lblMatchResult);
            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.btnNewCampaign);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel pnlHeader;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblBalance;
        private Border_Control.LobbyPanel lobbyPanel;
        private System.Windows.Forms.Label lblDialogue;
        private System.Windows.Forms.Label lblMatchResult;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.Button btnNewCampaign;
        private System.Windows.Forms.Timer shutterTimer;
    }
}

