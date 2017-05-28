using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursach.Classes;

namespace Kursach.Forms
{
    public partial class NewImageForm : Form
    {
        private bool haveResult = false;
        private Color color = Color.Black;
        private Size size = new Size(0,0);

        public NewImageForm()
        {
            InitializeComponent();
            widthPicker.Maximum = 4000;
            heightPicker.Maximum = 3000;
        }

        public Bitmap create()
        {
            haveResult = false;
            
            this.ShowDialog();

            if (haveResult)
            {
                size.Width = (int)widthPicker.Value;
                size.Height = (int)heightPicker.Value;
                Bitmap temp = new Bitmap(size.Width, size.Height);

                if (checkBox1.Checked)
                {
                    SolidBrush myBrush = new SolidBrush(colorPicker.BackColor);
                    Graphics g = Graphics.FromImage(temp);
                    g.FillRectangle(myBrush, new Rectangle(0, 0, size.Width, size.Height));
                }
                              
                return temp;
            }
            else
            {
                ExitWithoutSaveException ex = new ExitWithoutSaveException("Форма завершилась без сохранения параметров!");
                throw ex;
            }
            
            
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            haveResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            haveResult = false;
            this.Close();
        }

        private void colorPicker_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            colorPicker.BackColor = color;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            colorPicker.Enabled = !colorPicker.Enabled;
            label3.Enabled = !label3.Enabled;
        }
    }
}
