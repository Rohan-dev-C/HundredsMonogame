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
        public int value;
        public Vector2 speed;
        public bool Selected;
        SpriteFont font;
    
        public Ball(Vector2 position, Texture2D texture, Color color, Vector2 Scale, int value, Vector2 speed, bool selected, SpriteFont font) : base(position, texture, color, Scale)
        {
            this.position = position;
            this.texture = texture; 
            this.color = color; 
            this.scale = Scale;
            this.value = value; 
            this.speed = speed;
            this.Selected = selected;
            this.font = font; 
        }

        public Vector2 Center
        { 
            get
            {
                return Hitbox.Center.ToVector2(); 
            }
        }
        public double radius
        { get { return Size.X / 2; } }

        public void UpdateBall(int screenWidth, int screenHeight)
        {


            position.X += speed.X;
            position.Y += speed.Y;

            if (position.Y < 0)
            {
                speed.Y = Math.Abs(speed.Y);
            }
            if (position.Y + Hitbox.Height > screenHeight)
            {
                speed.Y = -Math.Abs(speed.Y);
            }
            if(position.X < 0)
            {
                speed.X = Math.Abs(speed.X); 
            }
            if (position.X + Hitbox.Width > screenWidth)
            {
                speed.X = -Math.Abs(speed.X); 
            }

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Vector2 length = font.MeasureString(value.ToString());
            spriteBatch.DrawString(this.font, this.value.ToString(), new Vector2(this.Hitbox.Center.X - length.X/2, this.Hitbox.Center.Y - length.Y/2), Color.Lime);

        }
        public bool IsColliding(Ball ballToCheck)
        {
            double sumofRadii = ballToCheck.radius + this.radius;

            double centerdistance = Math.Sqrt(Math.Pow((ballToCheck.Center.X - this.Center.X), 2) + Math.Pow(ballToCheck.Center.Y - this.Center.Y, 2));

            if(sumofRadii > centerdistance)
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }


    }
}
