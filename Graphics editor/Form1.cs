using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Graphics_editor.Drawing;
using Graphics_editor.Drawing.Shapes;
using System.Drawing.Drawing2D;

namespace Graphics_editor
{
    public partial class Form1 : Form
    {
        bool isPressed;
        int x1, y1, x2, y2;
        public Bitmap snapshot, tempDraw;
        Color foreColor;
        int lineWidth = 2;
        int FontSize = 2;
        string selectedTool = "none";
        Pen pen;
        private DrawingService drawingService;
        private readonly List<IShape> shapes = new List<IShape>();
        private List<Point> currentStrokePoints;

        public void clear()
        {
            shapes.Clear();
            using (Graphics g = pictureBox1.CreateGraphics())
            {
                g.Clear(Color.White);
            }
            pictureBox1.Image = null;
            snapshot = new Bitmap(Math.Max(1, pictureBox1.Width), Math.Max(1, pictureBox1.Height));
            tempDraw = (Bitmap)snapshot.Clone();
        }

        public void ReloadColor(Color a)
        {
            foreColor = a;
            colorB.BackColor = foreColor;
            pen.Color = foreColor;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            tempDraw = (Bitmap)snapshot.Clone();
            using (Graphics g = Graphics.FromImage(tempDraw))
            {
                IShape preview = CreatePreviewShape();
                if (preview != null)
                {
                    drawingService.DrawShape(g, preview);
                }

                if (currentStrokePoints != null && currentStrokePoints.Count > 0 && (selectedTool == "pensil" || selectedTool == "brush" || selectedTool == "eraser"))
                {
                    var color = selectedTool == "eraser" ? pictureBox1.BackColor : foreColor;
                    var hatch = MapHatchStyle(pensilStyle.SelectedIndex);
                    var stroke = new PencilStroke(currentStrokePoints.AsReadOnly(), color, lineWidth, hatch);
                    drawingService.DrawShape(g, stroke);
                }
            }

            e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
        }

        private void tool_click(object sender, EventArgs e)
        {
            brush.Checked = false;
            line.Checked = false;
            rectangle.Checked = false;
            eraser.Checked = false;
            ellipse.Checked = false;
            none.Checked = false;
            pip.Checked = false;
            pensil.Checked = false;
            spacerectangle.Checked = false;
            spaceellipse.Checked = false;
            text.Checked = false;
            triangle1.Checked = false;
            triangle.Checked = false;
            ToolStripButton selected = sender as ToolStripButton;
            selected.Checked = true;
            selectedTool = selected.Name;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            x1 = e.X;
            y1 = e.Y;
            x2 = x1;
            y2 = y1;

            if (selectedTool == "pensil" || selectedTool == "brush" || selectedTool == "eraser")
            {
                currentStrokePoints = new List<Point> { new Point(x1, y1) };
            }
            tempDraw = (Bitmap)snapshot.Clone();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                x2 = e.X;
                y2 = e.Y;

                if (currentStrokePoints != null && (selectedTool == "pensil" || selectedTool == "brush" || selectedTool == "eraser"))
                {
                    currentStrokePoints.Add(new Point(x2, y2));
                }

                pictureBox1.Invalidate();
                pictureBox1.Update();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            x2 = e.X;
            y2 = e.Y;

            IShape finalShape = CreateFinalShape();
            if (finalShape != null)
            {
                shapes.Add(finalShape);
                RedrawSnapshot();
            }

            currentStrokePoints = null;
        }

