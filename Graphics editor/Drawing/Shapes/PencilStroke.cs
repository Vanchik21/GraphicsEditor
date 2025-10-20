using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphics_editor.Drawing.Shapes
{
    using Drawing;

    public class PencilStroke : IShape
    {
        public IReadOnlyList<Point> Points { get; }
        public int Thickness { get; }
        public Color Color { get; }
        public HatchStyle? Hatch { get; } 

        public PencilStroke(IReadOnlyList<Point> points, Color color, int thickness = 1, HatchStyle? hatch = null)
        {
            Points = points;
            Color = color;
            Thickness = thickness;
            Hatch = hatch;
        }

        public void Draw(Graphics g)
        {
            if (g == null || Points == null || Points.Count == 0) return;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (Hatch.HasValue)
            {
                using (var hb = new HatchBrush(Hatch.Value, Color, Color.White))
                {
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var p = Points[i];
                        g.FillEllipse(hb, p.X - Thickness / 2, p.Y - Thickness / 2, Thickness, Thickness);
                    }
                }
            }
            else
            {
                using (var brush = new SolidBrush(Color))
                {
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var p = Points[i];
                        g.FillEllipse(brush, p.X - Thickness / 2, p.Y - Thickness / 2, Thickness, Thickness);
                    }
                }
            }
        }
    }
}
