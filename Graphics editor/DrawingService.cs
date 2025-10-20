using System.Collections.Generic;
using System.Drawing;
using Graphics_editor.Drawing;

namespace Graphics_editor
{
    public class DrawingService
    {
        public void DrawShape(Graphics g, IShape shape)
        {
            if (g == null || shape == null) return;
            shape.Draw(g);
        }

        public void RedrawAll(Graphics g, IEnumerable<IShape> shapes)
        {
            if (g == null || shapes == null) return;
            foreach (var s in shapes)
            {
                s.Draw(g);
            }
        }
    }
}
