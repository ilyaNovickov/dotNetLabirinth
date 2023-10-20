using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using LabirinthLib;
using LabirinthLib.Structs;
using LabirinthLib.Printers;
using System.Reflection;

namespace LabirinthWinformsApp
{
    public partial class MainForm : Form
    {
        private float zoom = 1f;
        Labirinth lab; 

        public MainForm()
        {
            InitializeComponent();

            lab = new Labirinth();

            this.zoomNumericUpDown.Minimum = 1;
            this.zoomTrackBar.Minimum = 1;

            this.zoomNumericUpDown.Maximum = 100;
            this.zoomTrackBar.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lab.GenerateLabirinth();
            DrawLabirinth();
            //label1.Text = lab.GetStringLabirinth();
        }

        private void DrawLabirinth()
        {
            Bitmap bitmap = new Bitmap((int)(lab.Width * zoom), (int)(lab.Height * zoom));
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Matrix matrix = new Matrix();
            matrix.Scale(zoom, zoom);
            g.Transform = matrix;
            lab.DrawLabirinth(g);
            labirinthPictureBox.Image = bitmap;
        }

        private void zoomTrackBar_Scroll(object sender, EventArgs e)
        {
            if (sender == zoomTrackBar)
                this.zoomNumericUpDown.Value = this.zoomTrackBar.Value;
            else if (sender == botSpeedTrackBar)
                botSpeedNumericUpDown.Value = botSpeedTrackBar.Value;
        }

        private void zoomNumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (sender == zoomNumericUpDown)
            {
                if (this.zoomTrackBar.Value != ((int)this.zoomNumericUpDown.Value))
                    this.zoomTrackBar.Value = ((int)this.zoomNumericUpDown.Value);

                zoom = zoomTrackBar.Value;
            }
            else if (sender == botSpeedNumericUpDown)
            {
                if (this.botSpeedTrackBar.Value != ((int)this.botSpeedNumericUpDown.Value))
                    this.botSpeedTrackBar.Value = ((int)this.botSpeedNumericUpDown.Value);

                //zoom = zoomTrackBar.Value;
            }
            DrawLabirinth();
        }
    }
}