        private void sizeCB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lineWidth = int.Parse(sizeCB.Text);
                pen = new Pen(foreColor, lineWidth);
            }
            catch { }
        }

        private void RedrawSnapshot()
        {
            snapshot = new Bitmap(Math.Max(1, pictureBox1.Width), Math.Max(1, pictureBox1.Height));
            using (Graphics g = Graphics.FromImage(snapshot))
            {
                g.Clear(pictureBox1.BackColor);
                drawingService.RedrawAll(g, shapes);
            }
            tempDraw = (Bitmap)snapshot.Clone();
            pictureBox1.Invalidate();
        }

        private IShape CreatePreviewShape()
        {
            switch (selectedTool)
            {
                case "line":
                    return new LineShape(new Point(x1, y1), new Point(x2, y2), CreateStrokeOptionsFromUI());
                case "rectangle":
                    return new RectangleShape(NormalizeRect(x1, y1, x2, y2), foreColor);
                case "spacerectangle":
                    return new RectangleShape(NormalizeRect(x1, y1, x2, y2), CreateStrokeOptionsFromUI());
                case "ellipse":
                    return new EllipseShape(NormalizeRect(x1, y1, x2, y2), foreColor);
                case "spaceellipse":
                    return new EllipseShape(NormalizeRect(x1, y1, x2, y2), CreateStrokeOptionsFromUI());
                case "triangle":
                case "triangle1":
                    var pts = new Point[] { new Point(x1, y1), new Point(x2, y2), new Point(x1, y2) };
                    if (selectedTool == "triangle")
                        return new TriangleShape(pts, foreColor);
                    return new TriangleShape(pts, CreateStrokeOptionsFromUI());
                case "text":
                    var fontOptions = new FontOptions(fontName.Text, FontSize, MapFontStyle(fontStyle.SelectedIndex));
                    return new TextShape(textToDraw.Text, new Point(x1, y1), fontOptions, foreColor);
                default:
                    return null;
            }
        }

        private IShape CreateFinalShape()
        {
            if (selectedTool == "pensil" || selectedTool == "brush" || selectedTool == "eraser")
            {
                if (currentStrokePoints == null || currentStrokePoints.Count == 0) return null;
                var color = selectedTool == "eraser" ? pictureBox1.BackColor : foreColor;
                var hatch = MapHatchStyle(pensilStyle.SelectedIndex);
                return new PencilStroke(currentStrokePoints.AsReadOnly(), color, lineWidth, hatch);
            }

            return CreatePreviewShape();
        }

        private Rectangle NormalizeRect(int ax, int ay, int bx, int by)
        {
            int x = Math.Min(ax, bx);
            int y = Math.Min(ay, by);
            int w = Math.Abs(bx - ax);
            int h = Math.Abs(by - ay);
            return new Rectangle(x, y, w, h);
        }

        private StrokeOptions CreateStrokeOptionsFromUI()
        {
            var style = MapLineStyle(choseLine.SelectedIndex);
            var startCap = MapStartCap(choseLine.SelectedIndex);
            var endCap = MapEndCap(choseLine.SelectedIndex);
            return new StrokeOptions(foreColor, lineWidth, style, startCap, endCap);
        }

        private StrokeLineStyle MapLineStyle(int index)
        {
            switch (index)
            {
                case 1: return StrokeLineStyle.Dash;
                case 2: return StrokeLineStyle.DashDot;
                case 3: return StrokeLineStyle.DashDotDot;
                case 4: return StrokeLineStyle.Dot;
                default: return StrokeLineStyle.Solid;
            }
        }

        private LineCap MapStartCap(int index)
        {
            switch (index)
            {
                case 5: return LineCap.ArrowAnchor;
                case 6: return LineCap.Round;
                case 7: return LineCap.Square;
                case 8: return LineCap.Triangle;
                default: return LineCap.Flat;
            }
        }

        private LineCap MapEndCap(int index)
        {
            switch (index)
            {
                case 5: return LineCap.DiamondAnchor;
                case 6: return LineCap.RoundAnchor;
                case 7: return LineCap.SquareAnchor;
                case 8: return LineCap.Flat;
                default: return LineCap.Flat;
            }
        }

        private HatchStyle? MapHatchStyle(int index)
        {
            switch (index)
            {
                case 0: return null;
                case 1: return HatchStyle.Cross;
                case 2: return HatchStyle.DarkDownwardDiagonal;
                case 3: return HatchStyle.DarkHorizontal;
                case 4: return HatchStyle.DarkVertical;
                case 5: return HatchStyle.DashedDownwardDiagonal;
                case 6: return HatchStyle.DashedHorizontal;
                case 7: return HatchStyle.DashedUpwardDiagonal;
                case 8: return HatchStyle.DashedVertical;
                case 9: return HatchStyle.DiagonalBrick;
                case 10: return HatchStyle.DiagonalCross;
                case 11: return HatchStyle.Divot;
                case 12: return HatchStyle.DottedGrid;
                case 13: return HatchStyle.DottedDiamond;
                case 14: return HatchStyle.Horizontal;
                case 15: return HatchStyle.LargeCheckerBoard;
                case 16: return HatchStyle.LargeConfetti;
                case 17: return HatchStyle.Plaid;
                case 18: return HatchStyle.Shingle;
                case 19: return HatchStyle.SmallGrid;
                case 20: return HatchStyle.SolidDiamond;
                case 21: return HatchStyle.Sphere;
                case 22: return HatchStyle.Trellis;
                case 23: return HatchStyle.Vertical;
                case 24: return HatchStyle.Wave;
                case 25: return HatchStyle.Weave;
                case 26: return HatchStyle.ZigZag;
                default: return null;
            }
        }

        private FontStyle MapFontStyle(int index)
        {
            switch (index)
            {
                case 1: return FontStyle.Bold;
                case 2: return FontStyle.Italic;
                case 3: return FontStyle.Underline;
                default: return FontStyle.Regular;
            }
        }

        private void newPicture_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void openPicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                clear();
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Refresh();
                pictureBox1.Update();
                snapshot = new Bitmap(pictureBox1.Image);
                tempDraw = new Bitmap(snapshot);
            }
        }

        private void savePicture_Click(object sender, EventArgs e)
        {
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);

                switch (strFilExtn)
                {
                    case "bmp": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    case "jpg": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case "png": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Png); break;
                    default: break;
                }
            }
        }

        private void colorB_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ReloadColor(colorDialog1.Color);
            }
        }

        private void clearPaint_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedTool == "pip")
            {
                Bitmap copy = new Bitmap(snapshot);
                ReloadColor(copy.GetPixel(e.X, e.Y));
            }
        }

        private void whiteC_Click(object sender, EventArgs e) => ReloadColor(Color.White);
        private void grayC_Click(object sender, EventArgs e) => ReloadColor(Color.Gray);
        private void brownC_Click(object sender, EventArgs e) => ReloadColor(Color.Brown);
        private void redC_Click(object sender, EventArgs e) => ReloadColor(Color.Red);
        private void orangeC_Click(object sender, EventArgs e) => ReloadColor(Color.Orange);
        private void yellowC_Click(object sender, EventArgs e) => ReloadColor(Color.Yellow);
        private void greenC_Click(object sender, EventArgs e) => ReloadColor(Color.Green);
        private void aquaC_Click(object sender, EventArgs e) => ReloadColor(Color.Aqua);
        private void blackC_Click(object sender, EventArgs e) => ReloadColor(Color.Black);

        private void Form1_Load(object sender, EventArgs e)
        {
            FontFamily[] family = FontFamily.Families;
            foreach (FontFamily font in family)
            {
                fontName.Items.Add(font.GetName(1).ToString());
            }

            pictureBox1.AllowDrop = true;
         
            if (snapshot == null)
            {
                snapshot = new Bitmap(Math.Max(1, pictureBox1.Width), Math.Max(1, pictureBox1.Height));
                tempDraw = (Bitmap)snapshot.Clone();
            }
        }

        private void fontSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FontSize = int.Parse(fontSize.Text);
            }
            catch { }
        }

        private void ButtonBackgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = c.Color;
                ButtonBackgroundColor.BackColor = c.Color;
                RedrawSnapshot();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Width < 2000 || pictureBox1.Height < 2000 && pictureBox1.Width > 100 || pictureBox1.Height > 100)
            {
                pictureBox1.Width += 100;
                pictureBox1.Height += 100;
            }
            RedrawSnapshot();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Width < 2000 || pictureBox1.Height < 2000 && pictureBox1.Width > 100 || pictureBox1.Height > 100)
            {
                pictureBox1.Width -= 100;
                pictureBox1.Height -= 100;
            }
            RedrawSnapshot();
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
            }
            snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tempDraw = (Bitmap)snapshot.Clone();
            shapes.Clear();
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void filtresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Виконав: Захаров І.А., ІПЗ-22-1");
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void blueC_Click(object sender, EventArgs e) => ReloadColor(Color.Blue);
        private void fuchsiaC_Click(object sender, EventArgs e) => ReloadColor(Color.Fuchsia);

        public Form1()
        {
            InitializeComponent();
            drawingService = new DrawingService();
            snapshot = new Bitmap(Math.Max(1, pictureBox1.ClientRectangle.Width), Math.Max(1, pictureBox1.ClientRectangle.Height));
            tempDraw = (Bitmap)snapshot.Clone();
            foreColor = Color.Black;
            pen = new Pen(foreColor, lineWidth);
            none.Checked = true;
            sizeCB.SelectedIndex = 9;
            choseLine.SelectedIndex = 0;
            pensilStyle.SelectedIndex = 0;
            fontSize.SelectedIndex = 13;
            fontStyle.SelectedIndex = 0;
        }
    }
}