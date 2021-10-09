using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        bool isPressed;

        bool ballSelected; 


        void newLevel(ref int levelNum, ref List<Ball> ball)
        {
            ball.Clear();
            levelNum++;
            for(int i = 0; i < levelNum; i++)
            {
                ball.Add(new Ball(new Vector2(100 * i + 5, 100 * i + 5), ballTexture, Color.Black, new Vector2(0.3f, 0.3f), 0, new Vector2(5,4), false));
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

            if(ks.IsKeyDown(Keys.LeftShift))
            {
                isPressed = true; 
            }
            else if(!ks.IsKeyDown(Keys.LeftShift))
            {
                isPressed = false; 
            }




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

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
                if(ballSelected == false)
                {
                    spriteBatch.DrawString(font, "Please Select a Ball", new Vector2(300, 50), Color.Black);
                }
            }

            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
