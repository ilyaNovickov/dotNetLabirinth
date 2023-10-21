using LabirinthLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace LabirinthWinformsApp
{
    public partial class MainForm : Form
    {
        private float zoom;
        private Labirinth lab;
        private Action timerAction;
        private Queue<LabirinthLib.Structs.Point> way = new Queue<LabirinthLib.Structs.Point>();
        private Queue<LabirinthLib.Structs.Point> allWay= new Queue<LabirinthLib.Structs.Point>();
        private Queue<Direction> directions = new Queue<Direction>();
        private LabirinthLib.Structs.Point prevPoint;
        private int botSpeed;

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

            this.botSpeedNumericUpDown.Minimum = 1;
            this.botSpeedTrackBar.Minimum = 1;

            this.botSpeedTrackBar.Maximum = 20;
            this.botSpeedNumericUpDown.Maximum = 20;
            botSpeed = 1 * 100;

            RedrawLabirinth();
        }

        private void RedrawLabirinth()
        {
            Bitmap bitmap = new Bitmap((int)(lab.Width * zoom), (int)(lab.Height * zoom));
            Graphics g = GetCustomizedGraphicsFromImage(bitmap);
            lab.DrawLabirinth(g);
            labirinthPictureBox.Image = bitmap;
        }

        private void UpdateLabirinth()
        {
            Bitmap bitmap = new Bitmap((int)(lab.Width * zoom), (int)(lab.Height * zoom));
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            g.DrawImage(labirinthPictureBox.Image, rect);
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

                botSpeed = botSpeedTrackBar.Value * 100;
            }
            if (!backgroundWorker.IsBusy)
                UpdateLabirinth();
                //RedrawLabirinth();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;

            botTableLayout.Enabled = false;

            lab.Size = new LabirinthLib.Structs.Size(((int)widthNumericUpDown.Value), 
                ((int)heightNumericUpDowm.Value));
            lab.EmptySpace = ((float)emptySpaceNumericUpDown.Value) / 100f;

            if (timer.Enabled)
                timer.Stop();

            timerAction = this.ReportThatWorking;
            timer.Interval = 1000;
            timer.Start();

            backgroundWorker.RunWorkerAsync(lab);
        }

        private void exitAndEnterButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
                timer.Stop();
            lab.GenerateInsAndExit();
            RedrawLabirinth();
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
            this.Text = ".Net Labirinth";
            botTableLayout.Enabled = true;
            timer.Stop();

            UpdateLabirinthData();
        }

        private void UpdateLabirinthData()
        {
            this.sizeLabel.Text = $"Размер : {lab.Size.ToString()}";
            this.emptySpaceLabel.Text = $"Пустое пространство : {lab.EmptySpace * 100} %";

            RedrawLabirinth();
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
            else if (sender == standartZoomComboBox)
            {
                switch (standartZoomComboBox.Text)
                {
                    case "":
                        break;
                    case "10%":
                        zoomNumericUpDown.Value = 10;
                        break;
                    case "25%":
                        zoomNumericUpDown.Value = 25;
                        break;
                    case "30%":
                        zoomNumericUpDown.Value = 30;
                        break;
                    case "40%":
                        zoomNumericUpDown.Value = 40;
                        break;
                    case "50%":
                        zoomNumericUpDown.Value = 50;
                        break;
                    case "60%":
                        zoomNumericUpDown.Value = 60;
                        break;
                    case "80%":
                        zoomNumericUpDown.Value = 80;
                        break;
                    case "90%":
                        zoomNumericUpDown.Value = 90;
                        break;
                    case "100%":
                        zoomNumericUpDown.Value = 100;
                        break;
                }
            }
        }

        private void убратьботаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool value = убратьботаToolStripMenuItem.Checked;
            botTableLayout.Enabled = !value;
            botTableLayout.Visible = !value;
            mainTableLayout.ColumnStyles[2].Width = value ? 0 : 25;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (timerAction != null)
                timerAction();
        }

        private void ReportThatWorking()
        {
            string[] vars = new string[] { "-", "/", "\\", "|"};
            Random random = new Random();
            this.Text = ".Net Labirinth | Думаем " + vars[random.Next(0, vars.Length)];
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool withScale = sender == экспортСМаштабомToolStripMenuItem;

            void SaveImageFile(string path)
            {
                Bitmap bitmap;
                Graphics g;
                if (!withScale)
                {
                    bitmap = new Bitmap(lab.Width, lab.Height);

                    g = Graphics.FromImage(bitmap);
                }
                else
                {
                    bitmap = new Bitmap((int)(lab.Width * zoom), (int)(lab.Height * zoom));

                    g = Graphics.FromImage(bitmap);

                    Matrix matrix = new Matrix();

                    matrix.Scale(zoom, zoom);

                    g.Transform = matrix;
                }

                lab.DrawLabirinth(g);

                bitmap.Save(path);

                bitmap.Dispose();
            }
            void SaveImageSerializeBin(string path)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Create))
                    formatter.Serialize(stream, lab);
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg)|*.jpeg;*.jpg|BMP (*.bmp)|*.bmp;";

                if (!withScale)
                    sfd.Filter += "|BIN (*.bin)|*.bin";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName;

                    if (path == "" || path == null)
                        return;

                    switch (Path.GetExtension(path))
                    {
                        case ".png":
                        case ".jpeg":
                        case ".jpg":
                        case ".bmp":
                            SaveImageFile(path);
                            break;
                        case ".bin":
                            SaveImageSerializeBin(path);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void импортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "BIN (*.bin)|*.bin";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;

                    if (path == null || path == "")
                        return;

                    BinaryFormatter formatter = new BinaryFormatter();

                    object result = null;

                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        result = formatter.Deserialize(stream);
                    }

                    if (result is Labirinth lab)
                    {
                        this.lab = lab;
                        UpdateLabirinthData();
                    }
                }
            }
        }

        private void botButton_Click(object sender, EventArgs e)
        {
            this.RedrawLabirinth();

            int numofEnter = 1;
            if (enterComboBox.Text == "№2")
                numofEnter = 2;

            prevPoint = LabirinthLib.Structs.Point.Empty;

            new_WalkerBot bot = new new_WalkerBot(lab);

            bot.FindExit(numofEnter);

            this.way = bot.WayToExitQueue;

            this.allWay = bot.WayQueue;

            this.directions = bot.DirectionsQueue;

            this.timerAction = this.ReportBotProgress;

            botLogRichTextBox.Clear();
            botLogRichTextBox.Text += "Запущен бот\n";

            timer.Interval = botSpeed;
            timer.Start();

        }

        private void ReportBotProgress()
        {
            if (labirinthPictureBox.Image == null)
                return;

            Graphics g = GetCustomizedGraphicsFromImage(labirinthPictureBox.Image);

            if (!prevPoint.IsZero())
            {
                using (SolidBrush way = new SolidBrush(Color.Chartreuse))
                    g.FillRectangle(way, prevPoint.X, prevPoint.Y, 1, 1);
            }
            if (allWay.Count != 0)
            {
                prevPoint = allWay.Dequeue();
                using (SolidBrush way = new SolidBrush(Color.DarkGreen))
                    g.FillRectangle(way, prevPoint.X, prevPoint.Y, 1, 1);
                botLogRichTextBox.Text += $"Бот пеермещён в точку : {prevPoint} | Направление : {directions.Dequeue()}\n";
            }
            else if (this.way != null && this.way.Count != 0)
            {
                Point point = way.Dequeue();
                using (SolidBrush way = new SolidBrush(Color.Green))
                    g.FillRectangle(way, point.X, point.Y, 1, 1);
            }
            labirinthPictureBox.Invalidate();


            if (allWay.Count == 0 && (this.way != null && this.way.Count == 0))
            {
                prevPoint = LabirinthLib.Structs.Point.Empty;
                timer.Stop();
            }
        }

        private Graphics GetCustomizedGraphicsFromImage(Image image)
        {
            Graphics gLocal = Graphics.FromImage(image);

            Matrix matrix = new Matrix();

            matrix.Scale(zoom, zoom);

            gLocal.Transform = matrix;

            return gLocal;
        }

        private void enterComboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.enterComboBox.SelectedIndex = enterComboBocMenu.SelectedIndex;
        }

        private void doingSomethingBotButton_Click(object sender, EventArgs e)
        {
            if (timerAction == ReportBotProgress)
            {
                if (sender == stopBotButton || sender == остановитьБотаToolStripMenuItem)
                {
                    botLogRichTextBox.Text += "Остановка\n";
                    timer.Stop();
                }
                else if (sender == goBotButton || sender == продолжитьToolStripMenuItem)
                {
                    botLogRichTextBox.Text += "Продолжение\n";
                    timer.Start();
                }
            }
        }

        private void botSpeedComboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(botSpeedComboBoxMenu.Text, out int res))
            {
                botSpeedNumericUpDown.Value = res;
            }
        }
    }
}
