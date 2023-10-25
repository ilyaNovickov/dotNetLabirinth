using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private float zoom;
        private Labirinth lab;
        private LabirinthStyle style;

        public LabirinthControl()
        {
            InitializeComponent();
        }

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
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

        public Labirinth Labirinth
        {
            get => lab;
            set
            {
                lab = value;
            }
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

            END:
            base.OnPaint(e);
        }

        private void DrawLabirinthNoneStyle(Graphics g)
        {
            lab.DrawLabirinth(g);
        }

        private void DrawLabirinthAutoSizeStyle(Graphics g)
        {
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
