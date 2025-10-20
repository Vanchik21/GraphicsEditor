using System.Drawing;

namespace Graphics_editor.Drawing
{
    public class FontOptions
    {
        public string FamilyName { get; }
        public float Size { get; }
        public FontStyle Style { get; }

        public FontOptions(string familyName, float size, FontStyle style = FontStyle.Regular)
        {
            FamilyName = familyName;
            Size = size;
            Style = style;
        }
    }
}
