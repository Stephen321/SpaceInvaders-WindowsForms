//-----------------------------------------------------------------------------------------------------------------
//Author:
////Name: Stephen Ennis
//Login ID = C00181305
//Date Created: 16/1/2014
//-----------------------------------------------------------------------------------------------------------------
//Description:
//A game where you control the player at the bottom and have to stop the invaders by shooting them.
//------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Space_Invaders
{
    public partial class Form1 : Form
    {
        const int MaxInvaders = 100; //max amount of invaders
        int noOfInvaders;  //current amount of invaders
        Player player;
        Invader[] invaders = new Invader[MaxInvaders]; //set up a new invader array which is capbale of holding MaxInvaders objects
        Bullet playerBullet;
        int invaderFrameTimer; //timer to change invader frame after a certain amount of time
        bool gameOver; //if the game is over or not
        Image gameOverImage; //game over screen
        int level; //what level the game is on
        const int MaxLevel = 5; //the highest level
        const int StartLevel = 1; //the level you start on

        public Form1()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            gameOverImage = Image.FromFile("Images/gameover.png");
            player = new Player(this.ClientRectangle.Width, this.ClientRectangle.Height); //Pass width and height of form to create Player object 
            playerBullet = new Bullet(); //set up bullet object
            noOfInvaders = 5;
            for (int i = 0; i < noOfInvaders; i++)
            {
                invaders[i] = new Invader(this.ClientRectangle.Width, this.ClientRectangle.Height, i); //create enemy objects
            }
            lblScore.Text = "" + player.Score ; //reset label that displays the score
            lblLives.Text = "" + player.Lives;
            level = StartLevel;
            lblLevel.Text = "" + level;
            invaderFrameTimer = 0;
            gameOver = false;
            btnRestart.Enabled = false;
            btnRestart.Visible = false;
        }

        public void UpdateWorld()
        {
            if (gameOver == false)//update the world as long as the game isnt over
            {
                for (int i = 0; i < noOfInvaders; i++)
                {
                    invaders[i].Move();
                }

                AnimateEnemy(); //animate the enemy
                playerBullet.Move();
                CheckCollisons();
                CheckGameOver();
                CheckAllInvadersDead();
            }
        }

        private void CheckAllInvadersDead()
        //checks if all the invaders are dead and if they are then reset their position and increase the level and difficulty
        {
             
            bool allInvadersDead = true; //only reset when all invaders are dead and this is true
            for (int i = 0; i < noOfInvaders; i++)
            {
                if (invaders[i].Alive == true)
                {

                    allInvadersDead = false;
                    break;          //no need to continue looping cause one is true and their position is only reset if none are true 
                }
                else
                    allInvadersDead = true;
            }

            if (allInvadersDead) //if all of the invaders are dead 
            {
                if (level <= 10)
                {
                    level++; //increase the level
                    noOfInvaders += 5; //increase how many invaders there are by 5
                }
                else if (level > 10)
                    level++; //increase the level but dont add anymore invaders
                lblLevel.Text = "" + level;
                for (int i = 0; i < noOfInvaders; i++)
                {
                    invaders[i] = new Invader(this.ClientRectangle.Width, this.ClientRectangle.Height, i); //create new enemy objects with default values
                }
            }
        }


        private void AnimateEnemy()
        //method to animate all enemies after a certain time has passed
        {
            invaderFrameTimer++;
            if (invaderFrameTimer == 15)
            {
                for (int i = 0; i < noOfInvaders; i++) //change the frame of all the invaders
                {
                    invaders[i].ChangeFrame();
                }
                invaderFrameTimer = 0; //reset the timer
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Set up paper , pens and brushes used to draw objects onto the screen
            Graphics paper = e.Graphics;
            Pen blackPen = new Pen(Color.Black); //Create a pen for the outline of the bullet/
            SolidBrush bulletBrush = new SolidBrush(Color.White); //Create a solid brush to fill the bullet with colour
            if (gameOver == false)
            {
                player.Draw(paper);
                playerBullet.Draw(paper, blackPen, bulletBrush);
                for (int i = 0; i < noOfInvaders; i++)
                {
                    invaders[i].Draw(paper);
                }
            }
            else
            {
                paper.DrawImage(gameOverImage, 0, 0);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                player.MoveRight();
            }
            else if (e.KeyCode == Keys.A)
            {
                player.MoveLeft();
            }

            if (e.KeyCode == Keys.Space)
            {
                playerBullet.FireBullet(player);
            }
        }

        private void CheckGameOver()
        { //game ends when the player has 0 lives remaining
            if (player.Lives == 0)
            {
                gameOver = true;
                btnRestart.Enabled = true; //display and enable button to restart
                btnRestart.Visible = true;
            }
                
        }

        private void CheckCollisons()
        { //check if the invader has collied with a bullet or the player
            for (int i = 0; i < noOfInvaders; i++)
            {
                invaders[i].CheckCollisions(player, playerBullet);
                lblLives.Text = "" + player.Lives; //update labels incase they have changed
                lblScore.Text = "" + player.Score;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            SetUpGame();
            this.Focus();  //changes focus to form,So that key presses wont do anything to the button http://stackoverflow.com/questions/6741544/c-sharp-stop-button-from-gaining-focus-on-click
        }
    }
}