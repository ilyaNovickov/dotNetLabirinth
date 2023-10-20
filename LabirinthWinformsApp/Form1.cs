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
        private float zoom;
        private Labirinth lab; 

        public MainForm()
        {
            InitializeComponent();

            lab = new Labirinth();

            this.standartEmptySpaccceComboBox.SelectedIndex = 0;
            this.standartSizeComboBox.SelectedIndex = 0;

            this.zoomNumericUpDown.Minimum = 10;
            this.zoomTrackBar.Minimum = 10;
            zoom = 10f;

            this.zoomNumericUpDown.Maximum = 100;
            this.zoomTrackBar.Maximum = 100;

            this.botSpeedNumericUpDown.Minimum = 0;
            this.botSpeedTrackBar.Minimum = 0;

            this.botSpeedTrackBar.Maximum = 100;
            this.botSpeedNumericUpDown.Maximum = 100;
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

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            if (sender == zoomTrackBar)
                this.zoomNumericUpDown.Value = this.zoomTrackBar.Value;
            else if (sender == botSpeedTrackBar)
                botSpeedNumericUpDown.Value = botSpeedTrackBar.Value;
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
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
            if (!backgroundWorker.IsBusy)
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

        private void exitAndEnterButton_Click(object sender, EventArgs e)
        {
            lab.GenerateInsAndExit();
            DrawLabirinth();
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

        private void standartSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == standartEmptySpaccceComboBox)
            {
                switch (standartEmptySpaccceComboBox.Text)
                {
                    case "":
                        break;
                    case "40%":
                        emptySpaceNumericUpDown.Value = 40;
                        break;
                    case "60%":
                        emptySpaceNumericUpDown.Value = 60;
                        break;
                    case "80%":
                        emptySpaceNumericUpDown.Value = 80;
                        break;
                    case "90%":
                        emptySpaceNumericUpDown.Value = 90;
                        break;
                    case "100%":
                        emptySpaceNumericUpDown.Value = 100;
                        break;
                }
            }
            else if (sender == standartSizeComboBox)
            {
                switch (standartSizeComboBox.Text)
                {
                    case "":
                        break;
                    case "5x5":
                        widthNumericUpDown.Value = 5;
                        heightNumericUpDowm.Value = 5;
                        break;
                    case "10x10":
                        widthNumericUpDown.Value = 10;
                        heightNumericUpDowm.Value = 10;
                        break;
                    case "20x20":
                        widthNumericUpDown.Value = 20;
                        heightNumericUpDowm.Value = 20;
                        break;
                    case "30x30":
                        widthNumericUpDown.Value = 30;
                        heightNumericUpDowm.Value = 30;
                        break;
                    case "40x40":
                        widthNumericUpDown.Value = 40;
                        heightNumericUpDowm.Value = 40;
                        break;
                    case "50x50":
                        widthNumericUpDown.Value = 50;
                        heightNumericUpDowm.Value = 50;
                        break;
                }
            }
        }

    }
}
