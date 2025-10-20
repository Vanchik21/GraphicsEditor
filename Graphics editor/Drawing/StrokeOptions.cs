using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphics_editor.Drawing
{
    public enum StrokeLineStyle { Solid, Dash, DashDot, DashDotDot, Dot
    }

    public class StrokeOptions
    {
        public Color Color { get; }
        public float Width { get; }
        public StrokeLineStyle LineStyle { get; }
        public LineCap StartCap { get; }
        public LineCap EndCap { get; }

        public StrokeOptions(Color color, float width,
            StrokeLineStyle lineStyle = StrokeLineStyle.Solid,
            LineCap startCap = LineCap.Flat,
            LineCap endCap = LineCap.Flat)
        {
            Color = color;
            Width = width;
            LineStyle = lineStyle;
            StartCap = startCap;
            EndCap = endCap;
        }
    }
}
