using System;
using System.Drawing;
using System.Windows.Forms;

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

        public void clear()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            pictureBox1.Image = null;
            snapshot = new Bitmap(snapshot.Width, snapshot.Height);
            tempDraw = (Bitmap)snapshot.Clone();
            g.Dispose();
        }
        public void ReloadColor(Color a)
        {
            foreColor = a;
            colorB.BackColor = foreColor;
            pen.Color = foreColor;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (selectedTool != "brush" && selectedTool != "eraser" && selectedTool != "pensil")
                tempDraw = (Bitmap)snapshot.Clone();
            Graphics g = Graphics.FromImage(tempDraw);
            switch (selectedTool)
            {
                case "line":
                    drawingService.DrawLine(g, foreColor, tempDraw, lineWidth, choseLine, pen, x1, y1, x2, y2);
                    break;
                case "rectangle":
                    drawingService.DrawRectangle(g, foreColor, tempDraw, x1, y1, x2, y2);
                    break;
                case "brush":
                    drawingService.DrawBrush(g, tempDraw, pen, x1, y1, x2, y2);
                    break;
                case "eraser":
                    drawingService.Erase(g, tempDraw, pictureBox1, lineWidth, x1, y1, x2, y2);
                    break;
                case "ellipse":
                    drawingService.DrawElipse(g, foreColor, x1, y1, x2, y2);
                    break;
                case "pensil":
                    drawingService.DrawPensil(g, foreColor, pensilStyle, lineWidth, x1, y1, x2, y2);
                    break;
                case "spacerectangle":
                    drawingService.DrawSpaceRectangle(g, tempDraw, pen, x1, y1, x2, y2);
                    break;
                case "spaceellipse":
                    drawingService.DrawSpaceEllipse(g, pen, x1, y1, x2, y2);
                    break;
                case "text":
                    drawingService.DrawText(g, fontStyle, fontName, FontSize, foreColor, textToDraw, x1, y1);
                    break;
                case "triangle1":
                    drawingService.DrawTriangle1(g, tempDraw, pen, x1, y1, x2, y2);
                    break;
                case "triangle":
                    drawingService.DrawTriangle(g, tempDraw, foreColor, x1, y1, x2, y2);
                    break;
            }
            g.Dispose();
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
            tempDraw = (Bitmap)snapshot.Clone();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                x2 = e.X;
                y2 = e.Y;
                pictureBox1.Invalidate();
                pictureBox1.Update();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            snapshot = (Bitmap)tempDraw.Clone();
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

        private void whiteC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.White);
        }

        private void grayC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Gray);
        }

        private void brownC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Brown);
        }

        private void redC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Red);
        }

        private void orangeC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Orange);
        }

        private void yellowC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Yellow);
        }

        private void greenC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Green);
        }

        private void aquaC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Aqua);
        }

        private void blackC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Black);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FontFamily[] family = FontFamily.Families;
            foreach (FontFamily font in family)
            {
                fontName.Items.Add(font.GetName(1).ToString());
            }

            pictureBox1.AllowDrop = true;
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
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Width < 2000 || pictureBox1.Height < 2000 && pictureBox1.Width > 100 || pictureBox1.Height > 100)
            {
                pictureBox1.Width += 100;
                pictureBox1.Height += 100;
            }
            snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tempDraw = (Bitmap)snapshot.Clone();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Width < 2000 || pictureBox1.Height < 2000 && pictureBox1.Width > 100 || pictureBox1.Height > 100)
            {
                pictureBox1.Width -= 100;
                pictureBox1.Height -= 100;
            }
            snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tempDraw = (Bitmap)snapshot.Clone();
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if(data != null)
            {
                var fileNames = data as string[];
                if (fileNames.Length > 0)
                    pictureBox1.Image = Image.FromFile(fileNames[0]);
            }
            snapshot = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tempDraw = (Bitmap)snapshot.Clone();
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

        private void blueC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Blue);
        }

        private void fuchsiaC_Click(object sender, EventArgs e)
        {
            ReloadColor(Color.Fuchsia);
        }

        public Form1()
        {
            InitializeComponent();
            drawingService = new DrawingService();
            snapshot = new Bitmap(pictureBox1.ClientRectangle.Width, pictureBox1.ClientRectangle.Height);
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
