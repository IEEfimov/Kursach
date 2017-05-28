using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach.Forms
{
    public partial class ImgInfo : Form
    {
        public ImgInfo()
        {
            InitializeComponent();
        }

        public void showInfo(string path)
        {
            Image img = Image.FromFile(path);

            int sizeX = img.Size.Width;
            int sizeY = img.Size.Height;

            NameLabel.Text = shortName(path);
            PathLabel.Text = path;

            double HResulution = img.HorizontalResolution / 2.54;
            double VResulution = img.VerticalResolution / 2.54;

            double SizeH = sizeX / HResulution;
            double SizeV = sizeY / VResulution;

            if (img.RawFormat.Equals(ImageFormat.Jpeg)) FormatLabel.Text = "Joint Photographic Experts Group (JPEG)";
            else if (img.RawFormat.Equals(ImageFormat.Bmp)) FormatLabel.Text = "Bitmap Picture (BMP)";
            else if (img.RawFormat.Equals(ImageFormat.Emf)) FormatLabel.Text = "Enhanced Windows Metafile (EMF)";
            else if (img.RawFormat.Equals(ImageFormat.Exif)) FormatLabel.Text = "Exchangeable Image File Format (EXIF)";
            else if (img.RawFormat.Equals(ImageFormat.Gif)) FormatLabel.Text = "Graphics Interchange Format (GIF)";
            else if (img.RawFormat.Equals(ImageFormat.Icon)) FormatLabel.Text = "Windows icon (ICO)";
            else if (img.RawFormat.Equals(ImageFormat.Png)) FormatLabel.Text = "Portable Network Graphics (PNG)";
            else if (img.RawFormat.Equals(ImageFormat.Tiff)) FormatLabel.Text = "Tagged Image File Format (TIFF)";
            else if (img.RawFormat.Equals(ImageFormat.Wmf)) FormatLabel.Text = "Windows MetaFile (WMF)";
            else if (img.RawFormat.Equals(ImageFormat.MemoryBmp)) FormatLabel.Text = "Memory BMP";
            else FormatLabel.Text = img.RawFormat.Guid.ToString();

            if (img.PixelFormat.Equals(PixelFormat.Format16bppGrayScale)) PixelFormatLabel.Text = "Оттенки серого";
            else PixelFormatLabel.Text = img.PixelFormat.ToString();



            SizeLabel.Text = img.Size.Width.ToString() + "x" + img.Size.Height.ToString();
            SizeSMLabel.Text = (Math.Round(HResulution,2).ToString() + 
                "x" + Math.Round(VResulution, 2).ToString());

            PhysicSizeLabel.Text = (Math.Round(SizeH, 2).ToString() + 
                  "x" + Math.Round(SizeV, 2).ToString());


            this.ShowDialog();
        }

        private void ImgInfo_Load(object sender, EventArgs e)
        {

        }

        private string shortName(string longStr)
        {
            string temp = "";
            for (int i = 0; i < longStr.Length; i++)
            {
                if (longStr[i] != '\\') temp += longStr[i];
                else temp = "";
            }
            return temp;
        }

        private void FormatLabel_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(FormatLabel.Text);
        }
    }
}
