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
    class Bullet
    {

        int x, y, width, height; //Position and dimensions of the bullet
        int speed; //Speed of the bullet
        bool alive; //if the bullet is alive then display it

        private static SoundPlayer shootSound = new SoundPlayer("Sounds/shoot.wav");

        public Bullet()
        {
            speed = 10;
            width = 5;
            height = 10;
            ResetPos();
        }

        public void FireBullet(Player player)
        {//move bullet and get start position if its alive
            
            if (alive == false) //if the bullet is not already false
            {
                GetBulletStartPosition(player);
                alive = true;
                shootSound.Play();
            }
        }

        public void ResetPos()
        { //reset the position of the bullet 
            alive = false; // no longer draw or fire the bullet
            x = -100;
            y = 200; //move bullet so invader doesnt hit it
        }

        public void Move()
        {
            if (y + height < 0) //bullet has gone off screen
            {
                ResetPos();
            }

            else
            {
                y -= speed; //move bullet up the screen
            }
        }
        private void GetBulletStartPosition(Player player)
        {//starting position of bullet relative to player
            x = (player.X + player.Width / 2)  - (width / 2) -1;
            y = player.Y - height;
        }

        public void Draw(Graphics paper, Pen blackPen, SolidBrush bulletBrush)
        //Draw the Invader
        {
            if (alive)
            {
                Rectangle bulletRect = new Rectangle(x, y, width, height); //Rectangle for the bullet 
                paper.FillRectangle(bulletBrush, bulletRect);
                paper.DrawRectangle(blackPen, bulletRect);
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

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
    }
}
