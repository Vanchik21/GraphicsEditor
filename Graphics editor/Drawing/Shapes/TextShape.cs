using System.Drawing;

namespace Graphics_editor.Drawing.Shapes
{
    using Drawing;

    public class TextShape : IShape
    {
        public string Text { get; }
        public Point Location { get; }
        public FontOptions FontOptions { get; }
        public Color Color { get; }

        public TextShape(string text, Point location, FontOptions fontOptions, Color color)
        {
            Text = text;
            Location = location;
            FontOptions = fontOptions;
            Color = color;
        }

        public void Draw(Graphics g)
        {
            if (g == null || FontOptions == null || string.IsNullOrEmpty(Text)) return;

            using (var font = new Font(FontOptions.FamilyName, FontOptions.Size, FontOptions.Style))
            using (var brush = new SolidBrush(Color))
            {
                g.DrawString(Text, font, brush, Location);
            }
        }
    }
}
    
