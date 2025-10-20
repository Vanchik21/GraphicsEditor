using System.Drawing;

namespace Graphics_editor.Drawing.Shapes
{
    using Drawing;

    public class EllipseShape : IShape
    {
        public Rectangle Bounds { get; }
        public Color FillColor { get; }
        public StrokeOptions Stroke { get; }
        public bool Filled { get; }

        public EllipseShape(Rectangle bounds, Color fillColor)
        {
            Bounds = bounds;
            FillColor = fillColor;
            Filled = true;
        }

        public EllipseShape(Rectangle bounds, StrokeOptions stroke)
        {
            Bounds = bounds;
            Stroke = stroke;
            Filled = false;
        }

        public void Draw(Graphics g)
        {
            if (g == null) return;
            if (Filled)
            {
                using (var brush = new SolidBrush(FillColor))
                {
                    g.FillEllipse(brush, Bounds);
                }
            }
            else if (Stroke != null)
            {
                using (var pen = PenFactory.Create(Stroke))
                {
                    g.DrawEllipse(pen, Bounds);
                }
            }
        }
    }
}
