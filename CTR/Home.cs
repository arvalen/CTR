using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using CTR;

namespace CTR
{
    public class Home : Form
    {
        private Label lblCTR;
        private PictureBox pbStart;
        private Label lblHTP;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private GameWindow gameWindow;
        private AnimatedPanel mainPanel;
        private PictureBox pbMusicToggle;
        private SoundPlayer soundPlayer;
        private bool isMusicPlaying;

        public Home()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Properti form
            this.BackColor = Color.Black;
            this.Icon = Properties.Resources.CTR;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Text = "Catch The Rubbish ◥◣";
            this.Size = new Size(1600, 1080);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle; 


            // Panel background dan controls
            mainPanel = new AnimatedPanel(Properties.Resources.bgrnd);
            mainPanel.Dock = DockStyle.Fill;

            // lblCTR
            lblCTR = new Label();
            lblCTR.BackColor = Color.Transparent;
            lblCTR.Font = new Font("Avicii", 42F, FontStyle.Regular, GraphicsUnit.Point);
            lblCTR.ForeColor = Color.FromArgb(17, 42, 70);
            lblCTR.Text = "CATCH THE RUBBISH";
            lblCTR.TextAlign = ContentAlignment.MiddleCenter;
            lblCTR.Dock = DockStyle.Top;
            lblCTR.Size = new Size(1578, 276);

            // lblHTP
            lblHTP = new Label();
            lblHTP.BackColor = Color.Transparent;
            lblHTP.Font = new Font("Chinese Rocks", 20F, FontStyle.Regular, GraphicsUnit.Point);
            lblHTP.ForeColor = Color.LightCyan;
            lblHTP.Text = "Cara Bermain\n\nAnda memiliki waktu terbatas untuk mengambil sampah sebanyak mungkin. Klik & seret sampah sampah itu ke tempat sampah. Anda memiliki waktu yang terbatas. \n Semoga berhasil.";
            lblHTP.TextAlign = ContentAlignment.MiddleCenter;
            lblHTP.Location = new Point(25, 792);
            lblHTP.Size = new Size(1530, 223);

            // pictureBox1
            pictureBox1 = new PictureBox();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Location = new Point(268, 313);
            pictureBox1.Size = new Size(156, 113);
            pictureBox1.Image = Properties.Resources._01;

            // pictureBox2
            pictureBox2 = new PictureBox();
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Location = new Point(310, 537);
            pictureBox2.Size = new Size(156, 113);
            pictureBox2.Image = Properties.Resources._02;

            // pictureBox3
            pictureBox3 = new PictureBox();
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Location = new Point(1102, 313);
            pictureBox3.Size = new Size(129, 143);
            pictureBox3.Image = Properties.Resources._03;

            // pictureBox4
            pictureBox4 = new PictureBox();
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.Location = new Point(1021, 494);
            pictureBox4.Size = new Size(156, 156);
            pictureBox4.Image = Properties.Resources._04;

            // pbStart 
            pbStart = new PictureBox();
            pbStart.BackColor = Color.Transparent;
            pbStart.SizeMode = PictureBoxSizeMode.StretchImage;
            pbStart.Location = new Point(588, 406);
            pbStart.Size = new Size(372, 161);
            pbStart.Image = Properties.Resources.start;
            pbStart.Click += new EventHandler(this.pbStart_Click);

            // pbMusicToggle 
            pbMusicToggle = new PictureBox();
            pbMusicToggle.BackColor = Color.Transparent;
            pbMusicToggle.SizeMode = PictureBoxSizeMode.StretchImage;
            pbMusicToggle.Location = new Point(704, 629);
            pbMusicToggle.Size = new Size(125, 104);
            pbMusicToggle.Image = Properties.Resources.music;
            pbMusicToggle.Click += new EventHandler(this.pbMusicToggle_Click);

            // sound player
            soundPlayer = new SoundPlayer(Properties.Resources.sora);
            isMusicPlaying = false;

            mainPanel.Controls.Add(lblCTR);
            mainPanel.Controls.Add(lblHTP);
            mainPanel.Controls.Add(pictureBox1);
            mainPanel.Controls.Add(pictureBox2);
            mainPanel.Controls.Add(pictureBox3);
            mainPanel.Controls.Add(pictureBox4);
            mainPanel.Controls.Add(pbStart);
            mainPanel.Controls.Add(pbMusicToggle);

            Controls.Add(mainPanel);
        }

        private void pbStart_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();

            // Sembunyikan elemen-elemen lain di form utama
            lblCTR.Visible = false;
            lblHTP.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pbStart.Visible = false;
            mainPanel.BackgroundImage = null;

            // Buat dan tampilkan game window
            gameWindow = new GameWindow();
            gameWindow.Dock = DockStyle.Fill;
            gameWindow.FormBorderStyle = FormBorderStyle.None;
            gameWindow.TopLevel = false;
            gameWindow.Visible = true;
            this.Controls.Add(gameWindow);

            this.ResumeLayout();

            this.Refresh();
            gameWindow.BringToFront();
        }

        private void pbMusicToggle_Click(object sender, EventArgs e)
        {
            if (isMusicPlaying)
            {
                soundPlayer.Stop();
                isMusicPlaying = false;
            }
            else
            {
                soundPlayer.PlayLooping();
                isMusicPlaying = true;
            }
        }
    }

    public class AnimatedPanel : Panel
    {
        private Image animatedImage;

        public AnimatedPanel(Image image)
        {
            this.animatedImage = image;
            this.DoubleBuffered = true;
            ImageAnimator.Animate(animatedImage, OnFrameChanged);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ImageAnimator.UpdateFrames(animatedImage);
            e.Graphics.DrawImage(animatedImage, new Rectangle(0, 0, this.Width, this.Height));
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
