using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

public class FileManager
{
    public Image OpenFile(OpenFileDialog openFileDialog)
    {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            return Image.FromFile(openFileDialog.FileName);
        }
        return null;
    }

    public void SaveFile(Image image)
    {
        SaveFileDialog sfd = new SaveFileDialog { Filter = "Images|*.png;*.bmp;*.jpg" };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            var format = GetImageFormat(Path.GetExtension(sfd.FileName));
            image.Save(sfd.FileName, format);
        }
    }

    public Image ReloadFile(string fileName)
    {
        return Image.FromFile(fileName);
    }

    private ImageFormat GetImageFormat(string extension)
    {
        switch (extension.ToLower())
        {
            case ".jpg":
                return ImageFormat.Jpeg;
            case ".bmp":
                return ImageFormat.Bmp;
            default:
                return ImageFormat.Png;
        }
    }
}
