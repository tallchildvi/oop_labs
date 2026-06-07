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
            ((System.ComponentModel.ISupportInitialize)numericThreads).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picProcessed).BeginInit();
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
            listBoxUrls.Size = new Size(120, 478);
            listBoxUrls.TabIndex = 0;
            // 
            // numericThreads
            // 
            numericThreads.BackColor = SystemColors.ControlDarkDark;
            numericThreads.Location = new Point(834, 436);
            numericThreads.Name = "numericThreads";
            numericThreads.Size = new Size(113, 25);
            numericThreads.TabIndex = 1;
            numericThreads.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // progressBar
            // 
            progressBar.ForeColor = SystemColors.Control;
            progressBar.Location = new Point(146, 468);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(882, 26);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 3;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.DarkBlue;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(953, 436);
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
            lblTime.Location = new Point(218, 438);
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
            picOriginal.Size = new Size(438, 413);
            picOriginal.TabIndex = 6;
            picOriginal.TabStop = false;
            // 
            // picProcessed
            // 
            picProcessed.BorderStyle = BorderStyle.FixedSingle;
            picProcessed.Location = new Point(590, 14);
            picProcessed.Name = "picProcessed";
            picProcessed.Size = new Size(438, 413);
            picProcessed.SizeMode = PictureBoxSizeMode.Zoom;
            picProcessed.TabIndex = 7;
            picProcessed.TabStop = false;
            // 
            // rbParallel
            // 
            rbParallel.AutoSize = true;
            rbParallel.Location = new Point(146, 440);
            rbParallel.Name = "rbParallel";
            rbParallel.Size = new Size(14, 13);
            rbParallel.TabIndex = 8;
            rbParallel.TabStop = true;
            rbParallel.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1048, 510);
            Controls.Add(rbParallel);
            Controls.Add(picProcessed);
            Controls.Add(picOriginal);
            Controls.Add(lblTime);
            Controls.Add(btnStart);
            Controls.Add(progressBar);
            Controls.Add(numericThreads);
            Controls.Add(listBoxUrls);
            Font = new Font("Segoe UI", 10F);
            Name = "Form1";
            Text = "Parallel Vision Studio ";
            ((System.ComponentModel.ISupportInitialize)numericThreads).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picProcessed).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}
