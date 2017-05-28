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
    public partial class animo : Form
    {
        Image[] imgs = new Image[89];
        int i = 0;
        Graphics g;
        public animo()
        {
            InitializeComponent();
            for (int i = 0; i < 89; i++)
            {
                imgs[i] = Image.FromFile("animation/" + i + ".gif");
            }
            this.Width = imgs[0].Size.Width + 16;
            this.Height = imgs[0].Size.Height + 40;
            g = CreateGraphics();
        }

        private void animo_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i < 88) i++;
            else i = 0;
            g.DrawImage(imgs[i],0,0);
        }
    }
}
