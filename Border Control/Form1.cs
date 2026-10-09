using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Border_Control
{
    public partial class Form1 : Form
    {
        private enum ShiftState { Waiting, Opening, Inspecting, Closing, Report, Finished }
        private ShiftState shiftState = ShiftState.Waiting;
        private GameSession session = new GameSession();
        private readonly TravelerGenerator generator = new TravelerGenerator();
        private readonly InspectionController inspection = new InspectionController();
        private readonly Dictionary<string, Form> documentWindows = new Dictionary<string, Form>();
        private Traveler traveler;
        private BulletinForm bulletin;

        public Form1()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            lobbyPanel.ActionRequested += Lobby_ActionRequested;
            inspection.Changed += Inspection_Changed;
            ResetBooth();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ShowBulletin();
            StartShift();
        }

        private void Lobby_ActionRequested(LobbyAction action)
        {
            // The scene only routes clicks; the existing game still handles decisions.
            switch (action)
            {
                case LobbyAction.Passport: OpenDocument("Passport"); break;
                case LobbyAction.EntryPermit: OpenDocument("EntryPermit"); break;
                case LobbyAction.WorkPermit: OpenDocument("WorkPermit"); break;
                case LobbyAction.HealthCertificate: OpenDocument("HealthCertificate"); break;
                case LobbyAction.Inspection:
                    if (shiftState == ShiftState.Inspecting) inspection.SetEnabled(!inspection.Enabled);
                    break;
                case LobbyAction.CancelSelection: inspection.Cancel(); break;
                case LobbyAction.Approve: Decide(true); break;
                case LobbyAction.Deny: Decide(false); break;
                case LobbyAction.Rulebook: ShowBulletin(); break;
            }
        }

        private void StartShift()
        {
            if (shiftState != ShiftState.Waiting || !session.StartShift()) return;
            CreateTravelerBehindShutter();
            shiftState = ShiftState.Opening;
            lblDialogue.Text = "The shutter is opening...";
            UpdateControls();
            shutterTimer.Start();
        }

        private void shutterTimer_Tick(object sender, EventArgs e)
        {
            if (shiftState == ShiftState.Opening)
            {
                lobbyPanel.SetShutter(Math.Max(-LobbyPanel.CanvasHeight, lobbyPanel.ShutterY - 8));
                if (lobbyPanel.ShutterY == -LobbyPanel.CanvasHeight)
                {
                    shutterTimer.Stop();
                    shiftState = ShiftState.Inspecting;
                    lblDialogue.Text = traveler.Purpose + " / AGE " + traveler.AgeOn(session.Rules.Date) + " / \"" + traveler.Dialogue + "\"";
                    UpdateControls();
                }
            }
            else if (shiftState == ShiftState.Closing)
            {
                lobbyPanel.SetShutter(Math.Min(0, lobbyPanel.ShutterY + 8));
                if (lobbyPanel.ShutterY != 0) return;
                traveler = null;
                lobbyPanel.SetTraveler(null);
                if (session.ShiftFinished)
                {
                    shutterTimer.Stop();
                    shiftState = ShiftState.Report;
                    lblDialogue.Text = "Shift complete. The booth is closed.";
                    UpdateControls();
                    BeginInvoke(new Action(FinishDay));
                }
                else
                {
                    CreateTravelerBehindShutter();
                    shiftState = ShiftState.Opening;
                    lblDialogue.Text = "Next traveler. The shutter is opening...";
                    UpdateControls();
                }
            }
        }

        private void CreateTravelerBehindShutter()
        {
            traveler = generator.Create(session.Rules, session.Processed + 1);
            lobbyPanel.SetTraveler(traveler);
        }

        private void Decide(bool approve)
        {
            if (shiftState != ShiftState.Inspecting || traveler == null) return;
            lblFeedback.Text = session.RecordDecision(traveler, approve);
            CloseDocumentWindows();
            inspection.SetEnabled(false);
            shiftState = ShiftState.Closing;
            lblDialogue.Text = "Decision stamped. The shutter is closing...";
            UpdateControls();
            shutterTimer.Start();
        }

        private void OpenDocument(string kind)
        {
            if (shiftState != ShiftState.Inspecting || traveler == null) return;
            Form existing;
            if (documentWindows.TryGetValue(kind, out existing) && !existing.IsDisposed)
            {
                existing.BringToFront();
                existing.Activate();
                return;
            }
            Form window = null;
            if (kind == "Passport" && traveler.Passport != null) window = new PassportForm(traveler, inspection);
            if (kind == "EntryPermit" && traveler.EntryPermit != null) window = new EntryPermitForm(traveler.EntryPermit, inspection);
            if (kind == "WorkPermit" && traveler.WorkPermit != null) window = new WorkPermitForm(traveler.WorkPermit, inspection);
            if (kind == "HealthCertificate" && traveler.HealthCertificate != null) window = new HealthCertificateForm(traveler.HealthCertificate, inspection);
            if (window == null) return;
            int index = documentWindows.Count;
            Rectangle screen = Screen.FromControl(this).WorkingArea;
            window.StartPosition = FormStartPosition.Manual;
            int x = Left + 24 + (index % 2) * 400 + (index / 2) * 30;
            int y = Top + 100 + (index / 2) * 110;
            window.Location = new Point(Math.Max(screen.Left, Math.Min(x, screen.Right - window.Width)),
                Math.Max(screen.Top, Math.Min(y, screen.Bottom - window.Height)));
            documentWindows[kind] = window;
            window.FormClosed += (sender, e) =>
            {
                Form saved;
                if (documentWindows.TryGetValue(kind, out saved) && saved == window) documentWindows.Remove(kind);
            };
            window.Show(this);
        }

        private void CloseDocumentWindows()
        {
            inspection.Cancel();
            Form[] windows = documentWindows.Values.ToArray();
            documentWindows.Clear();
            foreach (Form window in windows) if (!window.IsDisposed) window.Close();
        }

        private void Inspection_Changed(object sender, EventArgs e)
        {
            if (IsDisposed) return;
            UpdateInspectionDisplay();
        }

        private void UpdateInspectionDisplay()
        {
            lblMatchResult.Text = inspection.Enabled ? inspection.Message : "FIELD MATCHER / Click CHECK on the desk to inspect. Click x to cancel a pair.";
            lobbyPanel.SetInteraction(shiftState == ShiftState.Inspecting, inspection.Enabled, inspection.Message);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                inspection.Cancel();
                e.Handled = true;
            }
        }

        private void ShowBulletin()
        {
            if (bulletin != null && !bulletin.IsDisposed) { bulletin.Activate(); return; }
            bulletin = new BulletinForm(session.Rules);
            bulletin.Show(this);
        }

        private void FinishDay()
        {
            if (IsDisposed || shiftState != ShiftState.Report) return;
            DailyReport report = session.SettleDay();
            UpdateControls();
            using (var window = new ShiftReportForm(report)) window.ShowDialog(this);
            if (IsDisposed) return;
            if (session.NextDay())
            {
                ResetBooth();
                ShowBulletin();
                StartShift();
            }
            else
            {
                shiftState = ShiftState.Finished;
                lblDialogue.Text = session.EndingText();
                lblFeedback.Text = "CAMPAIGN COMPLETE / You finished all ten days.";
                UpdateControls();
            }
        }

        private void ResetBooth()
        {
            shutterTimer.Stop();
            CloseDocumentWindows();
            inspection.SetEnabled(false);
            if (bulletin != null && !bulletin.IsDisposed) bulletin.Close();
            traveler = null;
            shiftState = ShiftState.Waiting;
            lobbyPanel.SetShutter(0);
            lobbyPanel.SetTraveler(null);
            lblDialogue.Text = "The shutter opens automatically when the shift starts. The RULES book is on the desk.";
            lblFeedback.Text = lobbyPanel.ArtworkWarning ?? "Open the documents on the table. The original lobby layers are aligned at one shared origin.";
            UpdateControls();
        }

        private void UpdateControls()
        {
            lblDay.Text = "DAY " + session.Rules.Day + " / 10\n" + session.Rules.Date.ToString("yyyy-MM-dd");
            lblClock.Text = session.ClockText() + "\n" + session.Processed + "/" + session.Rules.TravelerLimit + " PROCESSED";
            lblBalance.Text = "BALANCE " + session.Balance + " cr\nPAY +" + session.Earnings + " / FINES -" + session.Penalties;
            btnNewCampaign.Visible = shiftState == ShiftState.Finished;
            UpdateInspectionDisplay();
        }

        private void btnNewCampaign_Click(object sender, EventArgs e)
        {
            if (shiftState != ShiftState.Finished) return;
            session = new GameSession();
            ResetBooth();
            ShowBulletin();
            StartShift();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) { CloseRelatedWindows(); }

        private void CloseRelatedWindows()
        {
            if (shutterTimer != null) shutterTimer.Stop();
            CloseDocumentWindows();
            if (bulletin != null && !bulletin.IsDisposed) bulletin.Close();
        }
    }
}
