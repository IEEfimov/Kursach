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
    public partial class SaveAsk : Form
    {
        private int answer = 0;
        // 0  - cancel 
        // 1 - save 
        // 2 - don't save
        public SaveAsk()
        {
            InitializeComponent();
        }

        public int ask(string name)
        {
            label1.Text = "Вы хотите сохранить изменения в " + name;
            ShowDialog();
            return answer;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            answer = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            answer = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            answer = 2;
            this.Close();
        }
    }
}
