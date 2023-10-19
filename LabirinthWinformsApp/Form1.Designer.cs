
namespace LabirinthWinformsApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labirinthPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labirinthTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.zoomTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.zoomNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labirinthPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.mainTableLayout.SuspendLayout();
            this.labirinthTableLayout.SuspendLayout();
            this.zoomTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 70);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.labirinthPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 405);
            this.panel1.TabIndex = 2;
            // 
            // labirinthPictureBox
            // 
            this.labirinthPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.labirinthPictureBox.Location = new System.Drawing.Point(0, 0);
            this.labirinthPictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labirinthPictureBox.Name = "labirinthPictureBox";
            this.labirinthPictureBox.Size = new System.Drawing.Size(136, 148);
            this.labirinthPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.labirinthPictureBox.TabIndex = 0;
            this.labirinthPictureBox.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test1ToolStripMenuItem,
            this.test2ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // test1ToolStripMenuItem
            // 
            this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
            this.test1ToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.test1ToolStripMenuItem.Text = "test1";
            // 
            // test2ToolStripMenuItem
            // 
            this.test2ToolStripMenuItem.Name = "test2ToolStripMenuItem";
            this.test2ToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.test2ToolStripMenuItem.Text = "test2";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1067, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(151, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.mainTableLayout.ColumnCount = 3;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.mainTableLayout.Controls.Add(this.button1, 0, 0);
            this.mainTableLayout.Controls.Add(this.labirinthTableLayout, 1, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 28);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 1;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Size = new System.Drawing.Size(1067, 500);
            this.mainTableLayout.TabIndex = 5;
            // 
            // labirinthTableLayout
            // 
            this.labirinthTableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.labirinthTableLayout.ColumnCount = 1;
            this.labirinthTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.labirinthTableLayout.Controls.Add(this.panel1, 0, 0);
            this.labirinthTableLayout.Controls.Add(this.zoomTableLayout, 0, 1);
            this.labirinthTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labirinthTableLayout.Location = new System.Drawing.Point(270, 4);
            this.labirinthTableLayout.Name = "labirinthTableLayout";
            this.labirinthTableLayout.RowCount = 2;
            this.labirinthTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.labirinthTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.labirinthTableLayout.Size = new System.Drawing.Size(525, 492);
            this.labirinthTableLayout.TabIndex = 3;
            // 
            // zoomTableLayout
            // 
            this.zoomTableLayout.AutoScroll = true;
            this.zoomTableLayout.ColumnCount = 2;
            this.zoomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.zoomTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.zoomTableLayout.Controls.Add(this.zoomNumericUpDown, 1, 0);
            this.zoomTableLayout.Controls.Add(this.zoomTrackBar, 0, 0);
            this.zoomTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomTableLayout.Location = new System.Drawing.Point(5, 420);
            this.zoomTableLayout.MinimumSize = new System.Drawing.Size(30, 30);
            this.zoomTableLayout.Name = "zoomTableLayout";
            this.zoomTableLayout.RowCount = 1;
            this.zoomTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.zoomTableLayout.Size = new System.Drawing.Size(515, 67);
            this.zoomTableLayout.TabIndex = 3;
            // 
            // zoomNumericUpDown
            // 
            this.zoomNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.zoomNumericUpDown.AutoSize = true;
            this.zoomNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zoomNumericUpDown.Location = new System.Drawing.Point(447, 18);
            this.zoomNumericUpDown.Name = "zoomNumericUpDown";
            this.zoomNumericUpDown.Size = new System.Drawing.Size(65, 30);
            this.zoomNumericUpDown.TabIndex = 0;
            this.zoomNumericUpDown.ValueChanged += new System.EventHandler(this.zoomNumericUpDown1_ValueChanged);
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomTrackBar.Location = new System.Drawing.Point(3, 5);
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.Size = new System.Drawing.Size(438, 56);
            this.zoomTrackBar.TabIndex = 1;
            this.zoomTrackBar.Scroll += new System.EventHandler(this.zoomTrackBar_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.mainTableLayout);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labirinthPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.labirinthTableLayout.ResumeLayout(false);
            this.zoomTableLayout.ResumeLayout(false);
            this.zoomTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox labirinthPictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem test1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test2ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TableLayoutPanel labirinthTableLayout;
        private System.Windows.Forms.TableLayoutPanel zoomTableLayout;
        private System.Windows.Forms.NumericUpDown zoomNumericUpDown;
        private System.Windows.Forms.TrackBar zoomTrackBar;
    }
}

