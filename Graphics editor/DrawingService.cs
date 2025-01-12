using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Windows.Forms;

namespace Graphics_editor
{
    public class DrawingService
    {
        public void DrawLine(Graphics g, Color foreColor, Bitmap tempDraw, int lineWidth, ToolStripComboBox choseLine, Pen pen, int x1, int y1, int x2, int y2)
        {
            if (tempDraw is null) return;

            Pen myPen = new Pen(foreColor, lineWidth);
            switch (choseLine.SelectedIndex)
            {
                case 0:
                    g.DrawLine(pen, x1, y1, x2, y2);
                    return;

                case 1:
                    myPen.DashStyle = DashStyle.Dash;
                    break;

                case 2:
                    myPen.DashStyle = DashStyle.DashDot;
                    break;

                case 3:
                    myPen.DashStyle = DashStyle.DashDotDot;
                    break;

                case 4:
                    myPen.DashStyle = DashStyle.Dot;
                    break;

                case 5:
                    myPen.StartCap = LineCap.ArrowAnchor;
                    myPen.EndCap = LineCap.DiamondAnchor;
                    break;

                case 6:
                    myPen.StartCap = LineCap.Round;
                    myPen.EndCap = LineCap.RoundAnchor;
                    break;

                case 7:
                    myPen.StartCap = LineCap.Square;
                    myPen.EndCap = LineCap.SquareAnchor;
                    break;

                case 8:
                    myPen.StartCap = LineCap.Triangle;
                    myPen.EndCap = LineCap.Flat;
                    break;

                default:
                    break;
            }
            g.DrawLine(myPen, x1, y1, x2, y2);
        }

        public void DrawRectangle(Graphics g, Color foreColor, Bitmap tempDraw, int x1, int y1, int x2, int y2)
        {
            if (tempDraw is null) return;

            if (x2 < x1 && y2 < y1)
            {
                g.FillRectangle(new SolidBrush(foreColor), x2, y2, Math.Abs(x2 - x1), Math.Abs(y2 - y1));
            }
            else if (x2 < x1 && y2 > y1)
            {
                g.FillRectangle(new SolidBrush(foreColor), x2, y1, Math.Abs(x2 - x1), y2 - y1);
            }
            else if (x2 > x1 && y2 < y1)
            {
                g.FillRectangle(new SolidBrush(foreColor), x1, y2, x2 - x1, Math.Abs(y2 - y1));
            }
            else
                g.FillRectangle(new SolidBrush(foreColor), x1, y1, x2 - x1, y2 - y1);
        }

        public void DrawElipse(Graphics g, Color foreColor, int x1, int y1, int x2, int y2)
        {
            g.FillEllipse(new SolidBrush(foreColor), x1, y1, x2 - x1, y2 - y1);
        }

        public void DrawSpaceEllipse(Graphics g, Pen pen, int x1, int y1, int x2, int y2)
        {
            g.DrawEllipse(pen, x1, y1, x2 - x1, y2 - y1);
        }

        public void DrawTriangle1(Graphics g, Bitmap tempDraw, Pen pen, int x1, int y1, int x2, int y2)
        {
            if (tempDraw != null)
            {
                Point[] myPoints = { new Point(x1,y1), new Point(x2,y2), new Point(x1,y2) };
                g.DrawPolygon(pen, myPoints);
            }
        }

        public void DrawTriangle(Graphics g, Bitmap tempDraw, Color foreColor, int x1, int y1, int x2, int y2)
        {
            if (tempDraw != null)
            {
                Point[] myPoints = { new Point(x1,y1), new Point(x2,y2), new Point(x1,y2) };
                g.FillPolygon(new SolidBrush(foreColor), myPoints);
            }
        }

        public void DrawPensil(Graphics g, Color foreColor, ToolStripComboBox pensilStyle, int lineWidth, int x1, int y1, int x2, int y2)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            HatchBrush hb = null;

