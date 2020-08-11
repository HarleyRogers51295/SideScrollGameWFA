using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideScrollerConsoleUI
{
    public partial class Form1 : Form
    {
        bool goLeft = false;//controls player going left
        bool goRight = false;//controls player going right
        bool jump = false;//checks if player is jumping
        bool hasKey = false;//checks if player has key

        int jumpSpeed = 10;//jump speed
        int force = 8;//force of jump
        int score = 0;//score

        int playerSpeed = 18;//player speed
        int backLeft = 8;//background movment speed

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            player.Top += jumpSpeed; //links the jump speed to player box
            player.Refresh();//refreshis the player picture boc continuously

            if (jump && force < 0)//if ther jump and force is less then zero, no jump
            {
                jump = false;
            }
            if (jump) //if jump is true, jump speed and force go down. simulates the drop
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else //jump speed up to 12. simulates the up of the jump
            {
                jumpSpeed = 12;
            }
            if (goLeft && player.Left > 100) // makes player stop when they hit the left wall
            {
                player.Left -= playerSpeed; 
            }
            if (goRight && player.Left +(player.Width + 100) < this.ClientSize.Width)//stops player on form right
            {
                player.Left += playerSpeed;
            }
            if (goRight && background.Left > -1350)
            {
                background.Left -= backLeft;
                foreach (Control x in this.Controls) //this makes all the other objects move with the background to the left
                {
                    if (x is PictureBox && x.Tag == "platform" || x is PictureBox && x.Tag == "coin" || x is PictureBox && x.Tag == "door" || x is PictureBox && x.Tag == "key")
                    {
                        x.Left -= backLeft;
                    }
                }
            }
            if (goLeft && background.Left < 2)
            {
                foreach (Control x in this.Controls) //this makes all the other objects move with the background to the right
                {
                    if (x is PictureBox && x.Tag == "platform" || x is PictureBox && x.Tag == "coin" || x is PictureBox && x.Tag == "door" || x is PictureBox && x.Tag == "key")
                    {
                        x.Left += backLeft;
                    }
                }
            }
            foreach (Control x in this.Controls) //checking ofr all the controls
            {
                if (x is PictureBox && x.Tag == "platform")//when x is a platform
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jump)//if player is colliding with the platform and not jumping
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;//put player on top of the box
                        jumpSpeed = 0;//make not jumping
                    }
                }
                if (x is PictureBox && x.Tag == "coin")//this checks if the player gets a coin
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);//reemoves the coin
                        score++;//increments the score
                    }
                }
            }
            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey)//if player connects with door and has the key
            {
                door.Image = Properties.Resources.door_open;//door changes color
                gameTimer.Stop(); //timer stops
                MessageBox.Show("You completed this level!!!");//winning message
            }
            if (player.Bounds.IntersectsWith(key.Bounds))//if player gets key
            {
                this.Controls.Remove(key);//key disapears
                hasKey = true;//player now has key
            }
            if (player.Top + player.Height > this.ClientSize.Height+60)//if player hits the bottowm
            {
                gameTimer.Stop();
                MessageBox.Show("You dun died dawg!");//game over!
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left) //if left arrow key is pressed, go left is turned true
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)//if right arrow key is pressed, go right is turned true
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && !jump) // if space bar is pressed and not already jumping, jump is changed to true
            {
                jump = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) //if left arrow key is pressed, go left is turned true then false
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)//if right arrow key is pressed, go right is turned true then false
            {
                goRight = false;
            }
            if (jump) // if space bar is pressed and not already jumping, jump is changed to true then false
            {
                jump = false;
            }
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
