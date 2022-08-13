using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_REX_GAME
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 10;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;


        public Form1()
        {
            InitializeComponent();

            GameReset();
        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            TREX.Top += jumpSpeed;
            txtScore.Text = "Score: " + score;

            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -10;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            if (TREX.Top > 239 && jumping == false)
            {
                force = 10;
                TREX.Top = 239;
                jumpSpeed = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string) x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    if (TREX.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        TREX.Image = Properties.Resources.dead;
                        txtScore.Text += " GAME OVER";               
                        isGameOver = true;
                    }

                }
            }

            if (score > 5)
            {
                obstacleSpeed = 15;
            }

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }

        }

        private void TREX_Click(object sender, EventArgs e)
        {

        }



        private void GameReset()
        {
            force = 10;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score:" + score;
            TREX.Image = Properties.Resources.running;
            isGameOver = false;
            TREX.Top = 239;


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);

                    x.Left = position;
                }
            }

            gameTimer.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
