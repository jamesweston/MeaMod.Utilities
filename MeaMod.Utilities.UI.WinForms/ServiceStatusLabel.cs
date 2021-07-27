using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MeaMod.Utilities.UI.WinForms
{
    public class ServiceStatusLabel : Control
    {
        private string _Status = "Demo";
        private Color _StatusColour = Color.Green;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Service Status")]
        [Browsable(true)]
        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Status Colour")]
        [Browsable(true)]
        public Color StatusColour
        {
            get
            {
                return _StatusColour;
            }

            set
            {
                _StatusColour = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var textMeasure = TextRenderer.MeasureText(e.Graphics, "Service Status: ", Font);
            var statusMeasure = TextRenderer.MeasureText(e.Graphics, _Status, Font);
            TextRenderer.DrawText(e.Graphics, "Service Status: ", Font, new Point(2, (int)Math.Round(ClientSize.Height / 2d - textMeasure.Height / 2d)), ForeColor);
            TextRenderer.DrawText(e.Graphics, _Status, Font, new Point(textMeasure.Width, (int)Math.Round(ClientSize.Height / 2d - statusMeasure.Height / 2d)), _StatusColour);
        }
    }
}