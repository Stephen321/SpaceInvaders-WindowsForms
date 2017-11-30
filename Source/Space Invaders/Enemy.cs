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
    class Invader
    {
        int x, y, width, height; //Position and dimensions of the enemy
        int speed; //Speed of the enemy
        int formWidth, formHeight; //width and height of the form
        int previousY, moveDownDistance; //used to control how much the enemy moves down the screen once it hits the side
        const int Left = -1, Right = 1, Down = 3;  //Constants to represent the direction the enemy is moving
        int currentDir, previousDir; //Current and last direction the player was moving in
        Image img; //enemy image
        Image frame1;
        Image frame2;
        bool alive;
        
        private static SoundPlayer killedSound = new SoundPlayer("Sounds/invaderkilled.wav");


        public Invader(int theFormWidth, int theFormHeight, int position)
        {

            frame1 = Image.FromFile("Images/invader.png");
            frame2 = Image.FromFile("Images/invader2.png");
            img = frame1;
            formWidth = theFormWidth; //form dimensions
            formHeight = theFormHeight;
            width = img.Width; //Invader dimensions
            height = img.Height;
            x = 0; //start the Invader just abovev the top left of the screen
            y = position*(-height*2); //different height above screen depending on starting position
            speed = 5; 
            moveDownDistance = 40; //The distance the enemy will move down the wall before going left/right
            previousY = 0;
            currentDir = Down; //The current direction the enemy is moving in
            previousDir = Left;
            alive = true;

        }
        public void ChangeFrame()
        {
            if (img == frame1) //check what frame is currently in image and change to the other frame
                img = frame2;
            else if (img == frame2)
                img = frame1;
        }

        public void  Move()
        //Moves the enemy left/right and down the sides of the screen
        {
            if (alive)
            {
                if (y + height > formHeight) //Check if it has left the screen
                {
                    ResetPosition();  //if it has then reset its position back above the screen
                    alive = false;
                }

                if (currentDir == Right) //Checks what direction the enemy is moving 
                {
                    x += speed; //moves Invader
                    if (x + width > formWidth) //if it hits the edge of the screen
                    {
                        previousY = y;    //store the y it had as it hit the edge of the screen
                        previousDir = Right; //store what direction it was just moving in
                        currentDir = Down;  //start moving down
                    }
                }
                else if (currentDir == Down)
                {
                    y += speed;

                    if (y >= previousY + moveDownDistance)
                    {
                        currentDir = -previousDir;  //Reverse the direction the Invader is moving after it finishes moving down
                    }                               //if it moved right and hit the right edge, it will now move left.

                }
                else if (currentDir == Left)
                {
                    x -= speed;
                    if (x < 0)
                    {
                        previousY = y;
                        previousDir = Left;
                        currentDir = Down;
                    }
                } //end else if
            }
        } //end method

        public void Draw(Graphics paper)
        //Draw the Invader
        {
            if (alive)
            {
                paper.DrawImage(img, x, y);
            }
        }

        private void ResetPosition()
        //reset the position of the invader so it is off the screen and cant be hit by bullet
        {
            x = 0;
            y = -height;
        }
        private bool DetectCollision(Player player)
        //Checks if there is no collision between the enemy object and player, if there is then return false or if there is a collision, return true.
        {
            if (player.X + player.Width <= x || player.X >= x + width ||
                player.Y + player.Height <= y || player.Y >= y + height)
                return false;    //No collision
            else
                return true; //Collision
        }

        private bool DetectCollision(Bullet bulletObject)
        //Overloaded method for bullet objects 
        {
            if (bulletObject.X + bulletObject.Width <= x || bulletObject.X >= x + width ||
                bulletObject.Y + bulletObject.Height <= y || bulletObject.Y >= y + height)
                return false;    //No collision
            else
                return true; //Collision
        }

        public void CheckCollisions(Player player, Bullet bulletObject)
        {
            if (DetectCollision(player)) //if and of the enemy and player have collided
            {
                ResetPosition();
                alive = false;
                player.ResetPosition();
                player.DecreaseLives();
                killedSound.Play(); //play the killed sound
            }

            if (DetectCollision(bulletObject)) //if enemy and bullet have collided
            {
                ResetPosition();
                alive = false;
                player.ChangeScore(10); //add 10 points to players score
                bulletObject.ResetPos();
                killedSound.Play();
            }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public SoundPlayer KilledSound
        {
            get { return killedSound; }
        }

        public bool Alive
        {
            get { return alive; }
        }
    }
}
