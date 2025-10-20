using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphics_editor.Drawing
{
    public static class PenFactory
    {
        public static Pen Create(StrokeOptions options)
        {
            var pen = new Pen(options.Color, options.Width);
            
            switch (options.LineStyle)
            {
                case StrokeLineStyle.Dash:
                    pen.DashStyle = DashStyle.Dash;
                    break;
                case StrokeLineStyle.DashDot:
                    pen.DashStyle = DashStyle.DashDot;
                    break;
                case StrokeLineStyle.DashDotDot:
                    pen.DashStyle = DashStyle.DashDotDot;
                    break;
                case StrokeLineStyle.Dot:
                    pen.DashStyle = DashStyle.Dot;
                    break;
                case StrokeLineStyle.Solid:
                default:
                    pen.DashStyle = DashStyle.Solid;
                    break;
            }

            pen.StartCap = options.StartCap;
            pen.EndCap = options.EndCap;
            return pen;
        }
    }
}
