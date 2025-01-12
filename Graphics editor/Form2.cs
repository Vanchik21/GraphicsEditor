using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graphics_editor
{
    public partial class Form2 : Form
    {
        private readonly ImageProcessor _imageProcessor;
        private readonly FileManager _fileManager;
        private readonly NotificationService _notificationService;
        private bool _opened;

        public Form2()
        {
            InitializeComponent();

            _imageProcessor = new ImageProcessor();
            _fileManager = new FileManager();
            _notificationService = new NotificationService();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void reload()
        {
            if (!_opened)
            {
                _notificationService.ShowError("Відкрийте зображення, а потім застосуйте зміни");
                return;
            }

            var file = _fileManager.ReloadFile(openFileDialog1.FileName);
            if (file != null)
            {
                pictureBox1.Image = file;
                _opened = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = _fileManager.OpenFile(openFileDialog1);
            if (file != null)
            {
                pictureBox1.Image = file;
                _opened = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_opened)
            {
                _notificationService.ShowError("Зображення не завантажено, спочатку завантажте зображення");
                return;
            }

            _fileManager.SaveFile(pictureBox1.Image);
        }

        private void ApplyFilter(Bitmap filteredImage)
        {
            if (!_opened)
            {
                _notificationService.ShowError("Відкрийте зображення, а потім застосуйте зміни");
                return;
            }

            pictureBox1.Image = filteredImage;
        }

        private void redbar_ValueChanged(object sender, EventArgs e)
        {
            var filteredImage = _imageProcessor.ApplyRGBChange(redbar.Value, greenbar.Value, bluebar.Value, (Bitmap)pictureBox1.Image);
            ApplyFilter(filteredImage);
        }

        private void greenbar_ValueChanged(object sender, EventArgs e)
        {
            var filteredImage = _imageProcessor.ApplyRGBChange(redbar.Value, greenbar.Value, bluebar.Value, (Bitmap)pictureBox1.Image);
            ApplyFilter(filteredImage);
        }

        private void bluebar_ValueChanged(object sender, EventArgs e)
        {
            var filteredImage = _imageProcessor.ApplyRGBChange(redbar.Value, greenbar.Value, bluebar.Value, (Bitmap)pictureBox1.Image);
            ApplyFilter(filteredImage);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e) => reload();
        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplySepia((Bitmap)pictureBox1.Image));
        private void artisticToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplyArtistic((Bitmap)pictureBox1.Image));
        private void grayToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplyGrayscale((Bitmap)pictureBox1.Image));
        private void spikeToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplySpike((Bitmap)pictureBox1.Image));
        private void flashToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplyFlash((Bitmap)pictureBox1.Image));
        private void frozenToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplyFrozen((Bitmap)pictureBox1.Image));
        private void sujiToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplySuji((Bitmap)pictureBox1.Image));
        private void dramaticToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplyDramatic((Bitmap)pictureBox1.Image));
        private void kakaoToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilter(_imageProcessor.ApplyKakao((Bitmap)pictureBox1.Image));
    }
}
