using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach.Forms
{
    public partial class MassInfo : Form
    {
        int count = 0;
        public MassInfo()
        {
            InitializeComponent();
        }

        public void getInfo(Bitmap image, int x, int y)
        {
            askDiapazon ab = new askDiapazon();
            count = ab.ask();
            bool flag = ab.isGray;

            Point p = new Point((x - ((count - 1) / 2)), (y - ((count - 1) / 2)));
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {

                    Color color = Color.Transparent;
                    if (p.X > 0 && p.Y > 0 && p.X < image.Width && p.Y < image.Width) color = image.GetPixel(p.X, p.Y);
                    Panel pn = new Panel();
                    Label temp1 = new Label();
                    if (!flag) temp1.Text = ""+color.R;
                    temp1.Location = new Point (10,0);
                    temp1.Width = 40;
                    temp1.Height = 15;
                    pn.Controls.Add(temp1);
                    Label temp2 = new Label();
                    if (!flag) temp2.Text = ""+color.G;
                    else temp2.Text = "" + (((color.R * 0.3) + (color.G * 0.59) + (color.B * 0.11)));
                    temp2.Location = new Point(10, 15);
                    temp2.Width = 40;
                    temp2.Height = 15;
                    pn.Controls.Add(temp2);
                    Label temp3 = new Label();
                    if (!flag) temp3.Text = ""+color.B;
                    temp3.Location = new Point(10,30);
                    temp3.Width = 40;
                    temp3.Height = 15;
                    pn.BackColor = color;
                    pn.Controls.Add(temp3);

                    if (((color.R+color.G+color.B) < 350) && color.A > 120)
                    {
                        temp1.ForeColor = Color.White;
                        temp2.ForeColor = Color.White;
                        temp3.ForeColor = Color.White;
                    }

                    pn.Width = 45;
                    pn.Height = 45;

                    pn.Location = new Point(50 * i, 50 * j);
                    this.Controls.Add(pn);
                    p.Y++;
                    //MessageBox.Show("1");
                }
                p.Y = y - (count - 1) / 2;
                p.X++;
            }
            this.Width = (count*50) + 16;
            this.Height = (count * 50) + 40;
            this.ShowDialog();
        }

        private void MassInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
