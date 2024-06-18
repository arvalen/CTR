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

        public ScoreboardForm(int score)
        {
            currentScore = score;
            InitializeComponent();
            InitializeCustomComponents();

            // Set form properties
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
        }

        private void InitializeComponent()
        {
            this.lblEnterName = new Label();
            this.txtName = new TextBox();
            this.btnSubmit = new Button();
            this.lblleaderboard = new Label();
            this.listBoxScores = new ListBox();
            this.btnPlayAgain = new Button();
            this.SuspendLayout();
            // 
            // lblEnterName
            // 
            this.lblEnterName.AutoSize = true;
            this.lblEnterName.Location = new Point(207, 106);
            this.lblEnterName.Name = "lblEnterName";
            this.lblEnterName.Size = new Size(198, 27);
            this.lblEnterName.Font = new Font("Arial", 12, FontStyle.Regular);
            this.lblEnterName.Text = "Enter your name:";
            // 
            // txtName
            // 
            this.txtName.Location = new Point(142, 146);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(320, 50);
            this.txtName.Font = new Font("Arial", 12, FontStyle.Regular);
            this.txtName.TextAlign = HorizontalAlignment.Center;

            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new Point(243, 220);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new Size(118, 49);
            this.btnSubmit.Font = new Font("Arial", 12, FontStyle.Regular);
            this.btnSubmit.Text = "Submit";
            // 
            // lblleaderboard
            // 
            this.lblleaderboard.AutoSize = true;
            this.lblleaderboard.Location = new Point(231, 307);
            this.lblleaderboard.Name = "lblleaderboard";
            this.lblleaderboard.Size = new Size(118, 27);
            this.lblleaderboard.Font = new Font("Arial", 12, FontStyle.Regular);
            this.lblleaderboard.Text = "Leaderboard";
            // 
            // listBoxScores
            // 
            this.listBoxScores.DrawMode = DrawMode.OwnerDrawFixed;
            this.listBoxScores.FormattingEnabled = true;
            this.listBoxScores.ItemHeight = 27;
            this.listBoxScores.Location = new Point(142, 345);
            this.listBoxScores.Name = "listBoxScores";
            this.listBoxScores.Size = new Size(320, 274);
            this.listBoxScores.Font = new Font("Arial", 12, FontStyle.Regular);
            this.listBoxScores.DrawItem += new DrawItemEventHandler(listBoxScores_DrawItem);
            // 
            // btnPlayAgain
            // 
            this.btnPlayAgain.Location = new Point(142, 652);
            this.btnPlayAgain.Name = "btnPlayAgain";
            this.btnPlayAgain.Size = new Size(320, 72);
            this.btnPlayAgain.Font = new Font("Arial", 12, FontStyle.Regular);
            this.btnPlayAgain.Text = "Play Again";

            // 
            // ScoreboardForm
            // 
            this.ClientSize = new Size(611, 828);
            this.Controls.Add(this.lblEnterName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblleaderboard);
            this.Controls.Add(this.listBoxScores);
            this.Controls.Add(this.btnPlayAgain);
            this.Name = "ScoreboardForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Scoreboard";
            this.ResumeLayout(false);
            this.PerformLayout();

            LoadScores();
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
