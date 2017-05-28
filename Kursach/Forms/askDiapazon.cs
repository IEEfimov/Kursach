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
    public partial class askDiapazon : Form
    {
        bool haveResult = false;
        bool isGrayflag = false; 

        public bool isGray
        {
            get { return isGrayflag; }
        }

        public askDiapazon()
        {
            InitializeComponent();
        }

        public int ask()
        {
            ShowDialog();
            isGrayflag = isGrayScale.Checked;
            if (haveResult) return (int)numericUpDown1.Value;
            else throw new ExitWithoutSaveException("Форма завершилась без сохранения!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            haveResult = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            haveResult = false;
            this.Close();
        }
    }
}
