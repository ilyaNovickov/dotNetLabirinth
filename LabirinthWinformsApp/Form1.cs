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
using System.Diagnostics;

namespace LabirinthWinformsApp
{
    public partial class MainForm : Form
    {
        private float zoom = 1f;
        private Labirinth lab; 

        public MainForm()
        {
            InitializeComponent();

            lab = new Labirinth();

            this.zoomNumericUpDown.Minimum = 10;
            this.zoomTrackBar.Minimum = 10;

            this.zoomNumericUpDown.Maximum = 100;
            this.zoomTrackBar.Maximum = 100;

            this.botSpeedNumericUpDown.Minimum = 0;
            this.botSpeedTrackBar.Minimum = 0;

            this.botSpeedTrackBar.Maximum = 100;
            this.botSpeedNumericUpDown.Maximum = 100;
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
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
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

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;

            lab.Size = new LabirinthLib.Structs.Size(((int)widthNumericUpDown.Value), 
                ((int)heightNumericUpDowm.Value));
            lab.EmptySpace = ((float)emptySpaceNumericUpDown.Value) / 100f;

            backgroundWorker.RunWorkerAsync(lab);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is Labirinth lab)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                lab.GenerateLabirinth();
                sw.Stop();
                e.Result = sw.Elapsed;
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is TimeSpan time)
            {
                string units = "мс";
                double value = time.TotalMilliseconds;
                if (time.TotalMilliseconds > 1000)
                {
                    units = "с";
                    value = time.TotalSeconds;
                }
                else if (time.TotalSeconds > 60)
                {
                    units = "мин";
                    value = time.TotalMinutes;
                }
                this.generationTimeLabel.Text =
                    $"Время генерации : {Math.Round(value, 2)}" + " " + units;
            }
            this.sizeLabel.Text = $"Размер : {lab.Size.ToString()}";
            this.emptySpaceLabel.Text = $"Пустое пространство : {lab.EmptySpace * 100} %";
            
            DrawLabirinth();
        }
    }
}
