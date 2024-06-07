using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTR
{
    public partial class GameWindow : Form
    {
        float timeLeft = 60f;
        int caught = 0;
        int spawnTime = 0;
        int spawnLimit = 30;
        List<Sampah> sampah_list = new List<Sampah>();
        Random rand = new Random();

        Image[] sampah_images = {Properties.Resources._01, Properties.Resources._02, Properties.Resources._03, Properties.Resources._04, Properties.Resources._05,
                                 Properties.Resources._06, Properties.Resources._07, Properties.Resources._08, Properties.Resources._09, Properties.Resources._10};


        public GameWindow()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            lblTime.Text = "Time Left: " + timeLeft.ToString("#") + ".s";
            lblCaught.Text = "Caught: " + caught;
            timeLeft -= 0.025f;

            if (sampah_list.Count < spawnLimit)
            {
                spawnTime--;

                if (spawnTime < 1)
                {
                    MakeSampah();
                    spawnTime = spawnLimit;
                }
            }

            foreach (Sampah sampah in sampah_list)
            {
                sampah.MoveSampah();

                sampah.positionX += sampah.speedX;

                if (sampah.positionX < 0 || sampah.positionX + sampah.width > this.ClientSize.Width)
                {
                    sampah.speedX = -sampah.speedX;

                    if (sampah.positionX < 0)
                    {
                        sampah.positionX = sampah.positionX + 10;
                    }
                    else if (sampah.positionX + sampah.width > this.ClientSize.Width)
                    {
                        sampah.positionX = sampah.positionX - 10;
                    }
                }

                sampah.positionY += sampah.speedY;

                if (sampah.positionY < 0 || sampah.positionY + sampah.height > this.ClientSize.Height - 50)
                {
                    sampah.speedY = -sampah.speedY;

                    if (sampah.positionY < 0)
                    {
                        sampah.positionY = sampah.positionY + 10;
                    }
                    else if (sampah.positionY + sampah.height > this.ClientSize.Height - 50)
                    {
                        sampah.positionY = sampah.positionY - 10;
                    }
                }


            }


            if (timeLeft < 1)
            {
                GameOver();
            }

            this.Invalidate();

        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }

        private void FormClickEvent(object sender, EventArgs e)
        {
            foreach (Sampah sampah in sampah_list.ToList())
            {
                MouseEventArgs mouse = (MouseEventArgs)e;

                if (mouse.X >= sampah.positionX && mouse.Y >= sampah.positionY && mouse.X <
                    sampah.positionX + sampah.width && mouse.Y < sampah.positionY + sampah.height)
                {
                    sampah_list.Remove(sampah);
                    caught++;
                }
            }

        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames();

            foreach (Sampah sampah in sampah_list)
            {
                e.Graphics.DrawImage(sampah.sampah_image, sampah.positionX, sampah.positionY, sampah.width, sampah.height);
            }

        }

        private void MakeSampah()
        {
            int i = rand.Next(sampah_images.Length);

            Sampah newSampah = new Sampah();
            newSampah.sampah_image = sampah_images[i];
            newSampah.positionX = rand.Next(50, this.ClientSize.Width - 200);
            newSampah.positionY = rand.Next(50, this.ClientSize.Height - 200);
            sampah_list.Add(newSampah);
            ImageAnimator.Animate(newSampah.sampah_image, this.OnFrameChangedHandler);
        }

        private void OnFrameChangedHandler(object? sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void RestartGame()
        {
            this.Invalidate();
            sampah_list.Clear();
            caught = 0;
            timeLeft = 60f;
            spawnTime = 0;
            lblTime.Text = "Time: 00";
            lblCaught.Text = "Caught: 0";
            GameTimer.Start();
        }

        private void GameOver()
        {
            GameTimer.Stop();
            MessageBox.Show("Waktu Habis!!, Kamu mengumpulkan " + caught + " sampah. Klik OK untuk mencoba lagi.", "Catch The Rubbish ");
            RestartGame();
        }

    }
}
