namespace Kursach.Forms
{
    partial class ChildForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPEG|*.jpg;*.jpeg;|BMP|*.bmp;|PNG|*.png;|GIF|*.gif;|ICON|*.icon;*.ico;|Другие|*.t" +
    "iff;*.exif;*.wmf;*.emf;";
            // 
            // ChildForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Kursach.Properties.Resources.back2;
            this.ClientSize = new System.Drawing.Size(284, 260);
            this.KeyPreview = true;
            this.Name = "ChildForm";
            this.Text = "Изображение";
            this.Activated += new System.EventHandler(this.ChildForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChildForm_FormClosing);
            this.Load += new System.EventHandler(this.ChildForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChildForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChildForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChildForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChildForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}