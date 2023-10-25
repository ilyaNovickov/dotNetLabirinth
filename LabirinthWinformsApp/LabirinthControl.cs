using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabirinthLib;
using LabirinthLib.Printers;

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
        private class WaysCollection
        {
            private Dictionary<string, List<Point>> ways = new Dictionary<string, List<Point>>();

            public List<Point> this[string key]
            {
                get => ways[key];
                set => ways[key] = value;
            }

            public List<Point> this[int index]
            {
                get => ways.Values.ElementAt(index);
            }

            public Dictionary<string, List<Point>> GetWays() => ways;
        }

        private float zoom = 1f;
        private Labirinth lab;
        private LabirinthStyle style;
        private WaysCollection ways;

        private Dictionary<string, List<Point>> asd = new Dictionary<string, List<Point>>()
        {
            { "1", new List<Point>() { new Point(0, 0), new Point(1, 1) } }
        };

        public LabirinthControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        [DefaultValue("1")]
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
                //this.Refresh();
            }
        }

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

        public Dictionary<string, List<Point>> Ways
        {
            get => ways.GetWays();
        }

        private void this_UpdateLabirinth(object sender, EventArgs e)
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

            foreach (Point point in asd["1"])
            {
                using (SolidBrush brush = new SolidBrush(Color.Pink))
                    e.Graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
            }

            END:
            base.OnPaint(e);
        }

        private void DrawLabirinthNoneStyle(Graphics g)
        {
            lab.DrawLabirinth(g);
        }

        private void DrawLabirinthAutoSizeStyle(Graphics g)
        {
            this.Size = new Size((int)(lab.Width*zoom), (int)(lab.Height * zoom));

            g.ScaleTransform(zoom, zoom);

            lab.DrawLabirinth(g);
        }

        private void DrawLabirinthStretchStyle(Graphics g)
        {
            float widthScale, heightScale;

            widthScale = this.Width / this.lab.Width;
            heightScale = this.Height / this.lab.Height;

            g.ScaleTransform(widthScale, heightScale);

            lab.DrawLabirinth(g);
        }
    }
}
