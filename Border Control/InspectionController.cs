using System;
using System.Drawing;
using System.Windows.Forms;

namespace Border_Control
{
    public enum FieldKind { Name, PassportNumber, BirthDate, Nationality, Purpose, Expiry, CertificateNumber, ExamDate, Clearance, Occupation, Employer, Stay, Sex }

    public class InspectionController
    {
        private class Field
        {
            public FieldKind Kind;
            public string Value;
            public Color OriginalColor;
        }

        private Label first;
        private Label second;
        public bool Enabled { get; private set; }
        public string Message { get; private set; }
        public event EventHandler Changed;

        public InspectionController() { Message = "Inspection off."; }

        public void BindField(Label label, FieldKind kind, string value)
        {
            label.Text = value;
            label.Tag = new Field { Kind = kind, Value = value, OriginalColor = label.BackColor };
            label.Cursor = Cursors.Hand;
            label.Click += Field_Click;
            label.Disposed += Field_Disposed;
        }

        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
            Cancel();
        }

        public void Cancel()
        {
            ClearHighlights();
            Message = Enabled ? "Choose a field in a document." : "Inspection off.";
            OnChanged();
        }

        private void Field_Click(object sender, EventArgs e)
        {
            if (!Enabled) return;
            Label label = (Label)sender;
            if (second != null) ClearHighlights();
            if (first == null)
            {
                first = label;
                Highlight(first);
                Message = "Choose the same field type in another document.";
            }
            else if (label == first)
            {
                Message = "Choose a second field, or cancel.";
            }
            else if (label.FindForm() == first.FindForm())
            {
                Message = "Choose a field in another document.";
            }
            else if (((Field)first.Tag).Kind != ((Field)label.Tag).Kind)
            {
                Message = "Different field types. Choose the same type, or cancel.";
            }
            else
            {
                second = label;
                Highlight(second);
                bool match = DailyRules.Same(((Field)first.Tag).Value, ((Field)second.Tag).Value);
                Message = (match ? "MATCH" : "MISMATCH") + "\n" + ((Field)first.Tag).Kind + " / selected values only.\nClick a field for another pair.";
            }
            OnChanged();
        }

        private void Field_Disposed(object sender, EventArgs e)
        {
            // A manually closed window cannot leave stale labels in the machine.
            if (sender == first || sender == second) Cancel();
        }

        private static void Highlight(Label label) { label.BackColor = Color.FromArgb(204, 184, 116); }

        private void ClearHighlights()
        {
            if (first != null && !first.IsDisposed) first.BackColor = ((Field)first.Tag).OriginalColor;
            if (second != null && !second.IsDisposed) second.BackColor = ((Field)second.Tag).OriginalColor;
            first = second = null;
        }

        private void OnChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }
    }
}