            switch (pensilStyle.SelectedIndex)
            {
                case 0:
                    g.FillEllipse(new SolidBrush(foreColor), x1, y1, lineWidth, lineWidth);
                    x1 = x2;
                    y1 = y2;
                    return;

                case 1:
                    hb = new HatchBrush(HatchStyle.Cross, foreColor, Color.White);
                    break;

                case 2:
                    hb = new HatchBrush(HatchStyle.DarkDownwardDiagonal, foreColor, Color.White);
                    break;

                case 3:
                    hb = new HatchBrush(HatchStyle.DarkHorizontal, foreColor, Color.White);
                    break;

                case 4:
                    hb = new HatchBrush(HatchStyle.DarkVertical, foreColor, Color.White);
                    break;

                case 5:
                    hb = new HatchBrush(HatchStyle.DashedDownwardDiagonal, foreColor, Color.White);
                    break;

                case 6:
                    hb = new HatchBrush(HatchStyle.DashedHorizontal, foreColor, Color.White);
                    break;

                case 7:
                    hb = new HatchBrush(HatchStyle.DashedUpwardDiagonal, foreColor, Color.White);
                    break;

                case 8:
                    hb = new HatchBrush(HatchStyle.DashedVertical, foreColor, Color.White);
                    break;

                case 9:
                    hb = new HatchBrush(HatchStyle.DiagonalBrick, foreColor, Color.White);
                    break;

                case 10:
                    hb = new HatchBrush(HatchStyle.DiagonalCross, foreColor, Color.White);
                    break;

                case 11:
                    hb = new HatchBrush(HatchStyle.Divot, foreColor, Color.White);
                    break;

                case 12:
                    hb = new HatchBrush(HatchStyle.DottedGrid, foreColor, Color.White);
                    break;

                case 13:
                    hb = new HatchBrush(HatchStyle.DottedDiamond, foreColor, Color.White);
                    break;

                case 14:
                    hb = new HatchBrush(HatchStyle.Horizontal, foreColor, Color.White);
                    break;

                case 15:
                    hb = new HatchBrush(HatchStyle.LargeCheckerBoard, foreColor, Color.White);
                    break;

                case 16:
                    hb = new HatchBrush(HatchStyle.LargeConfetti, foreColor, Color.White);
                    break;

                case 17:
                    hb = new HatchBrush(HatchStyle.Plaid, foreColor, Color.White);
                    break;

                case 18:
                    hb = new HatchBrush(HatchStyle.Shingle, foreColor, Color.White);
                    break;

                case 19:
                    hb = new HatchBrush(HatchStyle.SmallGrid, foreColor, Color.White);
                    break;

                case 20:
                    hb = new HatchBrush(HatchStyle.SolidDiamond, foreColor, Color.White);
                    break;

                case 21:
                    hb = new HatchBrush(HatchStyle.Sphere, foreColor, Color.White);
                    break;

                case 22:
                    hb = new HatchBrush(HatchStyle.Trellis, foreColor, Color.White);
                    break;

                case 23:
                    hb = new HatchBrush(HatchStyle.Vertical, foreColor, Color.White);
                    break;

                case 24:
                    hb = new HatchBrush(HatchStyle.Wave, foreColor, Color.White);
                    break;

                case 25:
                    hb = new HatchBrush(HatchStyle.Weave, foreColor, Color.White);
                    break;

                case 26:
                    hb = new HatchBrush(HatchStyle.ZigZag, foreColor, Color.White);
                    break;
            }

            if (hb != null)
            {
                g.FillEllipse(hb, x1, y1, lineWidth, lineWidth);
                x1 = x2;
                y1 = y2;
            }

        }

        public void DrawSpaceRectangle(Graphics g, Bitmap tempDraw, Pen pen, int x1, int y1, int x2, int y2)
        {
            if (tempDraw != null) return;

            if (x2 < x1 && y2 < y1)
            {
                g.DrawRectangle(pen, x2, y2, Math.Abs(x2 - x1), Math.Abs(y2 - y1));
            }
            else if (x2 < x1 && y2 > y1)
            {
                g.DrawRectangle(pen, x2, y1, Math.Abs(x2 - x1), y2 - y1);
            }
            else if (x2 > x1 && y2 < y1)
            {
                g.DrawRectangle(pen, x1, y2, x2 - x1, Math.Abs(y2 - y1));
            }
            else
                g.DrawRectangle(pen, x1, y1, x2 - x1, y2 - y1);
        }

        public void DrawText(Graphics g, ToolStripComboBox fontStyle, ToolStripComboBox fontName, int fontSize, Color foreColor, ToolStripTextBox textToDraw, int x1, int y1)
        {
            Font f1 = null;
            switch (fontStyle.SelectedIndex)
            {
                case 0:
                    f1 = new Font(fontName.Text, fontSize, FontStyle.Regular);
                    break;

                case 1:
                    f1 = new Font(fontName.Text, fontSize, FontStyle.Bold);
                    break;

                case 2:
                    f1 = new Font(fontName.Text, fontSize, FontStyle.Italic);
                    break;

                case 3:
                    f1 = new Font(fontName.Text, fontSize, FontStyle.Underline);
                    break;

                default:
                    break;
            }

            if (f1 != null)
            {
                g.DrawString(textToDraw.Text, f1, new SolidBrush(foreColor), x1, y1);
            }
        }

        public void DrawBrush(Graphics g, Bitmap tempDraw, Pen pen, int x1, int y1, int x2, int y2)
        {
            if (tempDraw != null)
                g.DrawLine(pen, x1, y1, x2, y2);
            x1 = x2;
            y1 = y2;
        }

        public void Erase(Graphics g, Bitmap tempDraw, PictureBox pictureBox1, int lineWidth, int x1, int y1, int x2, int y2)
        {
            if (tempDraw != null)
                g.FillEllipse(new SolidBrush(pictureBox1.BackColor), x1, y1, lineWidth, lineWidth);
            x1 = x2;
            y1 = y2;
        }
    }
}
