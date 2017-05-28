using Kursach.Classes;
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
    public partial class ChildForm : Form
    {
        private string name = null;
        private Image image;
        private Bitmap bitmap;

        private General parent;
        private bool isChanged = false;
        private bool drawPen = false;
        private bool FirstClick = false;

        private Point PreviousPoint;
        private Pen pen = new Pen(Color.Red, 3);

        public Bitmap Bitmap
        {
            get { return bitmap; }
        }

        public String Path
        {
            get { return name; }
        }

        public ChildForm(General parent, string name)
        {
            this.name = name;

            //if (System.IO.File.Exists("temp/tmp_" + shortName(name)))
            //        System.IO.File.Delete("temp/tmp_" + shortName(name));

            //if (System.IO.File.Exists(name))
            //{
            //    System.IO.File.Copy(name, "temp/tmp_" + shortName(name));
            //}
            //image = Image.FromFile("temp/tmp_" + shortName(name));

            image = Image.FromFile(name);
            bitmap = new Bitmap(image);

            this.parent = parent;
            this.MdiParent = parent;
            InitializeComponent();

            this.Width = image.Size.Width + 16;
            this.Height = image.Size.Height + 40;
            image.Dispose();
        }
        public ChildForm(General parent, Image img)
        {
            this.image = img;
            bitmap = (Bitmap)image;
            this.parent = parent;
            this.MdiParent = parent;
            InitializeComponent();

            this.Width = image.Width;
            this.Height = image.Height;

            this.Width = image.Size.Width + 16;
            this.Height = image.Size.Height + 40;

        }

        private void ChildForm_Load(object sender, EventArgs e)
        {
            this.Text = shortName(name);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;


        }
        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            UpdateScreen();
        }
        private void ChildForm_Activated(object sender, EventArgs e)
        {
            parent.setSelected(this);
            switch (parent.Tool)
            {
                case Tools.LINE:
                    this.Cursor = new Cursor("ico/cross.cur");
                    break;
                case Tools.ELLIPSE:
                    this.Cursor = new Cursor("ico/cross.cur");
                    break;
                case Tools.TEXT:
                    this.Cursor = new Cursor("ico/cross.cur");
                    break;
                case Tools.ZALIVKA:
                    this.Cursor = new Cursor("ico/zalivka.cur");
                    break;
                case Tools.PEN:
                    this.Cursor = new Cursor("ico/pencil.cur");
                    break;
            }
        }
        private void ChildForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.setSelected(null);
            SaveAsk asker = new SaveAsk();
            if (isChanged)
            {
                switch (asker.ask(shortName(name)))
                {
                    case 0:
                        e.Cancel = true;
                        break;
                    case 1:
                        if (!save()) e.Cancel = true;
                        break;
                    case 2:
                        break;
                }
            }

        }

        public bool save()
        {
            if (name == null) return saveAs();
            try
            {
                if (System.IO.File.Exists(name))
                    System.IO.File.Delete(name);
                image = bitmap;
                image.Save(name);
                bitmap = new Bitmap(image);
                image.Dispose();
                setChanged(false);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка сохраниния! \n\t> " + e.Message + " \n\n" + e.StackTrace, name);
                return false;
            }
            return true;
        }
        public bool saveAs()
        {
            try
            {
                saveFileDialog1.ShowDialog();
                string temp = saveFileDialog1.FileName;
                if (temp.Equals(""))
                {
                    return false;
                }
               
                if (System.IO.File.Exists(temp))
                    System.IO.File.Delete(temp);
                image = bitmap;
                image.Save(temp);
                bitmap = new Bitmap(image);
                image.Dispose();
                name = temp;
                setChanged(false);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка сохраниния! \n\t> " + e.Message + " \n\n" + e.StackTrace);
                return false;
            }

            return true;
        }
        private string shortName(string longStr)
        {
            if (longStr == null)
            {
                isChanged = true;
                return "Новое изображение";
            }

            string temp = "";
            for (int i = 0; i < longStr.Length; i++)
            {
                if (longStr[i] != '\\') temp += longStr[i];
                else temp = "";
            }
            return temp;
        }

        private void ChildForm_MouseDown(object sender, MouseEventArgs e)
        {
            switch (parent.Tool)
            {
                case Tools.LINE:
                    DrawLine(new Point(e.X, e.Y));
                    break;
                case Tools.ELLIPSE:
                    DrawEllipse(new Point(e.X, e.Y));
                    break;
                case Tools.TEXT:
                    DrawRect(new Point(e.X, e.Y));
                    break;
                case Tools.ZALIVKA:
                    Fill(new Point(e.X, e.Y));
                    if (!isChanged) setChanged(true);
                    break;
                case Tools.PEN:
                    drawPen = true;
                    break;
                case Tools.MASS:
                    MassInfo info = new MassInfo();
                    info.getInfo(bitmap,e.X,e.Y);
                    break;
                    // запоминаем первую точку для рисования
            }
            PreviousPoint.X = e.X;
            PreviousPoint.Y = e.Y;
        }
        private void ChildForm_MouseUp(object sender, MouseEventArgs e)
        {
            drawPen = false;
        }
        private void ChildForm_MouseMove(object sender, MouseEventArgs e)
        {
            // если курсов еще не отпущен
            if (drawPen)
            {
                if (!isChanged) setChanged(true);
                pen.Color = parent.Color;
                Point point = new Point(e.X, e.Y);
                Graphics onScreen = this.CreateGraphics();
                Graphics onBitmap = Graphics.FromImage(bitmap);
                onBitmap.DrawLine(pen, PreviousPoint, point);
                onScreen.DrawLine(pen, PreviousPoint, point);
                PreviousPoint = point;
            }
        }

        private void setChanged(bool value)
        {
            if (value)
            {
                isChanged = true;
                this.Text = "* " + shortName(name);
            }
            else
            {
                isChanged = false;
                this.Text = shortName(name);
            }

        }

        public void UpdateScreen()
        {
            Graphics onScreen = this.CreateGraphics();
            Graphics onBitmap = Graphics.FromImage(bitmap);
            onScreen.DrawImage(bitmap, 0, 0);
            onBitmap.DrawImage(bitmap, 0, 0);
        }
        void DrawLine(Point point)
        {
            // если один раз уже щелкнули
            if (FirstClick == true)
            {
                pen.Color = parent.Color;
                Graphics onScreen = this.CreateGraphics();
                Graphics onBitmap = Graphics.FromImage(bitmap);
                onScreen.DrawLine(pen, PreviousPoint, point);
                onBitmap.DrawLine(pen, PreviousPoint, point);
                FirstClick = false;
                if (!isChanged) setChanged(true);
            }
            else
            {
                FirstClick = true;
            }
        }
        void DrawEllipse(Point point)
        {
            // если один раз уже щелкнули
            if (FirstClick == true)
            {
                // создаем объект Pen
                Pen BаскРеn = new Pen(parent.Color, 3);
                Graphics onScreen = this.CreateGraphics();
                Graphics onBitmap = Graphics.FromImage(bitmap);
                onScreen.DrawEllipse(BаскРеn,
                    PreviousPoint.X, PreviousPoint.Y,
                    point.X - PreviousPoint.X, point.Y - PreviousPoint.Y);
                onBitmap.DrawEllipse(BаскРеn,
                    PreviousPoint.X, PreviousPoint.Y,
                    point.X - PreviousPoint.X, point.Y - PreviousPoint.Y);
                FirstClick = false;
                if (!isChanged) setChanged(true);
            }
            else
            {
                FirstClick = true;
            }
        }

        void DrawRect(Point point)
        {
            // если один раз уже щелкнули
            if (FirstClick == true)
            {
                // создаем объект Pen
                Pen BаскРеn = new Pen(parent.Color, 3);
                Graphics onScreen = this.CreateGraphics();
                Graphics onBitmap = Graphics.FromImage(bitmap);
                onScreen.DrawRectangle(BаскРеn,
                    PreviousPoint.X, PreviousPoint.Y,
                    point.X - PreviousPoint.X, point.Y - PreviousPoint.Y);
                onBitmap.DrawRectangle(BаскРеn,
                    PreviousPoint.X, PreviousPoint.Y,
                    point.X - PreviousPoint.X, point.Y - PreviousPoint.Y);
                FirstClick = false;
                if (!isChanged) setChanged(true);
            }
            else
            {
                FirstClick = true;
            }
        }

        private void Fill(Point point)
        {
            Color pixelcolor = bitmap.GetPixel(point.X, point.Y);
            Color color = parent.Color;
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point);
            while (stack.Count != 0)
            {
                Point pt = stack.Pop();
                bitmap.SetPixel(pt.X, pt.Y, color);
                Point a = new Point(pt.X + 1, pt.Y);
                Point b = new Point(pt.X, pt.Y + 1);
                Point c = new Point(pt.X - 1, pt.Y);
                Point d = new Point(pt.X, pt.Y - 1);
                if (pt.X + 1 < bitmap.Width && bitmap.GetPixel(pt.X + 1, pt.Y) == pixelcolor)
                    stack.Push(a);
                if (pt.Y + 1 < bitmap.Height && bitmap.GetPixel(pt.X, pt.Y + 1) == pixelcolor)
                    stack.Push(b);
                if (pt.X - 1 >= 0 && bitmap.GetPixel(pt.X - 1, pt.Y) == pixelcolor)
                    stack.Push(c);
                if (pt.Y - 1 >= 0 && bitmap.GetPixel(pt.X, pt.Y - 1) == pixelcolor)
                    stack.Push(d);
            }
            UpdateScreen();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                //MessageBox.Show("111");
                return (save());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

