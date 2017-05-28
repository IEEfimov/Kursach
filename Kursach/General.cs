using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursach.Forms;
using Kursach.Classes;

namespace Kursach
{

    public partial class General : Form
    {
        Image[] imgs = new Image[89];
        int i = 0;
        Graphics g;

        List<ChildForm> list = new List<ChildForm>();
        ChildForm selected;
        Tools tool = new Tools();
        Color color;

        public int Tool
        {
            set { tool.Value = value; }
            get { return tool.Value; }
        }

        public Color Color
        {
            set { color = value; }
            get { return color; }
        }

        public General()
        {
            InitializeComponent();
            Tool = Tools.PEN;
            panel2.Width = 360;
            panel2.Height = 180;
            panel2.Location = new Point(this.Size.Width / 2 - 180, this.Size.Height / 2 - 180);
            g = panel2.CreateGraphics() ;
            for (int i = 0; i < 89; i++)
            {
                imgs[i] = Image.FromFile("animo/frame" + i + ".png");
            }
            g.DrawImage(imgs[0], 0, 0, 90, 90);
        }

        public void setSelected(ChildForm selected)
        {
            this.selected = selected;
            if (selected == null)
            {
                сохранитьToolStripMenuItem.Enabled = false;
                сохранитьКакToolStripMenuItem.Enabled = false;
                закрытьToolStripMenuItem.Enabled = false;
            }else
            {
                сохранитьToolStripMenuItem.Enabled = true;
                сохранитьКакToolStripMenuItem.Enabled = true;
                закрытьToolStripMenuItem.Enabled = true;
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewImageForm newImage = new NewImageForm();
                Image temp = newImage.create();
                ChildForm child = new ChildForm(this, temp);
                child.Show();
            }
            catch (ExitWithoutSaveException)
            {

            }
            
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                String temp = openFileDialog1.FileName;
                if (temp.Equals("openFileDialog1")) return;
                ChildForm child = new ChildForm(this, temp);
                child.Show();
                openFileDialog1.FileName = "openFileDialog1";
            }
            catch (ExitWithoutSaveException)
            {

            }


        }


        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected.save();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            selected.saveAs();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ColorBtn_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            ColorBtn.BackColor = Color;
        }

        private void toolStripButtonText_Click(object sender, EventArgs e)
        {
            Tool = Tools.TEXT;
            if (selected != null) selected.Cursor = new Cursor("ico/cross.cur");
            
            foreach (ToolStripButton item in toolStrip1.Items)
            {
                item.Checked = false;
            }
            toolStripButtonText.Checked = true;
        }
        private void toolStripButtonPen_Click(object sender, EventArgs e)
        {
            Tool = Tools.PEN;
            if (selected != null) selected.Cursor = new Cursor("ico/pencil.cur");
            foreach (ToolStripButton item in toolStrip1.Items)
            {
                item.Checked = false;
            }
            toolStripButtonPen.Checked = true;

        }
        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            Tool = Tools.LINE;
            if (selected != null) selected.Cursor = new Cursor("ico/cross.cur");
            foreach (ToolStripButton item in toolStrip1.Items)
            {
                item.Checked = false;
            }
            toolStripButtonLine.Checked = true;
        }
        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            Tool = Tools.ELLIPSE;
            if (selected != null) selected.Cursor = new Cursor("ico/cross.cur");
            foreach (ToolStripButton item in toolStrip1.Items)
            {
                item.Checked = false;
            }
            toolStripButtonEllipse.Checked = true;
        }
        private void toolStripBttonZalivka_Click(object sender, EventArgs e)
        {
            Tool = Tools.ZALIVKA;
            if (selected != null) selected.Cursor = new Cursor("ico/zalivka.cur");
            foreach (ToolStripButton item in toolStrip1.Items)
            {
                item.Checked = false;
            }
            toolStripBttonZalivka.Checked = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Tool = Tools.MASS;
            if (selected != null) selected.Cursor = new Cursor("ico/cross.cur");
            foreach (ToolStripButton item in toolStrip1.Items)
            {
                item.Checked = false;
            }
        }

        private void General_Load(object sender, EventArgs e)
        {
            color = Color.Black;
            ColorBtn.BackColor = Color;
            g.DrawImage(imgs[1], 0, 0, 90, 90);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selected.Close();
        }

        private void анимацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
            {
                PauseBtn.Visible = true;
                playBtn.Visible = false;
                паузаToolStripMenuItem.Text = "Пауза";
                panel2.Width = 360;
                panel2.Height = 180;
                panel2.Location = new Point((this.Size.Width - 360) / 2, (this.Size.Height - 180) / 2);
                panel2.Visible = true;
            }
            else
            {
                playBtn.Visible = true;
                PauseBtn.Visible = false;
                паузаToolStripMenuItem.Text = "Старт";
                panel2.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i < 88) i++;
            else i = 0;
            g.DrawImage(imgs[i], 0, 0, 180, 180);
            imgs[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
            g.DrawImage(imgs[i],180,00,180,180);
            imgs[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        private void повернутьНа90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            foreach(Image img in imgs)
            {
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            timer1.Enabled = true;

        }

        private void отразитьПоXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            foreach (Image img in imgs)
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            timer1.Enabled = true;
        }

        private void отразитьПоYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            foreach (Image img in imgs)
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            timer1.Enabled = true;
        }

        private void обИзображенииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImgInfo info = new ImgInfo();
            if (selected != null) info.showInfo(selected.Path);
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            PauseBtn.Visible = true;
            playBtn.Visible = false;
            timer1.Enabled = true;
            if (timer1.Enabled) паузаToolStripMenuItem.Text = "Пауза";
            else паузаToolStripMenuItem.Text = "Старт";
            panel2.Width = 360;
            panel2.Height = 180;
            panel2.Location = new Point((this.Size.Width - 360) / 2 , (this.Size.Height-180) / 2);
            panel2.Visible = true;
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            playBtn.Visible = true;
            PauseBtn.Visible = false;
            timer1.Enabled = false;
            if (timer1.Enabled) паузаToolStripMenuItem.Text = "Пауза";
            else паузаToolStripMenuItem.Text = "Старт";
            panel2.Visible = false;
        }
    }
}
