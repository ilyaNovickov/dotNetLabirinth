using LabirinthLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LabirinthWinformsApp
{
    public enum LabirinthStyle : byte
    {
        None,
        AutoSize,
        Stretch
    }

    public partial class LabirinthControl : Control
    {
        public class ColorfulList
        {
            private List<Point> list;
            private Color color;
            private float percentofSize;

            public ColorfulList() : this(new List<Point>(), Color.Black)
            {

            }

            public ColorfulList(List<Point> values) : this(values, Color.Black)
            {

            }

            public ColorfulList(List<Point> values, Color color)
            {
                this.list = values;
                this.color = color;
                percentofSize = 1f;
            }

            public Color Color
            {
                get => color;
                set => color = value;
            }

            public List<Point> ListofPoints
            {
                get => list;
                set => list = value;
            }

            public float PerCentsofSize
            {
                get => percentofSize;
                set
                {
                    if (0.5f <= value && value <= 100f)
                        percentofSize = value;
                    else
                        percentofSize = 1f;
                }
            }
        }

        public class WaysCollection //: IEnumerable<KeyValuePair<string, ColorfulList>>
        {

            private Dictionary<string, ColorfulList> ways = new Dictionary<string, ColorfulList>();

            public ColorfulList this[string key]
            {
                get => ways[key];
                set => ways[key] = value;
            }

            public ColorfulList this[int index]
            {
                get => ways.Values.ElementAt(index);
            }

            //public Dictionary<string, List<Point>> GetWays() => ways;

            public ColorfulList AddWay(string name, Color color)
            {
                ColorfulList points = new ColorfulList()
                {
                    Color = color
                };

                this.ways.Add(name, points);

                return points;
            }

            public ColorfulList AddWay(string name)
            {
                ColorfulList wayPoints = new ColorfulList();

                this.ways.Add(name, wayPoints);

                return wayPoints;
            }

            public ColorfulList AddWay()
            {
                ColorfulList points = new ColorfulList();

                this.ways.Add(ways.Count.ToString(), points);

                return points;
            }

            public bool RemoveWay(string key)
            {
                return ways.Remove(key);
            }

            public void RemoveLast()
            {
                ways.Remove(ways.Keys.Last());
            }

            public List<string> GetKeys()
            {
                return ways.Keys.ToList();
            }

            public bool ContainseKey(string key)
            {
                return ways.ContainsKey(key);
            }

            public IEnumerator<KeyValuePair<string, ColorfulList>> GetEnumerator()
            {
                return this.ways.GetEnumerator();
            }

        }

        private float zoom = 1f;
        private Labirinth lab = null;
        private LabirinthStyle style = LabirinthStyle.None;
        private WaysCollection ways = new WaysCollection();

        private List<string> waysToMiss = new List<string>();

        //private Dictionary<string, List<Point>> asd = new Dictionary<string, List<Point>>()
        //{
        //    { "1", new List<Point>() { new Point(0, 0), new Point(1, 1) } }
        //};

        public LabirinthControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        [DisplayName("Маштаб лабиринта")]
        [Category("UsersProperties")]
        [DefaultValue("1")]
        [Description("Маштаб лабиринта в %")]
        public float Zoom
        {
            get => zoom;
            set
            {
                if (0.002f <= value && value <= 100f)
                    zoom = value;
                else
                    zoom = 1f;
                this.Invalidate();
            }
        }

        [DisplayName("Способ отрисовки лабиринта")]
        [Description("Способ отрисовки лабиринта" +
            " None - лабиринт рисуется с исходным размером" +
            " Stretch - лабиринт растягивается под размер элемента управления" +
            " AutoSize - элемент управления подстраивается под лабиринт")]
        [Category("UsersProperties")]
        public LabirinthStyle LabirinthStyle
        {
            get => style;
            set
            {
                style = value;
            }
        }

        [Browsable(false)]
        public Labirinth Labirinth
        {
            get => lab;
            set
            {
                if (lab != null)
                    lab.UpdateLabirinthEvent -= this_UpdateLabirinth;

                if (value == null)
                    lab = value;
                else
                {
                    lab = value;
                    lab.UpdateLabirinthEvent += this_UpdateLabirinth;
                }
            }
        }

        [Browsable(false)]
        public List<string> WaysToMiss
        {
            get => waysToMiss;
            set => waysToMiss = value;
        }

        [Browsable(false)]
        public WaysCollection Ways
        {
            get => ways;
        }

        public event EventHandler<ScaledMouseEventArgs> MouseScaledClick;

        public event EventHandler<ScaledMouseEventArgs> MouseScaledMove;

        protected void this_UpdateLabirinth(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (lab == null)
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                goto END;
            }

            //Просто так, вдруг пригодиться
            //e.Graphics.PageUnit = GraphicsUnit.Pixel;
            //e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            //e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            switch (style)
            {
                default:
                case LabirinthStyle.None:
                    DrawLabirinthNoneStyle(e.Graphics);
                    break;
                case LabirinthStyle.AutoSize:
                    DrawLabirinthAutoSizeStyle(e.Graphics);
                    break;
                case LabirinthStyle.Stretch:
                    DrawLabirinthStretchStyle(e.Graphics);
                    break;
            }

            foreach (KeyValuePair<string, ColorfulList> list in ways)
            {
                if (waysToMiss.Contains(list.Key))
                    continue;

                foreach (Point point in list.Value.ListofPoints)
                {
                    PointF pointF = new PointF(point.X + (1f - list.Value.PerCentsofSize) / 2,
                        point.Y + (1f - list.Value.PerCentsofSize) / 2);
                    SizeF sizef = new SizeF(list.Value.PerCentsofSize, list.Value.PerCentsofSize);

                    using (SolidBrush brush = new SolidBrush(list.Value.Color))
                        e.Graphics.FillRectangle(brush, new RectangleF(pointF, sizef));//point.X, point.Y, 1, 1);
                }
            }

END:
            base.OnPaint(e);
        }

        protected void DrawLabirinthNoneStyle(Graphics g)
        {
            lab.DrawLabirinth(g);
        }

        protected void DrawLabirinthAutoSizeStyle(Graphics g)
        {
            this.Size = new Size((int)(lab.Width * zoom), (int)(lab.Height * zoom));

            g.ScaleTransform(zoom, zoom);

            lab.DrawLabirinth(g);
        }

        protected void DrawLabirinthStretchStyle(Graphics g)
        {
            float widthScale, heightScale;

            widthScale = this.Width / this.lab.Width;
            heightScale = this.Height / this.lab.Height;

            g.ScaleTransform(widthScale, heightScale);

            lab.DrawLabirinth(g);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            MouseScaledClick?.Invoke(this, new ScaledMouseEventArgs(e, Zoom));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            MouseScaledMove?.Invoke(this, new ScaledMouseEventArgs(e, Zoom));
        }

        public Graphics CreateScaledGraphics()
        {
            Graphics g = CreateGraphics();

            g.ScaleTransform(zoom, zoom);

            return g;
        }
    }

    public class ScaledMouseEventArgs : MouseEventArgs
    {


        public ScaledMouseEventArgs(MouseEventArgs e, float zoom) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            Zoom = zoom;
        }

        public float Zoom { get; private set; }

        public float XTransformed
        {
            get => this.X / Zoom;
        }

        public float YTransformed
        {
            get => this.Y / Zoom;
        }

    }
}
