using System.Drawing;

namespace Graphics_editor.Drawing.Shapes
{
    using Drawing;

    public class LineShape : IShape
    {
        public Point Start { get; }
        public Point End { get; }
        public StrokeOptions Stroke { get; }

        public LineShape(Point start, Point end, StrokeOptions stroke)
        {
            Start = start;
            End = end;
            Stroke = stroke;
        }

        public void Draw(Graphics g)
        {
            if (g == null || Stroke == null) return;
            using (var pen = PenFactory.Create(Stroke))
            {
                g.DrawLine(pen, Start, End);
            }
        }
    }
}
