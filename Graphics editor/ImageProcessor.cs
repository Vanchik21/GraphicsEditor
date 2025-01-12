using System;
using System.Drawing;

namespace Graphics_editor
{

    public class ImageProcessor
    {
        public Bitmap ApplySepia(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Sepia);
        public Bitmap ApplyArtistic(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Artistic);
        public Bitmap ApplyGrayscale(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Grayscale);
        public Bitmap ApplySpike(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Spike);
        public Bitmap ApplyFlash(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Flash);
        public Bitmap ApplyFrozen(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Frozen);
        public Bitmap ApplySuji(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Suji);
        public Bitmap ApplyDramatic(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Dramatic);
        public Bitmap ApplyKakao(Image image) => ApplyColorFilter(image, ColorMatrixPresets.Kakao);

        public Bitmap ApplyRGBChange(int red, int green, int blue, Bitmap image)
        {
            Bitmap newImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color originalColor = image.GetPixel(x, y);
                    int newRed = Clamp(originalColor.R + red, 0, 255);
                    int newGreen = Clamp(originalColor.G + green, 0, 255);
                    int newBlue = Clamp(originalColor.B + blue, 0, 255);

                    Color newColor = Color.FromArgb(newRed, newGreen, newBlue);
                    newImage.SetPixel(x, y, newColor);
                }
            }

            return newImage;
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        private Bitmap ApplyColorFilter(Image image, float[][] colorMatrixPreset)
        {
            return new Bitmap(image);
        }
    }
}
