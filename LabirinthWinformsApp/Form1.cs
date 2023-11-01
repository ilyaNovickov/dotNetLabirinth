using LabirinthLib;
using LabirinthLib.Bot;
using LabirinthLib.Printers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using LabirinthWinformsApp.ExtraForms;

namespace LabirinthWinformsApp
{
    public partial class MainForm : Form
    {
        #region Vars
        private Labirinth lab;
        private Action timerAction;
        private Queue<LabirinthLib.Structs.Point> way = new Queue<LabirinthLib.Structs.Point>();
        private Queue<LabirinthLib.Structs.Point> allWay= new Queue<LabirinthLib.Structs.Point>();
        private Queue<Direction> directions = new Queue<Direction>();
        private int botSpeed;

        List<Point> deadEnds;

        bool exitWasFind = false;
        #endregion

        #region Constr
        public MainForm()
        {
            InitializeComponent();

            lab = new Labirinth();

			this.standartEmptySpaccceComboBox.SelectedIndex = 0;
            this.standartSizeComboBox.SelectedIndex = 0;

            this.zoomNumericUpDown.Minimum = 10;
            this.zoomTrackBar.Minimum = 10;
            labControl.Zoom = 10f;

            this.zoomNumericUpDown.Maximum = 100;
            this.zoomTrackBar.Maximum = 100;

            this.botSpeedNumericUpDown.Minimum = 1;
            this.botSpeedTrackBar.Minimum = 1;

            this.botSpeedTrackBar.Maximum = 20;
            this.botSpeedNumericUpDown.Maximum = 20;
            botSpeed = 1 * 100;

            labControl.Labirinth = lab;

            labControl.WaysToMiss.Add("EndWays");

            labControl.Ways.AddWay("WalkedWay", Color.LightGreen);
            labControl.Ways.AddWay("Bot", Color.DarkGreen);
            labControl.Ways.AddWay("FinalWay", Color.FromArgb(0, 255, 0));
            LabirinthControl.ColorfulList list = labControl.Ways.AddWay("EndWays", Color.FromArgb(179, 27, 27));
            list.PerCentsofSize = 0.5f;
        }
        #endregion

        #region TrackBarsAndNumerics
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

