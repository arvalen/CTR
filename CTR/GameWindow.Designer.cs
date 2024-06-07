namespace CTR
{
    partial class GameWindow
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
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            lblTime = new Label();
            lblCaught = new Label();
            GameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(-6, 949);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1593, 75);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.BackColor = Color.White;
            lblTime.Font = new Font("Chinese Rocks", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTime.Location = new Point(35, 966);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(250, 49);
            lblTime.TabIndex = 1;
            lblTime.Text = "Time Left: 00";
            lblTime.Click += label1_Click;
            // 
            // lblCaught
            // 
            lblCaught.AutoSize = true;
            lblCaught.BackColor = Color.White;
            lblCaught.Font = new Font("Chinese Rocks", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCaught.Location = new Point(1341, 966);
            lblCaught.Name = "lblCaught";
            lblCaught.Size = new Size(196, 49);
            lblCaught.TabIndex = 2;
            lblCaught.Text = "Caught: 0";
            lblCaught.Click += label2_Click;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 15;
            GameTimer.Tick += GameTimerEvent;
            // 
            // GameWindow
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1578, 1024);
            Controls.Add(lblCaught);
            Controls.Add(lblTime);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Name = "GameWindow";
            Text = "Catch The Rubbish";
            Load += GameWindow_Load;
            Click += FormClickEvent;
            Paint += FormPaintEvent;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblTime;
        private Label lblCaught;
        private System.Windows.Forms.Timer GameTimer;
    }
}