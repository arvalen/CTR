using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace CTR
{
    public class GameWindow : Form
    {
        private PictureBox pictureBoxInfo, pBtrClose;
        private int timeLeft = 5;
        private int caught = 0;
        private int spawnTime = 0;
        private int spawnLimit = 30;
        private Sampah draggingSampah = null;
        private Point draggingOffset;
        private bool isDragging = false;
        private System.Windows.Forms.Timer gameTimer;
        private BackgroundWorker bgWorker;
        private bool gameRunning = true;
        private Label lblCaught;
        private Label lblTimer;
        private ProgressBar cleanlinessMeter;


        private bool scoreSubmitted = false;

        Random rand = new Random();

        List<Sampah> sampahList = new List<Sampah>();

        Image[] sampahImages = {
            Properties.Resources._01, Properties.Resources._02, Properties.Resources._03, Properties.Resources._04, Properties.Resources._05,
            Properties.Resources._06, Properties.Resources._07, Properties.Resources._08, Properties.Resources._09, Properties.Resources._10
        };

        public GameWindow()
        {
            InitializeComponent();
            InitializeTimers();
            InitializeBackgroundWorker();
            StartCountdown();
        }

        private void InitializeComponent()
        {
            // Inisialisasi ProgressBar Kebersihan
            cleanlinessMeter = new ProgressBar();
            cleanlinessMeter.Location = new Point(650, 966);
            cleanlinessMeter.Size = new Size(300, 40);
            cleanlinessMeter.Minimum = 0;
            cleanlinessMeter.Maximum = spawnLimit; // Atur maksimum sesuai dengan spawnLimit
            cleanlinessMeter.Value = 0; // Atur nilai awal berdasarkan jumlah sampah saat ini
            this.Controls.Add(cleanlinessMeter);

            // Pengaturan form dan properti kontrol
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Text = "Catch The Rubbish";
            this.Size = new Size(1600, 1080);
            this.MouseDown += FormMouseDownEvent;
            this.MouseMove += FormMouseMoveEvent;
            this.MouseUp += FormMouseUpEvent;
            this.Paint += FormPaintEvent;

            // Inisialisasi label Time Left
            lblTimer = new Label();
            lblTimer.BackColor = Color.White;
            lblTimer.Font = new Font("Chinese Rocks", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lblTimer.Text = "Time Left: " + timeLeft;
            lblTimer.Location = new Point(35, 966);
            lblTimer.Size = new Size(323, 49);
            this.Controls.Add(lblTimer);

            // Inisialisasi label Caught
            lblCaught = new Label();
            lblCaught.BackColor = Color.White;
            lblCaught.Font = new Font("Chinese Rocks", 24F, FontStyle.Regular, GraphicsUnit.Point);
            lblCaught.Text = "Caught: 0";
            lblCaught.Location = new Point(1310, 966);
            lblCaught.Size = new Size(256, 49);
            this.Controls.Add(lblCaught);

            // Inisialisasi PictureBox Info
            pictureBoxInfo = new PictureBox();
            pictureBoxInfo.BackColor = Color.White;
            pictureBoxInfo.Location = new Point(-6, 949);
            pictureBoxInfo.Size = new Size(1593, 75);
            this.Controls.Add(pictureBoxInfo);

            // Inisialisasi PictureBox Close
            pBtrClose = new PictureBox();
            pBtrClose.BackColor = Color.Transparent;
            pBtrClose.Image = Properties.Resources.trOpen;
            pBtrClose.Location = new Point(1337, 673);
            pBtrClose.Size = new Size(250, 280);
            this.Controls.Add(pBtrClose);
        }

        private void UpdateCleanlinessMeter()
        {
            int cleanlinessValue = (int)Math.Round((double)sampahList.Count / spawnLimit * cleanlinessMeter.Maximum * 3);
            cleanlinessValue = Math.Min(cleanlinessValue, cleanlinessMeter.Maximum);

            cleanlinessMeter.Invoke((Action)(() => cleanlinessMeter.Value = cleanlinessValue));

            // Ubah warna ProgressBar
            Rectangle rect = new Rectangle(0, 0, cleanlinessMeter.Width, cleanlinessMeter.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Green, Color.Red, LinearGradientMode.Horizontal);

            // Gambar ProgressBar
            using (Graphics g = cleanlinessMeter.CreateGraphics())
            {
                g.FillRectangle(brush, 0, 0, rect.Width * cleanlinessMeter.Value / cleanlinessMeter.Maximum, rect.Height);
            }
        }

        private void InitializeTimers()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += GameLoopTickEvent;
            gameTimer.Start();
        }

        private void InitializeBackgroundWorker()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BackgroundWorker_DoWork;
            bgWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (isDragging)
                {
                    UpdateGame();
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        private void GameLoopTickEvent(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                UpdateGame();
            }
        }

        private async void StartCountdown()
        {
            while (timeLeft > 0)
            {
                await Task.Delay(1000);
                timeLeft--;
                UpdateTimerLabel();
            }

            if (timeLeft <= 0)
            {
                GameOver();
            }
        }

        private void UpdateTimerLabel()
        {
            lblTimer.Invoke((Action)(() => lblTimer.Text = "Time Left: " + timeLeft));
        }

        private void UpdateGame()
        {
            if (!gameRunning)
                return;

            if (sampahList.Count < spawnLimit)
            {
                spawnTime--;

                if (spawnTime < 1)
                {
                    MakeSampah();
                    spawnTime = spawnLimit;
                }
            }

            foreach (Sampah sampah in sampahList)
            {
                if (sampah != draggingSampah || !isDragging)
                {
                    sampah.MoveSampah();
                    sampah.positionX += sampah.speedX;

                    if (sampah.positionX < 0 || sampah.positionX + sampah.width > this.ClientSize.Width)
                    {
                        sampah.speedX = -sampah.speedX;

                        if (sampah.positionX < 0)
                        {
                            sampah.positionX += 10;
                        }
                        else if (sampah.positionX + sampah.width > this.ClientSize.Width)
                        {
                            sampah.positionX -= 10;
                        }
                    }

                    sampah.positionY += sampah.speedY;

                    if (sampah.positionY < 0 || sampah.positionY + sampah.height > this.ClientSize.Height - 50)
                    {
                        sampah.speedY = -sampah.speedY;

                        if (sampah.positionY < 0)
                        {
                            sampah.positionY += 10;
                        }
                        else if (sampah.positionY + sampah.height > this.ClientSize.Height - 50)
                        {
                            sampah.positionY -= 10;
                        }
                    }
                }
            }
            UpdateCleanlinessMeter(); 

            this.Invoke((Action)(() => this.Invalidate()));
        }

        private void FormMouseDownEvent(object sender, MouseEventArgs e)
        {
            foreach (Sampah sampah in sampahList)
            {
                if (e.X >= sampah.positionX && e.Y >= sampah.positionY && e.X < sampah.positionX + sampah.width && e.Y < sampah.positionY + sampah.height)
                {
                    draggingSampah = sampah;
                    draggingOffset = new Point(e.X - sampah.positionX, e.Y - sampah.positionY);
                    isDragging = true;
                    break;
                }
            }
        }

        private void FormMouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (isDragging && draggingSampah != null)
            {
                draggingSampah.positionX = e.X - draggingOffset.X;
                draggingSampah.positionY = e.Y - draggingOffset.Y;
                this.Invoke((Action)(() => this.Invalidate()));
            }
        }

        private void FormMouseUpEvent(object sender, MouseEventArgs e)
        {
            if (isDragging && draggingSampah != null)
            {
                if (pBtrClose.Bounds.Contains(e.Location))
                {
                    sampahList.Remove(draggingSampah);
                    caught++;
                    lblCaught.Text = "Caught: " + caught;
                    lblCaught.Invalidate();
                    lblCaught.Update();
                    UpdateCleanlinessMeter();
                }
                draggingSampah = null;
                isDragging = false;
            }
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames();

            foreach (Sampah sampah in sampahList)
            {
                e.Graphics.DrawImage(sampah.sampahImage, sampah.positionX, sampah.positionY, sampah.width, sampah.height);
            }
        }

        private void MakeSampah()
        {
            int i = rand.Next(sampahImages.Length);

            Sampah newSampah = new Sampah();
            newSampah.sampahImage = sampahImages[i];
            newSampah.positionX = rand.Next(50, this.ClientSize.Width - 200);
            newSampah.positionY = rand.Next(50, this.ClientSize.Height - 200);
            sampahList.Add(newSampah);
            ImageAnimator.Animate(newSampah.sampahImage, this.OnFrameChangedHandler);
        }

        private void OnFrameChangedHandler(object? sender, EventArgs e)
        {
            this.Invoke((Action)(() => this.Invalidate()));
        }

        private void RestartGame()
        {
            this.Invalidate();
            gameRunning = true;
            sampahList.Clear();
            caught = 0;
            lblCaught.Text = "Caught: 0";
            timeLeft = 5;
            spawnTime = 0;
            lblTimer.Text = "Time Left: " + timeLeft;
            scoreSubmitted = false;
            StartCountdown();
        }

        private void GameOver()
        {
            gameRunning = false; // Menghentikan pergerakan sampah
            UpdateCleanlinessMeter(); // Update terakhir meter kebersihan

            ScoreboardForm scoreboard = new ScoreboardForm(caught);
            scoreboard.ShowDialog();
            RestartGame();
        }
    }
}
