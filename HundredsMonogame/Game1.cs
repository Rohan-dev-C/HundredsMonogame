using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace HundredsMonogame
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        int levelNumber = 0;

        //get good
        

        Texture2D ballTexture; 

        List<Ball> balls = new List<Ball>();

        SpriteFont font;

        bool GameStarted = false;
        Random random = new Random();


        void newLevel(ref int levelNum, ref List<Ball> ball)
        {
            ball.Clear();
            levelNum++;
            for(int i = 0; i < levelNum; i++)
            {
                int startingPosY = random.Next(0, 8);
                int startingPosX = random.Next(0, 8); 
                ball.Add(new Ball(new Vector2(100 * startingPosX, 100 * startingPosY), ballTexture, Color.DarkOrchid, new Vector2(0.3f, 0.3f), 0, new Vector2(5,4), false, font));
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ballTexture = Content.Load<Texture2D>("whiteCircle");
            font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState ks = Keyboard.GetState();

            if (GameStarted == false && ks.IsKeyDown(Keys.Space))
            {
                GameStarted = true;
                levelNumber = 1; 
                newLevel(ref levelNumber, ref balls);
            }

            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].UpdateBall(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            }

            MouseState ms = Mouse.GetState(); 
            for (int i = 0; i < balls.Count; i++)
            {
                if (ms.LeftButton == ButtonState.Pressed && balls[i].Hitbox.Contains(ms.Position))
                {
                    for (int j = 0; j < balls.Count; j++)
                    {
                        balls[j].Selected = false;
                    }
                    balls[i].Selected = true;
                }
                else if (ms.LeftButton == ButtonState.Released)
                {
                    balls[i].Selected = false; 
                }
            }
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].Selected)
                {
                    balls[i].value++;
                    balls[i].scale.X += 0.02f;
                    balls[i].scale.Y += 0.02f; 
                }
            }
            int total = 0; 
            for (int i = 0; i < balls.Count; i++)
            {
                total += balls[i].value; 

                
            }
            if (total > 100)
            {
                newLevel(ref levelNumber, ref balls); 
            }
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = 0; j < balls.Count; j++)
                {
                    if (balls[i].IsColliding(balls[j]))
                    {
                        Vector2 speed = balls[i].speed;
                        balls[i].speed = balls[j].speed;
                        balls[j].speed = speed; 

                    }
                }
            }
          
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            spriteBatch.DrawString(font, levelNumber.ToString(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, balls.Count.ToString(), new Vector2(0, 40), Color.White);

            if (GameStarted == false)
            {
                spriteBatch.DrawString(font, "Press Space to Start", new Vector2(300, 300), Color.Black);
                spriteBatch.DrawString(font, "Click on a Circle in Game to Select", new Vector2(300, 200), Color.Black);
            }
            else if (GameStarted)
            {
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].Draw(spriteBatch);
                }
      
            }

            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
