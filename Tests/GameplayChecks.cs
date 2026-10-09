using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Border_Control;

internal static class GameplayChecks
{
    private static int checks;
    private static readonly BindingFlags privateInstance = BindingFlags.Instance | BindingFlags.NonPublic;

    [STAThread]
    private static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CheckRulesAndAccounting();
            CheckGenerators();
            CheckDesignerLoading();
            CheckMatchingWindows();
            CheckLobbyArtwork();
            CheckBoothCampaign();
            Console.WriteLine("PASS: " + checks + " assertions.");
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL: " + exception);
            return 1;
        }
    }

    private static void Assert(bool condition, string message)
    {
        checks++;
        if (!condition) throw new Exception(message);
    }

    private static T Control<T>(Form form, string name) where T : System.Windows.Forms.Control
    {
        return (T)form.Controls.Find(name, true).Single();
    }

    private static T Field<T>(Form form, string name)
    {
        return (T)form.GetType().GetField(name, privateInstance).GetValue(form);
    }

    private static void ClickField(Label label)
    {
        typeof(System.Windows.Forms.Control).GetMethod("OnClick", privateInstance).Invoke(label, new object[] { EventArgs.Empty });
    }

    private static void PumpUntil(Func<bool> done, string failure, int timeout = 5000)
    {
        var watch = Stopwatch.StartNew();
        while (!done())
        {
            Application.DoEvents();
            if (watch.ElapsedMilliseconds > timeout) throw new Exception(failure);
            Thread.Sleep(2);
        }
        Application.DoEvents();
    }

    private static void CheckRulesAndAccounting()
    {
        var generator = new TravelerGenerator(new Random(6));
        for (int day = 1; day <= 10; day++)
        {
            var rules = new DailyRules(day);
            Traveler t = generator.CreateValid(rules);
            Assert(rules.FindProblems(t).Count == 0, "Valid traveler rejected on day " + day);
            t.Passport.Expires = rules.Date;
            Assert(rules.FindProblems(t).Count == 0, "Expiry today must be valid.");
            t.Passport.Expires = rules.Date.AddDays(-1);
            Assert(rules.FindProblems(t).Contains("Passport expired."), "Expired passport accepted.");
            t.Passport = null;
            Assert(rules.FindProblems(t).Contains("Missing passport."), "Missing passport crashed or passed.");
            Assert(rules.BulletinText().Contains(rules.NewRule), "Bulletin omits new rule.");
        }
        Traveler foreign = generator.CreateValid(new DailyRules(8));
        foreign.Nationality = foreign.Passport.Nationality = "Norvia";
        foreign.Purpose = "WORK";
        foreign.EntryPermit = new EntryPermit { FullName = foreign.FullName, PassportNumber = foreign.Passport.Number,
            Purpose = "WORK", StayDays = 30, Expires = new DateTime(1987, 1, 1) };
        foreign.WorkPermit = new WorkPermit { FullName = foreign.FullName, Occupation = "Driver", Employer = "Rail",
            Purpose = "WORK", Expires = new DateTime(1987, 1, 1) };
        foreign.HealthCertificate = new HealthCertificate { FullName = foreign.FullName, Number = "HC-123456",
            Examined = new DateTime(1986, 10, 20), ValidUntil = new DateTime(1987, 1, 1), Cleared = true };
        Assert(new DailyRules(8).FindProblems(foreign).Count == 0, "Four valid documents rejected.");
        foreign.EntryPermit.FullName = "Different Name";
        Assert(!new DailyRules(3).FindProblems(foreign).Any(p => p.Contains("mismatch")), "Matching introduced too early.");
        Assert(new DailyRules(4).FindProblems(foreign).Contains("Entry permit name mismatch."), "Name mismatch not detected.");
        foreign.EntryPermit.FullName = foreign.FullName;
        foreign.EntryPermit.PassportNumber = "NO-999999";
        Assert(new DailyRules(4).FindProblems(foreign).Contains("Entry passport number mismatch."), "Number mismatch not detected.");
        foreign.EntryPermit.PassportNumber = foreign.Passport.Number;
        foreign.EntryPermit.Expires = new DateTime(1986, 10, 1);
        Assert(new DailyRules(2).FindProblems(foreign).Contains("Entry permit expired."), "Expired entry permit passed.");
        foreign.EntryPermit.Expires = new DateTime(1987, 1, 1);
        foreign.EntryPermit.StayDays = 120;
        Assert(!new DailyRules(7).FindProblems(foreign).Any(p => p.Contains("Stay")), "Duration introduced too early.");
        Assert(new DailyRules(8).FindProblems(foreign).Any(p => p.Contains("Stay")), "Duration limit not enforced.");
        foreign.EntryPermit.StayDays = 30;
        foreign.WorkPermit.Expires = new DateTime(1986, 10, 1);
        Assert(new DailyRules(5).FindProblems(foreign).Contains("Work permit expired."), "Expired work permit passed.");
        foreign.WorkPermit.Expires = new DateTime(1987, 1, 1);
        foreign.WorkPermit.FullName = "Wrong Name";
        Assert(new DailyRules(5).FindProblems(foreign).Contains("Work permit name mismatch."), "Work name mismatch passed.");
        foreign.WorkPermit.FullName = foreign.FullName;
        foreign.HealthCertificate.Cleared = false;
        Assert(new DailyRules(6).FindProblems(foreign).Contains("Medical clearance refused."), "Invalid clearance passed.");
        foreign.HealthCertificate.Cleared = true;
        foreign.HealthCertificate.FullName = "Wrong Name";
        Assert(new DailyRules(6).FindProblems(foreign).Contains("Health certificate name mismatch."), "Health mismatch passed.");
        foreign.HealthCertificate.FullName = foreign.FullName;
        foreign.HealthCertificate.ValidUntil = new DateTime(1986, 10, 31);
        Assert(new DailyRules(6).FindProblems(foreign).Contains("Health certificate expired."), "Expired health passed.");
        foreign.HealthCertificate.ValidUntil = new DateTime(1987, 1, 1);
        foreign.HealthCertificate.Examined = new DateTime(1986, 12, 1);
        Assert(new DailyRules(7).FindProblems(foreign).Contains("Invalid medical examination date."), "Future examination passed.");
        foreign.HealthCertificate.Examined = new DateTime(1986, 10, 20);
        foreign.Passport.Number = "BAD";
        Assert(new DailyRules(7).FindProblems(foreign).Contains("Invalid passport number."), "Malformed passport number passed.");
        foreign.Passport.Number = foreign.EntryPermit.PassportNumber;
        foreign.EntryPermit.Purpose = "VISIT";
        Assert(new DailyRules(8).FindProblems(foreign).Contains("Entry purpose does not match the stated purpose."), "Stated purpose mismatch passed.");
        foreign.EntryPermit.Purpose = "WORK";
        foreign.WorkPermit = null;
        Assert(!new DailyRules(4).FindProblems(foreign).Any(p => p.Contains("work")), "Work required too early.");
        Assert(new DailyRules(5).FindProblems(foreign).Contains("Missing work permit."), "Missing work permit passed.");
        foreign.HealthCertificate = null;
        Assert(!new DailyRules(5).FindProblems(foreign).Any(p => p.Contains("health")), "Health required too early.");
        Assert(new DailyRules(6).FindProblems(foreign).Contains("Missing health certificate."), "Missing health passed.");
        foreign.EntryPermit = null;
        Assert(new DailyRules(2).FindProblems(foreign).Contains("Missing entry permit."), "Missing entry passed.");
        using (var bulletin = new BulletinForm(new DailyRules(10)))
            Assert(!Control<TextBox>(bulletin, "txtRules").Text.Replace("\r\n", "").Contains("\n"), "Bulletin contains unsupported lone line feeds.");

        var session = new GameSession();
        Assert(!session.NextDay(), "Can skip an unsettled shift.");
        Assert(session.StartShift() && !session.StartShift(), "Shift can start twice.");
        for (int i = 0; i < session.Rules.TravelerLimit; i++)
            session.RecordDecision(generator.CreateValid(session.Rules), i >= 3);
        Assert(session.Correct == 3 && session.Incorrect == 3 && session.Earnings == 18 && session.Penalties == 10,
            "Warnings, salary or third-error fine wrong.");
        DailyReport report = session.SettleDay();
        report.Story = "First line.\nSecond line.";
        using (var printed = new ShiftReportForm(report))
            Assert(Control<TextBox>(printed, "txtReport").Text.Contains("First line.\r\nSecond line."), "Report story line breaks collapse.");
        Assert(report.Balance == 23 && report.Expenses == 35, "Settlement wrong.");
        Assert(session.SettleDay() == report && session.Balance == 23, "Settlement paid twice.");
        Assert(session.NextDay() && !session.ShiftStarted && session.Processed == 0, "Next day didn't reset.");
        while (session.Rules.Day < 9)
        {
            session.StartShift();
            while (!session.ShiftFinished) session.RecordDecision(generator.CreateValid(session.Rules), true);
            session.SettleDay();
            session.NextDay();
        }
        session.StartShift();
        session.RecordDecision(generator.CreateValid(session.Rules), false);
        session.RecordDecision(generator.CreateValid(session.Rules), false);
        session.RecordDecision(generator.CreateValid(session.Rules), false);
        Assert(session.Penalties == 15, "Day 9 fine did not increase.");
        Console.WriteLine("PASS: rule boundaries, missing documents and accounting.");
    }

    private static void CheckGenerators()
    {
        var generator = new TravelerGenerator(new Random(42));
        int valid = 0;
        int total = 0;
        for (int day = 1; day <= 10; day++)
        {
            var rules = new DailyRules(day);
            for (int i = 0; i < 300; i++)
            {
                Traveler traveler = generator.CreateValid(rules);
                Assert(rules.FindProblems(traveler).Count == 0, "CreateValid violates rules.");
                Assert(traveler.AgeOn(rules.Date) >= 19 && traveler.AgeOn(rules.Date) <= 65, "Unbelievable generated age.");
                traveler = generator.Create(rules, 99);
                if (rules.FindProblems(traveler).Count == 0) valid++;
                total++;
            }
        }
        Assert(valid > total * 0.55 && valid < total * 0.75, "Suspicion balance out of range.");
        Assert(generator.Create(new DailyRules(3), 3).SpecialRole == "REFUGEE", "Refugee missing.");
        Assert(generator.Create(new DailyRules(5), 3).WorkPermit == null, "Special worker missing.");
        Assert(generator.Create(new DailyRules(9), 4).SpecialRole == "COURIER", "Courier missing.");
        Assert(generator.Create(new DailyRules(10), 5).SpecialRole == "INVESTIGATOR", "Investigator missing.");
        Console.WriteLine("PASS: 3,000 valid fixtures; random valid rate " + (100 * valid / total) + "%; scripted visitors.");
    }

    private static void CheckDesignerLoading()
    {
        foreach (Type type in new[] { typeof(Form1), typeof(PassportForm), typeof(EntryPermitForm),
            typeof(WorkPermitForm), typeof(HealthCertificateForm), typeof(BulletinForm), typeof(ShiftReportForm) })
        {
            using (var surface = new DesignSurface())
            {
                surface.BeginLoad(type);
                Assert(surface.LoadErrors.Count == 0, "Designer load failed: " + type.Name);
                Assert(surface.View is System.Windows.Forms.Control, "Missing Designer view: " + type.Name);
            }
        }
        Console.WriteLine("PASS: all seven Forms load in a WinForms DesignSurface.");
    }

    private static void CheckMatchingWindows()
    {
        var rules = new DailyRules(8);
        var t = new TravelerGenerator(new Random(8)).CreateValid(rules);
        t.EntryPermit = new EntryPermit { FullName = t.FullName, PassportNumber = t.Passport.Number, Purpose = t.Purpose,
            Expires = rules.Date.AddDays(30), StayDays = 20 };
        t.WorkPermit = new WorkPermit { FullName = "Different Name", Occupation = "Driver", Employer = "Rail",
            Expires = rules.Date.AddDays(30), Purpose = "WORK" };
        var inspection = new InspectionController();
        using (var passport = new PassportForm(t, inspection))
        using (var entry = new EntryPermitForm(t.EntryPermit, inspection))
        using (var work = new WorkPermitForm(t.WorkPermit, inspection))
        using (var health = new HealthCertificateForm(t.HealthCertificate, inspection))
        {
            passport.Show(); entry.Show(); work.Show(); health.Show(); Application.DoEvents();
            Label a = Control<Label>(passport, "lblNameValue");
            Label b = Control<Label>(entry, "lblNameValue");
            Color original = a.BackColor;
            ClickField(a);
            Assert(inspection.Message == "Inspection off.", "Fields compare while inspection disabled.");
            inspection.SetEnabled(true);
            ClickField(a);
            Assert(a.BackColor != original, "First field not highlighted.");
            ClickField(Control<Label>(entry, "lblExpiryValue"));
            Assert(inspection.Message.Contains("Different field types"), "Unrelated fields compared.");
            ClickField(b);
            Assert(inspection.Message.StartsWith("MATCH"), "Equal names mismatched.");
            ClickField(a);
            ClickField(Control<Label>(work, "lblNameValue"));
            Assert(inspection.Message.StartsWith("MISMATCH"), "Different values matched.");
            inspection.Cancel();
            Assert(a.BackColor == original, "Cancel leaves highlights.");
            ClickField(Control<Label>(health, "lblNameValue"));
            ClickField(a);
            Assert(inspection.Message.StartsWith("MATCH"), "Health window comparison failed.");
            passport.Close();
            Assert(inspection.Message == "Choose a field in a document.", "Closed form leaves stale selection.");
            Assert(DailyRules.Same("  Lena Dima ", "lena dima"), "Comparison normalization failed.");
        }
        Console.WriteLine("PASS: real modeless document Forms, highlighting, match/mismatch, cancel and disposal.");
    }

    private static void ClickScene(LobbyPanel scene, LobbyAction action)
    {
        Rectangle area = scene.HotspotBounds(action);
        Point point = scene.CanvasToClient(new Point(area.Left + area.Width / 2, area.Top + area.Height / 2));
        typeof(System.Windows.Forms.Control).GetMethod("OnMouseDown", privateInstance).Invoke(scene,
            new object[] { new MouseEventArgs(MouseButtons.Left, 1, point.X, point.Y, 0) });
    }

    private static Bitmap Render(LobbyPanel scene)
    {
        var bitmap = new Bitmap(scene.Width, scene.Height);
        scene.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
        return bitmap;
    }

    private static void CheckLobbyArtwork()
    {
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ControlBorderArt", "Lobby");
        using (var scene = new LobbyPanel { Size = new Size(450, 280) })
        using (var background = new Bitmap(System.IO.Path.Combine(path, "BackGround.png")))
        using (var glass = new Bitmap(System.IO.Path.Combine(path, "GlassPanel.png")))
        using (var metal = new Bitmap(System.IO.Path.Combine(path, "MetalPanel.png")))
        using (var table = new Bitmap(System.IO.Path.Combine(path, "Table.png")))
        using (var walls = new Bitmap(System.IO.Path.Combine(path, "Walls.png")))
        {
            scene.CreateControl();
            Assert(scene.LoadedLayerCount == 5 && scene.ArtworkWarning == null, "Not all original layers loaded.");
            Assert(glass.GetPixel(100, 50).A == 51 && glass.GetPixel(221, 140).A == 0, "Glass transparency metadata changed.");
            using (Bitmap closed = Render(scene))
            {
                Assert(closed.GetPixel(100, 50).ToArgb() == metal.GetPixel(100, 50).ToArgb(), "Closed shutter recolored or misaligned.");
                Assert(closed.GetPixel(32, 230).ToArgb() == table.GetPixel(32, 230).ToArgb(), "Table isn't in front of the lower wall.");
                Assert(closed.GetPixel(32, 50).ToArgb() == walls.GetPixel(32, 50).ToArgb(), "Upper wall frame misaligned.");
                Assert(closed.GetPixel(100, 195).ToArgb() == table.GetPixel(100, 195).ToArgb(), "Foreground table misaligned.");
                var traveler = new TravelerGenerator(new Random(3)).CreateValid(new DailyRules(8));
                scene.SetTraveler(traveler);
                using (Bitmap hidden = Render(scene))
                    for (int y = 5; y < 184; y += 5)
                        for (int x = 45; x < 408; x += 5)
                            Assert(hidden.GetPixel(x, y) == closed.GetPixel(x, y), "Traveler visible through closed shutter.");
                scene.SetShutter(-LobbyPanel.CanvasHeight);
                using (Bitmap open = Render(scene))
                {
                    Color under = background.GetPixel(100, 50);
                    Color overlay = glass.GetPixel(100, 50);
                    Color actual = open.GetPixel(100, 50);
                    int red = (overlay.R * overlay.A + under.R * (255 - overlay.A) + 127) / 255;
                    int green = (overlay.G * overlay.A + under.G * (255 - overlay.A) + 127) / 255;
                    int blue = (overlay.B * overlay.A + under.B * (255 - overlay.A) + 127) / 255;
                    Assert(Math.Abs(actual.R - red) <= 1 && Math.Abs(actual.G - green) <= 1 && Math.Abs(actual.B - blue) <= 1,
                        "Glass alpha not composited over background.");
                    Assert(open.GetPixel(225, 130) != closed.GetPixel(225, 130), "Traveler never revealed.");
                    Assert(open.GetPixel(225, 220).ToArgb() == table.GetPixel(225, 220).ToArgb(), "Traveler overlaps foreground table.");
                    scene.Size = new Size(900, 560);
                    Assert(scene.SceneScale == 2, "Integer scale not selected.");
                    using (Bitmap enlarged = Render(scene))
                        for (int y = 20; y < 184; y += 13)
                            for (int x = 50; x < 400; x += 17)
                                for (int a = 0; a < 2; a++)
                                    for (int b = 0; b < 2; b++)
                                        Assert(enlarged.GetPixel(x * 2 + a, y * 2 + b) == open.GetPixel(x, y), "Pixel art blurred or layers scaled independently.");
                }
                bool blocked = false;
                try { scene.SetTraveler(null); } catch (InvalidOperationException) { blocked = true; }
                Assert(blocked, "Traveler can be replaced with open shutter.");
            }
            scene.SetShutter(0);
            scene.SetTraveler(null);
            foreach (Size size in new[] { new Size(450, 280), new Size(1015, 620), new Size(1350, 840), new Size(337, 215) })
            {
                scene.Size = size;
                scene.SetInteraction(false, false, "");
                Point source = new Point(32, 230);
                Assert(scene.HitTest(scene.CanvasToClient(source)) == null, "Decorative wall square has a click action.");
                Assert(scene.HitTest(new Point(-1, -1)) == null, "Outside-canvas click accepted.");
                scene.SetTraveler(new TravelerGenerator(new Random(6)).CreateValid(new DailyRules(8)));
                scene.SetInteraction(true, true, "");
                Assert(scene.HitTest(scene.CanvasToClient(source)) == null, "Wall square becomes interactive during inspection.");
                foreach (LobbyAction action in new[] { LobbyAction.Passport, LobbyAction.Inspection, LobbyAction.CancelSelection,
                    LobbyAction.Approve, LobbyAction.Deny, LobbyAction.Rulebook })
                {
                    Rectangle area = scene.HotspotBounds(action);
                    Assert(scene.HitTest(scene.CanvasToClient(new Point(area.Left + area.Width / 2, area.Top + area.Height / 2))) == action,
                        "Hitbox lost after resize: " + action);
                }
            }
        }
        Console.WriteLine("PASS: all five original layers, glass alpha, shutter masking, foreground depth, sharp 2x pixels and resized hitboxes.");
    }

    private static void CheckBoothCampaign()
    {
        int reports = 0;
        using (var booth = new Form1())
        using (var reportCloser = new System.Windows.Forms.Timer { Interval = 15 })
        {
            var session = Field<GameSession>(booth, "session");
            var scene = Control<LobbyPanel>(booth, "lobbyPanel");
            Assert(scene.LoadedLayerCount == 5, "Campaign didn't load user artwork.");
            Assert(Field<Traveler>(booth, "traveler") == null && scene.ShutterY == 0, "Traveler arrives before the booth is shown.");
            Assert(!scene.ActionAvailable(LobbyAction.Approve), "Can decide before opening.");
            booth.Show(); Application.DoEvents();
            Traveler firstTraveler = Field<Traveler>(booth, "traveler");
            Assert(session.ShiftStarted && firstTraveler != null, "Initial shift didn't start automatically.");
            Assert(!scene.ActionAvailable(LobbyAction.Passport), "Opening isn't locked.");
            foreach (BulletinForm book in Application.OpenForms.OfType<BulletinForm>().ToArray()) book.Close();
            ClickScene(scene, LobbyAction.Approve);
            Assert(session.Processed == 0, "Decision accepted during opening.");
            PumpUntil(() => scene.ShutterY < 0, "Shutter never moved.");
            Assert(scene.ShutterY > -LobbyPanel.CanvasHeight, "Shutter jumped without animation.");
            PumpUntil(() => scene.CanInspect, "Shutter never opened.");
            ClickScene(scene, LobbyAction.Passport);
            var docs = Field<Dictionary<string, Form>>(booth, "documentWindows");
            Assert(docs.Count == 1, "Passport doesn't open from art.");
            Form firstWindow = docs["Passport"];
            ClickScene(scene, LobbyAction.Passport);
            Assert(docs.Count == 1 && docs["Passport"] == firstWindow, "Duplicate document windows.");
            firstWindow.Close();
            Assert(docs.Count == 0 && firstWindow.IsDisposed, "Manual close not cleaned up.");
            ClickScene(scene, LobbyAction.Passport);
            Assert(docs.Count == 1, "Cannot reopen disposed document.");
            Form reopened = docs["Passport"];
            ClickScene(scene, LobbyAction.Deny);
            Assert(reopened.IsDisposed && docs.Count == 0, "Decision did not close document windows.");
            Assert(Field<Traveler>(booth, "traveler") == firstTraveler, "Traveler swapped before shutter closes.");
            ClickScene(scene, LobbyAction.Approve);
            Assert(session.Processed == 1, "Double decision accepted during closing.");
            PumpUntil(() => Field<Traveler>(booth, "traveler") != firstTraveler, "Traveler never replaced.");
            PumpUntil(() => scene.CanInspect, "Second traveler never ready.");
            // Initial motion uses production timing; speed up the full campaign only.
            Field<System.Windows.Forms.Timer>(booth, "shutterTimer").Interval = 1;
            reportCloser.Tick += (sender, e) =>
            {
                ShiftReportForm reportForm = Application.OpenForms.OfType<ShiftReportForm>().FirstOrDefault();
                if (reportForm == null) return;
                Assert(scene.ShutterY == 0 && docs.Count == 0, "Report shown before closed shutter/clean desk.");
                Assert(session.IsSettled, "Report shown before settlement.");
                reports++;
                Control<Button>(reportForm, "btnContinue").PerformClick();
            };
            reportCloser.Start();
            for (int day = 1; day <= 10; day++)
            {
                if (day > 1)
                {
                    Assert(session.ShiftStarted && Field<Traveler>(booth, "traveler") != null,
                        "Next day's shift didn't start automatically.");
                    foreach (BulletinForm book in Application.OpenForms.OfType<BulletinForm>().ToArray()) book.Close();
                    PumpUntil(() => scene.CanInspect, "Next day never started.");
                }
                int limit = session.Rules.TravelerLimit;
                while (session.Rules.Day == day && session.Processed < limit)
                {
                    Traveler current = Field<Traveler>(booth, "traveler");
                    bool approve = session.Rules.FindProblems(current).Count == 0;
                    if (day == 6 && current.EntryPermit != null && current.HealthCertificate != null)
                    {
                        foreach (LobbyAction action in new[] { LobbyAction.Passport, LobbyAction.EntryPermit, LobbyAction.WorkPermit, LobbyAction.HealthCertificate })
                            if (scene.ActionAvailable(action)) ClickScene(scene, action);
                        Assert(docs.Count >= 3, "Multiple document windows unavailable.");
                        ClickScene(scene, LobbyAction.Inspection);
                        ClickField(Control<Label>(docs["Passport"], "lblNameValue"));
                        ClickField(Control<Label>(docs["EntryPermit"], "lblNameValue"));
                        string expected = DailyRules.Same(current.Passport.FullName, current.EntryPermit.FullName) ? "MATCH" : "MISMATCH";
                        Assert(Control<Label>(booth, "lblMatchResult").Text.StartsWith(expected), "Art machine doesn't use document controller.");
                        ClickScene(scene, LobbyAction.CancelSelection);
                        Assert(Control<Label>(booth, "lblMatchResult").Text == "Choose a field in a document.", "Machine cancel hotspot failed.");
                    }
                    ClickScene(scene, approve ? LobbyAction.Approve : LobbyAction.Deny);
                    Assert(docs.Count == 0, "Documents survive departure.");
                    if (session.Processed == limit)
                        PumpUntil(() => reports == day && (day == 10 || session.Rules.Day == day + 1), "Report/day change failed.");
                    else PumpUntil(() => scene.CanInspect, "Next traveler transition failed.");
                }
            }
            Assert(reports == 10 && session.TotalProcessed == 80, "Campaign length wrong.");
            Assert(!session.NextDay() && scene.ShutterY == 0 && !scene.CanInspect, "Final day/reset state wrong.");
            Assert(Control<Button>(booth, "btnNewCampaign").Visible, "Replay button missing after final day.");
            Assert(Application.OpenForms.OfType<PassportForm>().Count() == 0, "Documents leak after campaign.");
            Control<Button>(booth, "btnNewCampaign").PerformClick();
            session = Field<GameSession>(booth, "session");
            Assert(session.Rules.Day == 1 && session.Balance == 50 && session.ShiftStarted, "New campaign didn't reset and start automatically.");
            booth.Close();
            Assert(!Application.OpenForms.Cast<Form>().Any(), "Owned windows survive main form disposal.");
        }
        Console.WriteLine("PASS: automatic shift starts, artwork clicks, real Timer motion, transition locks, document lifecycle, 80 travelers and 10 reports.");
    }
}

