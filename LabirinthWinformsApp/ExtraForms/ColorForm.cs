using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabirinthWinformsApp.ExtraForms
{
    public partial class ColorForm : Form
    {
        private LabirinthControl.WaysCollection ways;
        private Dictionary<string, Color> newColors = new Dictionary<string, Color>();

        public ColorForm()
        {
            InitializeComponent();
        }

        public ColorForm(LabirinthControl.WaysCollection ways) : this()
        {
            
            this.CancelButton = cancelButton;
            this.AcceptButton = okButton;

            this.ways = ways;

            int countofWays = ways.GetKeys().Count;
            tableLayoutPanel1.RowCount = countofWays;
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[0].Height = 100f/countofWays;

            int row = 0;

            foreach (KeyValuePair<string, LabirinthControl.ColorfulList> pair in ways)
            {
                Panel panelForLabel = new Panel()
                {
                    AutoScroll = true
                };

                Label label = new Label()
                {
                    Name = pair.Key,
                    Text = pair.Key,
                    Font = new Font("Times New Roman", 16)
                };

                Panel panel = new Panel()
                {
                    Name = pair.Key + "Panel",
                    BackColor = pair.Value.Color,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle
                };

                panel.Click += panel_Click;

                tableLayoutPanel1.Controls.Add(panelForLabel, 0, row);
                panelForLabel.Controls.Add(label);
                tableLayoutPanel1.Controls.Add(panel, 1, row);

                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / tableLayoutPanel1.RowCount));

                row++;
            }
        }

        public Dictionary<string, Color> NewWaysColors
        {
            get => newColors;
        }

        private void panel_Click(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                string key = panel.Name.Replace("Panel", "");

                if (ways.ContainseKey(key))
                {
                    using (ColorDialog cd = new ColorDialog())
                    {
                        if (cd.ShowDialog() == DialogResult.OK)
                        {
                            if (newColors.ContainsKey(key))
                                newColors[key] = cd.Color;
                            else
                                newColors.Add(key, cd.Color);
                            panel.BackColor = cd.Color;
                        }
                    }
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
