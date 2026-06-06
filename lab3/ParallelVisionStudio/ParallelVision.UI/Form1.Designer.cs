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
            ((System.ComponentModel.ISupportInitialize)numericThreads).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(120, 94);
            listBox1.TabIndex = 0;
            // 
            // numericThreads
            // 
            numericThreads.Location = new Point(138, 12);
            numericThreads.Name = "numericThreads";
            numericThreads.Size = new Size(120, 23);
            numericThreads.TabIndex = 1;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(146, 49);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(100, 23);
            progressBar.TabIndex = 3;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(146, 83);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 23);
            btnStart.TabIndex = 4;
            btnStart.Text = "button1";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.Location = new Point(273, 9);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(38, 15);
            lblTime.TabIndex = 5;
            lblTime.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTime);
            Controls.Add(btnStart);
            Controls.Add(progressBar);
            Controls.Add(numericThreads);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numericThreads).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private NumericUpDown numericThreads;
        private ProgressBar progressBar;
        private Button btnStart;
        private Label lblTime;
    }
}
