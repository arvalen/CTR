using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CTR
{
    public partial class ScoreboardForm : Form
    {
        private static List<PlayerScore> scores = new List<PlayerScore>();
        private int currentScore;
        private bool scoreSubmitted = false;
        private PictureBox pbBackToHome;

        public ScoreboardForm(int score)
        {
            currentScore = score;
            InitializeComponent();
            InitializeCustomComponents();

            //  Form properties
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(173, 232, 244);

            lblEnterName = new Label();
            txtName = new TextBox();
            btnSubmit = new Button();
            lblleaderboard = new Label();
            listBoxScores = new ListBox();
            btnPlayAgain = new Button();
            SuspendLayout();

            // lblEnterName
            lblEnterName.AutoSize = true;
            lblEnterName.Font = new Font("Chinese Rocks", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lblEnterName.Location = new Point(203, 106);
            lblEnterName.Name = "lblEnterName";
            lblEnterName.Size = new Size(198, 27);
            lblEnterName.Text = "Enter your name:";

            // txtName
            txtName.Font = new Font("Chinese Rocks", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtName.Location = new Point(142, 146);
            txtName.Name = "txtName";
            txtName.Size = new Size(320, 35);
            txtName.TextAlign = HorizontalAlignment.Center;

            // btnSubmit
            btnSubmit.BackColor = Color.DeepSkyBlue;
            btnSubmit.Font = new Font("Chinese Rocks", 15F, FontStyle.Regular, GraphicsUnit.Point);
            btnSubmit.Location = new Point(243, 220);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(118, 49);
            btnSubmit.Text = "Submit";

            // lblleaderboard
            lblleaderboard.AutoSize = true;
            lblleaderboard.Font = new Font("Chinese Rocks", 14F, FontStyle.Regular, GraphicsUnit.Point);
            lblleaderboard.Location = new Point(231, 307);
            lblleaderboard.Name = "lblleaderboard";
            lblleaderboard.Size = new Size(148, 27);
            lblleaderboard.Text = "Leaderboard";

            // listBoxScores
            listBoxScores.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxScores.Font = new Font("Chinese Rocks", 14F, FontStyle.Regular, GraphicsUnit.Point);
            listBoxScores.FormattingEnabled = true;
            listBoxScores.ItemHeight = 27;
            listBoxScores.Location = new Point(74, 345);
            listBoxScores.Name = "listBoxScores";
            listBoxScores.Size = new Size(471, 274);
            listBoxScores.DrawItem += listBoxScores_DrawItem;

            // btnPlayAgain
            btnPlayAgain.BackColor = Color.ForestGreen;
            btnPlayAgain.Font = new Font("Chinese Rocks", 24F, FontStyle.Regular, GraphicsUnit.Point);
            btnPlayAgain.Location = new Point(142, 652);
            btnPlayAgain.Name = "btnPlayAgain";
            btnPlayAgain.Size = new Size(320, 72);
            btnPlayAgain.Text = "Play Again";

            // ScoreboardForm
            ClientSize = new Size(611, 828);
            Controls.Add(lblEnterName);
            Controls.Add(txtName);
            Controls.Add(btnSubmit);
            Controls.Add(lblleaderboard);
            Controls.Add(listBoxScores);
            Controls.Add(btnPlayAgain);
            Name = "ScoreboardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Scoreboard";
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeCustomComponents()
        {
            btnSubmit.Click += (sender, e) => SubmitScore(txtName.Text);
            btnPlayAgain.Click += (sender, e) => this.Close();
        }

        private Label lblEnterName;
        private TextBox txtName;
        private Button btnSubmit;
        private Label lblleaderboard;
        private ListBox listBoxScores;
        private Button btnPlayAgain;

        private void SubmitScore(string playerName)
        {
            if (!string.IsNullOrWhiteSpace(playerName) && !scoreSubmitted)
            {
                playerName = playerName.ToUpper();

                var existingPlayerScore = scores.FirstOrDefault(s => s.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase));
                if (existingPlayerScore != null)
                {
                    if (currentScore > existingPlayerScore.Score)
                    {
                        existingPlayerScore.Score = currentScore;
                    }
                }
                else
                {
                    scores.Add(new PlayerScore { Name = playerName, Score = currentScore });
                }
                scores = scores.OrderByDescending(s => s.Score).ToList();
                LoadScores();
                scoreSubmitted = true;
                txtName.Enabled = false;
                btnSubmit.Enabled = false;
            }
        }

        private void LoadScores()
        {
            listBoxScores.Items.Clear();
            foreach (var score in scores)
            {
                listBoxScores.Items.Add($"{score.Name}:{score.Score}");
            }
        }

        private void listBoxScores_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0) return;

            var item = listBoxScores.Items[e.Index].ToString();
            var parts = item.Split(':');
            var name = parts[0];
            var score = parts[1];

            var nameRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width / 2, e.Bounds.Height);
            var scoreRect = new Rectangle(e.Bounds.Left + e.Bounds.Width / 2, e.Bounds.Top, e.Bounds.Width / 2, e.Bounds.Height);

            using (var nameBrush = new SolidBrush(e.ForeColor))
            using (var scoreBrush = new SolidBrush(e.ForeColor))
            using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.DrawString(name, e.Font, nameBrush, nameRect, format);
                e.Graphics.DrawString(score, e.Font, scoreBrush, scoreRect, format);
            }

            e.DrawFocusRectangle();
        }
    }

}
