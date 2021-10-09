using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace HundredsMonogame
{
    class Ball : Sprite
    {
        int value;
        public Vector2 speed;
        public bool Selected; 
        public Ball(Vector2 position, Texture2D texture, Color color, Vector2 Scale, int value, Vector2 speed, bool selected) : base(position, texture, color, Scale)
        {
            this.position = position;
            this.texture = texture; 
            this.color = color; 
            this.scale = Scale;
            this.value = value; 
            this.speed = speed;
            this.Selected = selected; 
        }

        public void UpdateBall(int screenWidth, int screenHeight)
        {
            position.X += speed.X;
            position.Y += speed.Y;

            if (position.Y < 0)
            {
                speed.Y = Math.Abs(speed.Y);
            }
            if (position.Y + texture.Height * scale.Y > screenHeight)
            {
                speed.Y = -speed.Y;
            }
            if(position.X < 0)
            {
                speed.X = Math.Abs(speed.X); 
            }
            if (position.X + texture.Width * scale.X > screenWidth)
            {
                speed.X = -speed.X; 
            }
       
        }

    }
}
