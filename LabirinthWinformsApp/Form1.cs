using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabirinthLib;
using LabirinthLib.Structs;

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
            Bitmap bitmap = new Bitmap(lab.Width, lab.Height);
            lab.DrawLabirinth(Graphics.FromImage(bitmap));
            pictureBox1.Image = bitmap;
        }
    }
}
