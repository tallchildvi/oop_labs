namespace ParallelVision.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxUrls = new ListBox();
            numericThreads = new NumericUpDown();
            progressBar = new ProgressBar();
            btnStart = new Button();
            lblTime = new Label();
            picOriginal = new PictureBox();
            picProcessed = new PictureBox();
            rbParallel = new RadioButton();
            panel1 = new Panel();
            comboFilter = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numericThreads).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picProcessed).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxUrls
            // 
            listBoxUrls.BackColor = SystemColors.ControlDarkDark;
            listBoxUrls.BorderStyle = BorderStyle.FixedSingle;
            listBoxUrls.FormattingEnabled = true;
            listBoxUrls.ItemHeight = 17;
            listBoxUrls.Location = new Point(12, 14);
            listBoxUrls.Name = "listBoxUrls";
            listBoxUrls.Size = new Size(120, 410);
            listBoxUrls.TabIndex = 0;
            // 
            // numericThreads
            // 
            numericThreads.BackColor = SystemColors.ControlDarkDark;
            numericThreads.Location = new Point(825, 17);
            numericThreads.Name = "numericThreads";
            numericThreads.Size = new Size(113, 25);
            numericThreads.TabIndex = 1;
            numericThreads.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // progressBar
            // 
            progressBar.ForeColor = SystemColors.Control;
            progressBar.Location = new Point(0, 48);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1048, 26);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 3;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.DimGray;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(953, 16);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 26);
            btnStart.TabIndex = 4;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(32, 11);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(45, 19);
            lblTime.TabIndex = 5;
            lblTime.Text = "label1";
            // 
            // picOriginal
            // 
            picOriginal.BorderStyle = BorderStyle.FixedSingle;
            picOriginal.Location = new Point(146, 14);
            picOriginal.Name = "picOriginal";
            picOriginal.Size = new Size(438, 410);
            picOriginal.TabIndex = 6;
            picOriginal.TabStop = false;
            // 
            // picProcessed
            // 
            picProcessed.BorderStyle = BorderStyle.FixedSingle;
            picProcessed.Location = new Point(590, 14);
            picProcessed.Name = "picProcessed";
            picProcessed.Size = new Size(438, 410);
            picProcessed.SizeMode = PictureBoxSizeMode.Zoom;
            picProcessed.TabIndex = 7;
            picProcessed.TabStop = false;
            // 
            // rbParallel
            // 
            rbParallel.AutoSize = true;
            rbParallel.Location = new Point(12, 15);
            rbParallel.Name = "rbParallel";
            rbParallel.Size = new Size(14, 13);
            rbParallel.TabIndex = 8;
            rbParallel.TabStop = true;
            rbParallel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Controls.Add(comboFilter);
            panel1.Controls.Add(rbParallel);
            panel1.Controls.Add(lblTime);
            panel1.Controls.Add(numericThreads);
            panel1.Controls.Add(progressBar);
            panel1.Controls.Add(btnStart);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 430);
            panel1.Name = "panel1";
            panel1.Size = new Size(1039, 74);
            panel1.TabIndex = 9;
            // 
            // comboFilter
            // 
            comboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFilter.FormattingEnabled = true;
            comboFilter.Items.AddRange(new object[] { "Інверсія кольорів", "Чорно-білий (Grayscale)", "Штучне засвітлення" });
            comboFilter.Location = new Point(689, 16);
            comboFilter.Name = "comboFilter";
            comboFilter.Size = new Size(121, 25);
            comboFilter.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1039, 504);
            Controls.Add(panel1);
            Controls.Add(picProcessed);
            Controls.Add(picOriginal);
            Controls.Add(listBoxUrls);
            Font = new Font("Segoe UI", 10F);
            Name = "Form1";
            Text = "Parallel Vision Studio ";
            ((System.ComponentModel.ISupportInitialize)numericThreads).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picProcessed).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxUrls;
        private NumericUpDown numericThreads;
        private ProgressBar progressBar;
        private Button btnStart;
        private Label lblTime;
        private PictureBox picOriginal;
        private PictureBox picProcessed;
        private RadioButton rbParallel;
        private Panel panel1;
        private ComboBox comboFilter;
    }
}
