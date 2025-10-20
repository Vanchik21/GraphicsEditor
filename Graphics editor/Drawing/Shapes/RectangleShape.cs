using System.Drawing;

namespace Graphics_editor.Drawing.Shapes
{
    public class RectangleShape : IShape
    {
        public Rectangle Rect { get; }
        public Color FillColor { get; }
        public StrokeOptions Stroke { get; }
        public bool Filled { get; }

        public RectangleShape(Rectangle rect, Color fillColor)
        {
            Rect = rect;
            FillColor = fillColor;
            Filled = true;
        }

        public RectangleShape(Rectangle rect, StrokeOptions stroke)
        {
            Rect = rect;
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
                    g.FillRectangle(brush, Rect);
                }
            }
            else if (Stroke != null)
            {
                using (var pen = PenFactory.Create(Stroke))
                {
                    g.DrawRectangle(pen, Rect);
                }
            }
        }
    }
}
