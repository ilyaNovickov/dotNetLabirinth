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
            }

            base.OnPaint(e);
        }
    }
}
