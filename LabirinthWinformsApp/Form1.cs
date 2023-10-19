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

namespace LabirinthWinformsApp
{
    public partial class Form1 : Form
    {
        Labirinth lab = new Labirinth(10); 

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lab.GenerateLabirinth();
            Bitmap bitmap = new Bitmap(lab.Width * 10, lab.Height * 10);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            Matrix matrix = new Matrix();
            matrix.Scale(10f, 10f);
            g.Transform = matrix;
            lab.DrawLabirinth(g);
            pictureBox1.Image = bitmap;

            label1.Text = lab.GetStringLabirinth();
        }
    }
}
