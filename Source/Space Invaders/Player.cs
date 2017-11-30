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
    class Player
    {
        int x, y, width, height;  //Position and dimensions of the player
        int speed; //Speed of the player
        int formWidth, formHeight; //width and height of the form]
        int score;
        int lives;
        const int MaxLives = 5; //max amount of lives the player has
        Image img; //player image

        private static SoundPlayer killedSound = new SoundPlayer("Sounds/explosion.wav");

        public Player(int theFormWidth, int theFormHeight)
        {
            formWidth = theFormWidth;
            formHeight = theFormHeight;
            img = Image.FromFile("Images/player.png");
            width = img.Width;
            height = img.Height;
            lives = MaxLives;
            x = formWidth / 2 - width; //Centre the player at the bottom of the screen
            y = formHeight - height;
            score = 0;
            speed = 4;
        }

        public void Draw(Graphics paper)
        {
            paper.DrawImage(img, x, y);
        }

        public void MoveLeft()
        {
            if (x < 0) //If it touches the left of the screen
                x = formWidth - width; //Move it to the right of the screen
            else
                x -= speed; //move left
        }

        public void MoveRight()
        {
            if (x > formWidth - width) //If it touches the right side of the screen, move it to the left
                x = 0;
            else
                x += speed; //move right
        }

        public void ChangeScore(int points)
        { //change players score
            score += points;
        }

        public void DecreaseLives()
        {
            lives -= 1; //decrease lives by 1
        }

        public void ResetPosition()
        {//reset players position to the center of the screen when they get hit

            x = formWidth / 2 - width; //Centre the player at the bottom of the screen
            y = formHeight - height;
        }

        public int Lives
        {
            get { return lives; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Score
        {
            get { return score; }
        }

        public SoundPlayer KilledSound
        {
            get { return killedSound; }
        }
    }
}