                labControl.Zoom = zoomTrackBar.Value;
            }
            else if (sender == botSpeedNumericUpDown)
            {
                if (this.botSpeedTrackBar.Value != ((int)this.botSpeedNumericUpDown.Value))
                    this.botSpeedTrackBar.Value = ((int)this.botSpeedNumericUpDown.Value);

                botSpeed = botSpeedTrackBar.Value * 100;
            }
        }
        #endregion

        #region ExtraMethods
        private void UpdateLabirinthData()
        {
            this.sizeLabel.Text = $"Размер : {lab.Size}";
            this.emptySpaceLabel.Text = $"Пустое пространство : {lab.EmptySpace * 100} %";
            ClearLabirinthPoints();
        }

        private void ClearLabirinthPoints()
        {
            foreach (KeyValuePair<string, LabirinthControl.ColorfulList> list in labControl.Ways)
            {
                list.Value.ListofPoints.Clear();
            }
        }

        private void ReportThatWorking()
        {
            string[] vars = new string[] { "-", "/", "\\", "|" };
            Random random = new Random();
            this.Text = ".Net Labirinth | Думаем " + vars[random.Next(0, vars.Length)];
        }

        private void labControl_MouseTransformedClick(object sender, ScaledMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                labControl.Invalidate();
                return;
            }
            labControl.Refresh();
            Graphics g = labControl.CreateScaledGraphics();
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color.Fuchsia)))
                g.FillRectangle(brush, (int)e.XTransformed, (int)e.YTransformed, 1, 1);
        }

        private void labControl_MouseScaledMove(object sender, ScaledMouseEventArgs e)
        {
            mousePositionLabel.Text = $"Курсор : {(int)e.XTransformed} | {(int)e.YTransformed}";
        }
        #endregion

        #region LabirinthInterection
        private void generateButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.IsBusy)
                return;

            botTableLayout.Enabled = false;

            ClearLabirinthPoints();

            lab.Size = new LabirinthLib.Structs.Size(((int)widthNumericUpDown.Value), 
                ((int)heightNumericUpDowm.Value));
            lab.EmptySpace = ((float)emptySpaceNumericUpDown.Value) / 100f;

            if (timer.Enabled)
                timer.Stop();

            timerAction = this.ReportThatWorking;
            timer.Interval = 1000;
            timer.Start();

            botLogRichTextBox.Clear();

            backgroundWorker.RunWorkerAsync(lab);
        }

        private void exitAndEnterButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
                timer.Stop();
            way?.Clear();
            allWay.Clear();
            directions.Clear();
            botLogRichTextBox.Clear();
			lab.GenerateInsAndExit();
            ClearLabirinthPoints();
        }
        #endregion

        #region Botinteraction
        private void botButton_Click(object sender, EventArgs e)
        {
            ClearLabirinthPoints();

            int numofEnter = 1;
            if (enterComboBox.Text == "№2")
                numofEnter = 2;

            new_WalkerBot bot = new new_WalkerBot(lab);

            exitWasFind = bot.FindExit(numofEnter);

            this.way = bot.WayToExitQueue;

            this.allWay = bot.WayQueue;

            this.directions = bot.DirectionsQueue;

            this.timerAction = this.ReportBotProgress;

            this.deadEnds = bot.DeadEndsList.ToDrawingPointList();


            botLogRichTextBox.Clear();
            botLogRichTextBox.Text += "Запущен бот\n";

            timer.Interval = botSpeed;
            timer.Start();

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
                    timer.Interval = this.botSpeed;
                    timer.Start();
                }
            }
        }

        private void ReportBotProgress()
        {
            if (labControl.Labirinth == null)
                return;

            if (allWay.Count != 0)
            {
                Point point = allWay.Dequeue();
                labControl.Ways["WalkedWay"].ListofPoints.Add(point);
                labControl.Ways["Bot"].ListofPoints.Clear();
                labControl.Ways["Bot"].ListofPoints.Add(point);
                botLogRichTextBox.Text += $"Бот пеермещён в точку : {point} | Направление : {directions.Dequeue()}\n";
            }
            else if (way != null && way.Count != 0)
            {
                Point point = way.Dequeue();
                labControl.Ways["FinalWay"].ListofPoints.Add(point);
            }

            labControl.Invalidate();

            if ((allWay.Count == 0 && way?.Count == 0) || (allWay.Count == 0 && way == null))
            {
                botLogRichTextBox.Text += $"Выход {(exitWasFind ? "был" : "не был")} найден\n";
                //if (exitWasFind)
                //{
                //    botLogRichTextBox.Text += "Путь к выходу:\n";
                //    foreach (Point point in labirinthControl1.Ways["FinalWay"].ListofPoints)
                //    {
                //        botLogRichTextBox.Text += "Путь к выходу:\n";
                //    }
                //}

                labControl.Ways["Bot"].ListofPoints.Clear();

                labControl.Ways["EndWays"].ListofPoints = deadEnds;

                timer.Stop();
            }
        }
        #endregion

        #region BackgroundWorkerAndTimer
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

        private void timer_Tick(object sender, EventArgs e)
        {
            timerAction?.Invoke();
        }
        #endregion

        #region Menu
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

        private void показыватьТупикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (показыватьТупикиToolStripMenuItem.Checked)
                labControl.WaysToMiss.Remove("EndWays");
            else
                labControl.WaysToMiss.Add("EndWays");
            labControl.Invalidate();
        }

        private void botSpeedComboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(botSpeedComboBoxMenu.Text, out int res))
            {
                botSpeedNumericUpDown.Value = res;
            }
        }

        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool withScale = sender == экспортСМаштабомToolStripMenuItem;

            void SaveImageFile(string path)
            {
                Bitmap bitmap;
                Graphics g;

                float zoom = labControl.Zoom;

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

                g.Dispose();
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

        private void созранитьЛогБотаToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "TXT (*.txt)|*.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string path = sfd.FileName;

                    if (path == "" || path == null)
                        return;

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.Write(botLogRichTextBox.Text);
                    }
                }
            }
        }

        private void экспортЛабиринтаСToolStripMenuItem_Click(object sender, EventArgs e)
        {

            void SaveImageFile(string path)
            {
                float zoom = labControl.Zoom;
                Bitmap bitmap = new Bitmap((int)(lab.Width * zoom), (int)(lab.Height * zoom));
                Graphics g = Graphics.FromImage(bitmap);
                lab.DrawLabirinth(g);
                foreach (KeyValuePair<string, LabirinthControl.ColorfulList> pair in labControl.Ways)
                {
                    if (labControl.WaysToMiss.Contains(pair.Key))
                        continue;

                    using (SolidBrush brush = new SolidBrush(pair.Value.Color))
                        foreach (Point point in pair.Value.ListofPoints)
                        {
                            g.FillRectangle(brush, point.X, point.Y, 1, 1);
                        }
                }
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg)|*.jpeg;*.jpg|BMP (*.bmp)|*.bmp;";


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
                        default:
                            break;
                    }
                }
            }
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorForm colorForm = new ColorForm(labControl.Ways);
            if (colorForm.ShowDialog() == DialogResult.OK)
            {
                foreach (KeyValuePair<string, Color> pair in colorForm.NewWaysColors)
                {
                    labControl.Ways[pair.Key].Color = pair.Value;
                }
                labControl.Invalidate();
            }
            colorForm.Dispose();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        #endregion
    }
}
