using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Border_Control
{
    public enum LobbyAction { Passport, EntryPermit, WorkPermit, HealthCertificate, Inspection, CancelSelection, Approve, Deny, Rulebook }

    // One WinForms Panel composites transparent PNGs. This avoids the fake
    // transparency of overlapping PictureBoxes and keeps every layer aligned.
    public class LobbyPanel : Panel
    {
        public const int CanvasWidth = 450;
        public const int CanvasHeight = 280;
        private readonly Dictionary<string, Image> images = new Dictionary<string, Image>();
        private readonly ToolTip hints = new ToolTip();
        private readonly Font smallFont = new Font("Consolas", 6F, FontStyle.Bold, GraphicsUnit.Pixel);
        private readonly Dictionary<LobbyAction, Rectangle> hotspots = new Dictionary<LobbyAction, Rectangle>
        {
            { LobbyAction.Passport, new Rectangle(63, 216, 32, 42) },
            { LobbyAction.EntryPermit, new Rectangle(107, 216, 32, 42) },
            { LobbyAction.WorkPermit, new Rectangle(151, 216, 32, 42) },
            { LobbyAction.HealthCertificate, new Rectangle(195, 216, 32, 42) },
            { LobbyAction.Inspection, new Rectangle(256, 219, 65, 38) },
            { LobbyAction.CancelSelection, new Rectangle(313, 219, 8, 10) },
            { LobbyAction.Approve, new Rectangle(339, 217, 41, 20) },
            { LobbyAction.Deny, new Rectangle(339, 245, 41, 20) },
            { LobbyAction.Rulebook, new Rectangle(389, 219, 20, 40) }
        };
        private Traveler traveler;
        private int shutterY;
        private string hovered = "";
        private bool canInspect;
        private bool inspectionOn;
        private string matchText = "";

        public event Action<LobbyAction> ActionRequested;
        public int ShutterY { get { return shutterY; } }
        public bool TravelerVisible { get { return traveler != null; } }
        public bool CanInspect { get { return canInspect; } }
        public int LoadedLayerCount { get; private set; }
        public string ArtworkWarning { get; private set; }

        public LobbyPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.FromArgb(32, 33, 30);
            TabStop = true;
            LoadArtwork();
        }

        private void LoadArtwork()
        {
            string assemblyFolder = Path.GetDirectoryName(typeof(LobbyPanel).Assembly.Location);
            string root = Path.Combine(Application.StartupPath, "ControlBorderArt", "Lobby");
            if (!Directory.Exists(root)) root = Path.Combine(assemblyFolder, "ControlBorderArt", "Lobby");
            foreach (string name in new[] { "BackGround.png", "GlassPanel.png", "MetalPanel.png", "Table.png", "Walls.png" })
            {
                Image image = ReadImage(Path.Combine(root, name));
                if (image == null)
                {
                    ArtworkWarning = "Missing or unreadable lobby layer: " + name;
                    continue;
                }
                if (image.Size != new Size(CanvasWidth, CanvasHeight))
                {
                    image.Dispose();
                    ArtworkWarning = name + " must use the shared 450 x 280 canvas.";
                    continue;
                }
                images[name] = image;
                LoadedLayerCount++;
            }
            string assets = Path.Combine(Application.StartupPath, "Assets");
            LoadOptional("Traveler", Path.Combine(assets, "Characters", "TravelerPlaceholder.png"));
            LoadOptional("Passport", Path.Combine(assets, "Documents", "Passport.png"));
            LoadOptional("EntryPermit", Path.Combine(assets, "Documents", "EntryPermit.png"));
            LoadOptional("WorkPermit", Path.Combine(assets, "Documents", "WorkPermit.png"));
            LoadOptional("HealthCertificate", Path.Combine(assets, "Documents", "HealthCertificate.png"));
        }

        private void LoadOptional(string key, string path)
        {
            Image image = ReadImage(path);
            if (image != null) images[key] = image;
        }

        private static Image ReadImage(string path)
        {
            try
            {
                if (!File.Exists(path)) return null;
                using (Image source = Image.FromFile(path)) return new Bitmap(source);
            }
            catch (IOException) { return null; }
            catch (UnauthorizedAccessException) { return null; }
            catch (ArgumentException) { return null; }
            catch (OutOfMemoryException) { return null; }
        }

        public void SetTraveler(Traveler value)
        {
            // Called only behind a completely closed shutter.
            if (shutterY != 0) throw new InvalidOperationException("Close the shutter before replacing a traveler.");
            traveler = value;
            Invalidate();
        }

        public void SetShutter(int y)
        {
            shutterY = Math.Max(-CanvasHeight, Math.Min(0, y));
            Invalidate();
        }

        public void SetInteraction(bool inspect, bool mode, string result)
        {
            canInspect = inspect;
            inspectionOn = mode;
            matchText = result ?? "";
            Invalidate();
        }

        public float SceneScale
        {
            get
            {
                float fit = Math.Min((float)ClientSize.Width / CanvasWidth, (float)ClientSize.Height / CanvasHeight);
                return fit >= 1 ? (float)Math.Floor(fit) : Math.Max(0.01F, fit);
            }
        }

        public Rectangle DisplayBounds
        {
            get
            {
                int width = (int)(CanvasWidth * SceneScale);
                int height = (int)(CanvasHeight * SceneScale);
                return new Rectangle((ClientSize.Width - width) / 2, (ClientSize.Height - height) / 2, width, height);
            }
        }

        public Point CanvasToClient(Point point)
        {
            Rectangle display = DisplayBounds;
            return new Point(display.Left + (int)(point.X * SceneScale), display.Top + (int)(point.Y * SceneScale));
        }

        public Point ClientToCanvas(Point point)
        {
            Rectangle display = DisplayBounds;
            if (!display.Contains(point)) return new Point(-1, -1);
            return new Point((int)((point.X - display.Left) / SceneScale), (int)((point.Y - display.Top) / SceneScale));
        }

        public Rectangle HotspotBounds(LobbyAction action) { return hotspots[action]; }

        public bool ActionAvailable(LobbyAction action)
        {
            if (action == LobbyAction.Rulebook) return true;
            if (!canInspect || traveler == null) return false;
            switch (action)
            {
                case LobbyAction.Passport: return traveler.Passport != null;
                case LobbyAction.EntryPermit: return traveler.EntryPermit != null;
                case LobbyAction.WorkPermit: return traveler.WorkPermit != null;
                case LobbyAction.HealthCertificate: return traveler.HealthCertificate != null;
                case LobbyAction.CancelSelection: return inspectionOn;
                default: return true;
            }
        }

        public LobbyAction? HitTest(Point clientPoint)
        {
            Point point = ClientToCanvas(clientPoint);
            // The cancel corner is inside the machine; test it first.
            if (hotspots[LobbyAction.CancelSelection].Contains(point) && ActionAvailable(LobbyAction.CancelSelection))
                return LobbyAction.CancelSelection;
            foreach (var area in hotspots)
                if (area.Key != LobbyAction.CancelSelection && area.Value.Contains(point) && ActionAvailable(area.Key))
                    return area.Key;
            return null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            Focus();
            LobbyAction? action = HitTest(e.Location);
            if (action.HasValue && ActionRequested != null) ActionRequested(action.Value);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            LobbyAction? action = HitTest(e.Location);
            Cursor = action.HasValue ? Cursors.Hand : Cursors.Default;
            string hint = action.HasValue ? action.Value.ToString() : "";
            if (hint == hovered) return;
            hovered = hint;
            if (action == LobbyAction.Inspection) hint = "Toggle inspection mode";
            if (action == LobbyAction.CancelSelection) hint = "Cancel selected fields";
            hints.SetToolTip(this, hint);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(BackColor);
            GraphicsState saved = g.Save();
            Rectangle display = DisplayBounds;
            g.TranslateTransform(display.Left, display.Top);
            g.ScaleTransform(SceneScale, SceneScale);
            g.SetClip(new Rectangle(0, 0, CanvasWidth, CanvasHeight));
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.None;
            g.CompositingMode = CompositingMode.SourceOver;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            // All five original PNGs use the SAME origin and dimensions.
            DrawLayer(g, "BackGround.png", 0);
            if (traveler != null) DrawTraveler(g);
            DrawLayer(g, "GlassPanel.png", 0); // Keep its original 20% alpha.
            if (images.ContainsKey("MetalPanel.png")) DrawLayer(g, "MetalPanel.png", shutterY);
            else GameArt.DrawPlaceholderIn(g, new Rectangle(42, shutterY, 368, CanvasHeight), Placeholder.Shutter);
            DrawLayer(g, "Walls.png", 0);
            DrawLayer(g, "Table.png", 0); // The table is the foremost lobby artwork layer.
            DrawDeskObjects(g);
            g.Restore(saved);
        }

        private void DrawLayer(Graphics g, string name, int y)
        {
            Image image;
            if (images.TryGetValue(name, out image))
                g.DrawImage(image, new Rectangle(0, y, CanvasWidth, CanvasHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        private void DrawTraveler(Graphics g)
        {
            var position = new Rectangle(164, 60, 124, 188);
            Image image;
            if (images.TryGetValue("Traveler", out image)) GameArt.DrawImageIn(g, image, position);
            else GameArt.DrawPlaceholderIn(g, position, Placeholder.Traveler, traveler.PortraitStyle);
        }

        private void DrawDeskObjects(Graphics g)
        {
            if (canInspect && traveler != null)
            {
                DrawDocument(g, LobbyAction.Passport, "PASS", Placeholder.Passport);
                DrawDocument(g, LobbyAction.EntryPermit, "ENTRY", Placeholder.EntryPermit);
                DrawDocument(g, LobbyAction.WorkPermit, "WORK", Placeholder.WorkPermit);
                DrawDocument(g, LobbyAction.HealthCertificate, "HEALTH", Placeholder.HealthCertificate);
            }
            Rectangle machine = hotspots[LobbyAction.Inspection];
            Fill(g, machine, Color.FromArgb(57, 63, 54));
            string display = !inspectionOn ? "CHECK" : matchText.StartsWith("MISMATCH") ? "NO MATCH" : matchText.StartsWith("MATCH") ? "MATCH" : "SELECT";
            TextAt(g, display, machine, Color.FromArgb(212, 206, 175));
            TextAt(g, "x", hotspots[LobbyAction.CancelSelection], Color.FromArgb(212, 206, 175));
            DrawStamp(g, LobbyAction.Approve, "APPROVE", Color.FromArgb(84, 112, 77));
            DrawStamp(g, LobbyAction.Deny, "DENY", Color.FromArgb(127, 75, 62));
            Rectangle book = hotspots[LobbyAction.Rulebook];
            Fill(g, book, Color.FromArgb(183, 169, 125));
            TextAt(g, "RULES", book, Color.FromArgb(45, 49, 39));
        }

        private void DrawDocument(Graphics g, LobbyAction action, string title, Placeholder kind)
        {
            if (!ActionAvailable(action)) return;
            Rectangle area = hotspots[action];
            Image image;
            if (images.TryGetValue(action.ToString(), out image)) GameArt.DrawImageIn(g, image, area);
            else GameArt.DrawPlaceholderIn(g, area, kind);
            TextAt(g, title, new Rectangle(area.Left - 3, area.Bottom + 3, area.Width + 6, 10), Color.FromArgb(219, 210, 176));
        }

        private void DrawStamp(Graphics g, LobbyAction action, string title, Color color)
        {
            Fill(g, hotspots[action], color);
            TextAt(g, title, hotspots[action], Color.FromArgb(226, 218, 189));
        }

        private static void Fill(Graphics g, Rectangle area, Color color)
        {
            using (var brush = new SolidBrush(color)) g.FillRectangle(brush, area);
            using (var pen = new Pen(Color.FromArgb(31, 35, 30))) g.DrawRectangle(pen, area.X, area.Y, area.Width - 1, area.Height - 1);
        }

        private void TextAt(Graphics g, string text, Rectangle area, Color color)
        {
            using (var brush = new SolidBrush(color))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                g.DrawString(text, smallFont, brush, area, format);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                hints.Dispose();
                smallFont.Dispose();
                foreach (Image image in images.Values) image.Dispose();
                images.Clear();
            }
            base.Dispose(disposing);
        }
    }
}
