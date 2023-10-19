using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackBarWithNumericUpDowmLib
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (sender is TrackBar trackBar)
            {
                this.numericUpDown1.Value = this.trackBar1.Value;
            }
            else if (sender is NumericUpDown numeric)
            {
                this.trackBar1.Value = ((int)this.numericUpDown1.Value);
            }
            numericUpDown1.Scroll += numericUpDown1_Scroll;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
