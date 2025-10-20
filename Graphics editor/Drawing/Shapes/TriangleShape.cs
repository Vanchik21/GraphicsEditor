using System.Drawing;

namespace Graphics_editor.Drawing.Shapes
{
    using Drawing;
    public class TriangleShape : IShape
    {
        public Point[] Points { get; }
        public Color? FillColor { get; }
        public StrokeOptions Stroke { get; }

        // Filled triangle
        public TriangleShape(Point[] points, Color fillColor)
        {
            Points = (Point[])points.Clone();
            FillColor = fillColor;
        }

        // Stroked triangle
        public TriangleShape(Point[] points, StrokeOptions stroke)
        {
            Points = (Point[])points.Clone();
            Stroke = stroke;
        }

        public void Draw(Graphics g)
        {
            if (g == null || Points == null || Points.Length < 3) return;

            if (FillColor.HasValue)
            {
                using (var brush = new SolidBrush(FillColor.Value))
                {
                    g.FillPolygon(brush, Points);
                }
            }
            else if (Stroke != null)
            {
                using (var pen = PenFactory.Create(Stroke))
                {
                    g.DrawPolygon(pen, Points);
                }
            }
        }
    }
}
