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
            listBox1 = new ListBox();
            numericThreads = new NumericUpDown();
            progressBar = new ProgressBar();
            btnStart = new Button();
            lblTime = new Label();
            picOriginal = new PictureBox();
            picProcessed = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)numericThreads).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picProcessed).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 424);
            listBox1.TabIndex = 0;
            // 
            // numericThreads
            // 
            numericThreads.Location = new Point(762, 382);
            numericThreads.Name = "numericThreads";
            numericThreads.Size = new Size(113, 23);
            numericThreads.TabIndex = 1;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(146, 413);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(882, 23);
            progressBar.TabIndex = 3;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(953, 382);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 4;
            btnStart.Text = "button1";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(881, 386);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(38, 15);
            lblTime.TabIndex = 5;
            lblTime.Text = "label1";
            // 
            // picOriginal
            // 
            picOriginal.Location = new Point(146, 12);
            picOriginal.Name = "picOriginal";
            picOriginal.Size = new Size(438, 364);
            picOriginal.TabIndex = 6;
            picOriginal.TabStop = false;
            // 
            // picProcessed
            // 
            picProcessed.Location = new Point(590, 12);
            picProcessed.Name = "picProcessed";
            picProcessed.Size = new Size(438, 364);
            picProcessed.SizeMode = PictureBoxSizeMode.Zoom;
            picProcessed.TabIndex = 7;
            picProcessed.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 450);
            Controls.Add(picProcessed);
            Controls.Add(picOriginal);
            Controls.Add(lblTime);
            Controls.Add(btnStart);
            Controls.Add(progressBar);
            Controls.Add(numericThreads);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "s";
            ((System.ComponentModel.ISupportInitialize)numericThreads).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)picProcessed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private NumericUpDown numericThreads;
        private ProgressBar progressBar;
        private Button btnStart;
        private Label lblTime;
        private PictureBox picOriginal;
        private PictureBox picProcessed;
    }
}
