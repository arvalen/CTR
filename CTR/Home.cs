using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CTR;

public class Home : Form
{
    private Label lblCTR;
    private Button btnStart;
    private Label lblHTP;
    private PictureBox pictureBox1;
    private PictureBox pictureBox2;
    private PictureBox pictureBox3;
    private PictureBox pictureBox4;
    private GameWindow gameWindow; // tambahkan variabel global untuk instance GameWindow

    public Home()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // Form properties
        this.BackColor = Color.FromArgb(172, 200, 229);
        this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        this.Text = "Catch The Rubbish";
        this.Size = new Size(1600, 1080);

        // lblCTR
        lblCTR = new Label();
        lblCTR.BackColor = Color.FromArgb(172, 200, 229);
        lblCTR.Font = new Font("Avicii", 42F, FontStyle.Regular, GraphicsUnit.Point);
        lblCTR.ForeColor = Color.FromArgb(17, 42, 70);
        lblCTR.Text = "CATCH THE RUBBISH";
        lblCTR.TextAlign = ContentAlignment.MiddleCenter;
        lblCTR.Dock = DockStyle.Top;
        lblCTR.Size = new Size(1578, 276);
        this.Controls.Add(lblCTR);

        // btnStart
        btnStart = new Button();
        btnStart.UseVisualStyleBackColor = true;
        btnStart.Font = new Font("Vermin Verile", 36F, FontStyle.Regular, GraphicsUnit.Point);
        btnStart.Text = "START";
        btnStart.TextAlign = ContentAlignment.MiddleCenter;
        btnStart.Location = new Point(588, 406);
        btnStart.Size = new Size(372, 161);
        btnStart.Click += new EventHandler(this.btnStart_Click); // Tambahkan event handler
        this.Controls.Add(btnStart);

        // lblHTP
        lblHTP = new Label();
        lblHTP.BackColor = Color.FromArgb(172, 200, 229);
        lblHTP.Font = new Font("Chinese Rocks", 20F, FontStyle.Regular, GraphicsUnit.Point);
        lblHTP.Text = "Cara Bermain\n\nAnda memiliki waktu terbatas untuk menangkap sampah sebanyak mungkin. Cukup klik sampah yang terbang di taman dan itu akan ditambahkan ke skor Anda. Anda memiliki waktu yang terbatas. Semoga berhasil.";
        lblHTP.TextAlign = ContentAlignment.MiddleCenter;
        lblHTP.Location = new Point(25, 792);
        lblHTP.Size = new Size(1530, 223);
        this.Controls.Add(lblHTP);

        // pictureBox1
        pictureBox1 = new PictureBox();
        pictureBox1.BackColor = Color.FromArgb(172, 200, 229);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox1.Location = new Point(268, 313);
        pictureBox1.Size = new Size(156, 113);
        pictureBox1.Image = CTR.Properties.Resources._01;
        this.Controls.Add(pictureBox1);

        // pictureBox2
        pictureBox2 = new PictureBox();
        pictureBox2.BackColor = Color.FromArgb(172, 200, 229);
        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox2.Location = new Point(310, 537);
        pictureBox2.Size = new Size(156, 113);
        pictureBox2.Image = CTR.Properties.Resources._02;
        this.Controls.Add(pictureBox2);

        // pictureBox3
        pictureBox3 = new PictureBox();
        pictureBox3.BackColor = Color.FromArgb(172, 200, 229);
        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox3.Location = new Point(1102, 313);
        pictureBox3.Size = new Size(129, 143);
        pictureBox3.Image = CTR.Properties.Resources._03;
        this.Controls.Add(pictureBox3);

        // pictureBox4
        pictureBox4 = new PictureBox();
        pictureBox4.BackColor = Color.FromArgb(172, 200, 229);
        pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
        pictureBox4.Location = new Point(1021, 494);
        pictureBox4.Size = new Size(156, 156);
        pictureBox4.Image = CTR.Properties.Resources._04;
        this.Controls.Add(pictureBox4);

    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        gameWindow = new GameWindow();
        gameWindow.Dock = DockStyle.Fill;
        gameWindow.FormBorderStyle = FormBorderStyle.None;
        gameWindow.TopLevel = false;
        gameWindow.Visible = true;
        this.Controls.Add(gameWindow);
        gameWindow.BringToFront();

        // Sembunyikan elemen-elemen lain di form utama 
        lblCTR.Visible = false;
        lblHTP.Visible = false;
        pictureBox1.Visible = false;
        pictureBox2.Visible = false;
        pictureBox3.Visible = false;
        pictureBox4.Visible = false;
        btnStart.Visible = false;
    }

}