using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Border_Control
{
    public enum Placeholder { Booth, Desk, Shutter, Traveler, Passport, EntryPermit, WorkPermit, HealthCertificate }

    public static class GameArt
    {
        // Standard PictureBoxes remain editable in the Toolbox/Designer.
        // An optional image is loaded once; missing or unreadable art uses drawn rectangles.
        public static void Attach(PictureBox box, string relativePath, Placeholder placeholder, int portraitStyle = 0)
        {
            Image ownedImage = null;
            string path = Path.Combine(Application.StartupPath, "Assets", relativePath);
            try
            {
                if (box.Image == null && File.Exists(path))
                {
                    using (var source = Image.FromFile(path)) ownedImage = new Bitmap(source);
                    box.Image = ownedImage;
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (ArgumentException) { }
            catch (OutOfMemoryException) { } // GDI+ also uses this for an invalid image.
            box.Paint += (sender, e) =>
            {
                Rectangle bounds = box.ClientRectangle;
                e.Graphics.Clear(box.BackColor);
                if (box.Image != null) DrawImage(e.Graphics, box.Image, bounds);
                else DrawPlaceholder(e.Graphics, bounds, placeholder, box.Tag is int ? (int)box.Tag : portraitStyle);
            };
            box.Disposed += (sender, e) => { if (ownedImage != null) ownedImage.Dispose(); };
        }

        private static void DrawImage(Graphics graphics, Image image, Rectangle area)
        {
            if (area.Width < 1 || area.Height < 1) return;
            float scale = Math.Min((float)area.Width / image.Width, (float)area.Height / image.Height);
            int width = Math.Max(1, (int)(image.Width * scale));
            int height = Math.Max(1, (int)(image.Height * scale));
            var target = new Rectangle(area.X + (area.Width - width) / 2, area.Y + (area.Height - height) / 2, width, height);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;
            graphics.DrawImage(image, target, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
        }

        public static void DrawImageIn(Graphics graphics, Image image, Rectangle area)
        {
            DrawImage(graphics, image, area);
        }

        public static void DrawPlaceholderIn(Graphics graphics, Rectangle area, Placeholder kind, int style = 0)
        {
            GraphicsState saved = graphics.Save();
            graphics.TranslateTransform(area.X, area.Y);
            DrawPlaceholder(graphics, new Rectangle(0, 0, area.Width, area.Height), kind, style);
            graphics.Restore(saved);
        }

        private static void DrawPlaceholder(Graphics graphics, Rectangle bounds, Placeholder kind, int style)
        {
            int width = bounds.Width;
            int height = bounds.Height;
            if (kind == Placeholder.Shutter)
            {
                graphics.Clear(Color.FromArgb(74, 78, 75));
                using (var dark = new Pen(Color.FromArgb(39, 43, 41), 3))
                using (var light = new Pen(Color.FromArgb(101, 105, 99)))
                    for (int y = 12; y < height; y += 24)
                    {
                        graphics.DrawLine(dark, 0, y, width, y);
                        graphics.DrawLine(light, 0, y + 3, width, y + 3);
                    }
                using (var handle = new SolidBrush(Color.FromArgb(27, 30, 28)))
                    graphics.FillRectangle(handle, width / 2 - 32, height - 24, 64, 8);
            }
            else if (kind == Placeholder.Traveler)
            {
                int unit = Math.Max(1, Math.Min(width / 16, height / 20));
                int x = width / 2;
                int y = height / 2 - 6 * unit;
                Color[] coats = { Color.FromArgb(76, 83, 66), Color.FromArgb(98, 75, 61), Color.FromArgb(75, 81, 87), Color.FromArgb(90, 71, 76) };
                using (var coat = new SolidBrush(coats[style % coats.Length]))
                using (var skin = new SolidBrush(Color.FromArgb(184, 163, 130)))
                using (var hair = new SolidBrush(Color.FromArgb(48, 42, 35)))
                {
                    graphics.FillRectangle(coat, x - 6 * unit, y + 8 * unit, 12 * unit, 11 * unit);
                    graphics.FillRectangle(skin, x - 3 * unit, y, 6 * unit, 8 * unit);
                    graphics.FillRectangle(hair, x - 3 * unit, y, 6 * unit, 2 * unit);
                    graphics.FillRectangle(hair, x - 2 * unit, y + 4 * unit, unit, unit);
                    graphics.FillRectangle(hair, x + unit, y + 4 * unit, unit, unit);
                }
            }
            else if (kind == Placeholder.Booth)
            {
                graphics.Clear(Color.FromArgb(117, 123, 110));
                using (var wall = new SolidBrush(Color.FromArgb(83, 88, 79)))
                using (var road = new SolidBrush(Color.FromArgb(65, 66, 60)))
                {
                    graphics.FillRectangle(wall, 0, height / 3, width, height / 3);
                    graphics.FillRectangle(road, 0, height * 2 / 3, width, height / 3);
                }
            }
            else if (kind == Placeholder.Desk)
            {
                graphics.Clear(Color.FromArgb(91, 73, 57));
                using (var seam = new Pen(Color.FromArgb(65, 53, 43), 2))
                    for (int x = 0; x < width; x += 180) graphics.DrawLine(seam, x, 0, x, height);
            }
            else
            {
                Color paper = kind == Placeholder.Passport ? Color.FromArgb(89, 102, 77) : Color.FromArgb(204, 196, 162);
                using (var fill = new SolidBrush(paper))
                using (var ink = new Pen(Color.FromArgb(54, 57, 47), 2))
                {
                    graphics.FillRectangle(fill, 4, 3, Math.Max(1, width - 8), Math.Max(1, height - 6));
                    graphics.DrawRectangle(ink, 5, 4, Math.Max(1, width - 11), Math.Max(1, height - 9));
                    for (int line = 1; line <= 3; line++) graphics.DrawLine(ink, width / 4, height * line / 5, width * 3 / 4, height * line / 5);
                }
            }
        }
    }
}
