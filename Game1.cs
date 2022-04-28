using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace monogame3
{
    public class Game1 : Game
    {
        Random gen = new Random();
        Texture2D greyTexture;
        Rectangle greyRect;
        Vector2 greySpeed;
        Texture2D brownTexture;
        Rectangle brownRect;
        Vector2 brownSpeed;
        Texture2D creamTexture;
        Rectangle creamRect;
        Vector2 creamSpeed;
        Texture2D orangeTexture;
        Rectangle orangeRect;
        Vector2 orangeSpeed;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.Title = "they've got the moves";
            greyRect = new Rectangle(300, 10, 100, 100);
            greySpeed = new Vector2(2, 2);
            brownRect = new Rectangle(0, 430, 100, 100);
            brownSpeed = new Vector2(2, 3);
            creamRect = new Rectangle(0, 0, 50, 50);
            creamSpeed = new Vector2(5, 5);
            orangeRect = new Rectangle(350, 250, 100, 100);
            do
            {
                orangeSpeed = new Vector2(gen.Next(-4, 5), gen.Next(-4, 5));
            } while (orangeSpeed.Equals((0, 0)));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            greyTexture = Content.Load<Texture2D>("tribbleGrey");
            creamTexture = Content.Load<Texture2D>("tribbleCream");
            orangeTexture = Content.Load<Texture2D>("tribbleOrange");
            brownTexture = Content.Load<Texture2D>("tribbleBrown");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            greyRect.X += (int)greySpeed.X;
            if (greyRect.Right >= _graphics.PreferredBackBufferWidth || greyRect.Left <= 0)
                greySpeed.X *= -1;
            greyRect.Y += (int)greySpeed.Y;
            if (greyRect.Bottom > _graphics.PreferredBackBufferHeight || greyRect.Top <= 0)
                greySpeed.Y *= -1;

            creamRect.X += (int)creamSpeed.X;
            if (creamRect.Left >= _graphics.PreferredBackBufferWidth)
                creamRect.X = 0 - creamRect.Width;
            creamRect.Y += (int)creamSpeed.Y;
            if (creamRect.Top >= _graphics.PreferredBackBufferHeight)
                creamRect.Y = 0 - creamRect.Height;

            brownRect.X += (int)brownSpeed.X;
            if (brownRect.Right >= _graphics.PreferredBackBufferWidth || brownRect.Left <= 0)
                brownSpeed.X *= -1;
            brownRect.Y += (int)brownSpeed.Y;
            if (brownRect.Bottom > _graphics.PreferredBackBufferHeight || brownRect.Top <= 0)
                brownSpeed.Y *= -1;

            if (orangeRect.Top > _graphics.PreferredBackBufferHeight || orangeRect.Bottom < 0 || orangeRect.Left > _graphics.PreferredBackBufferWidth || orangeRect.Right < 0)
            {
                int newSize = gen.Next(70, 131);
                orangeRect.Width = newSize;
                orangeRect.Height = newSize;
                int side = gen.Next(4);
                switch (side){
                    case 0:
                        orangeRect.X = 0 - orangeRect.Width;
                        orangeRect.Y = gen.Next(_graphics.PreferredBackBufferHeight - orangeRect.Height);
                        orangeSpeed.X = gen.Next(1, 5);
                        orangeSpeed.Y = gen.Next(-4, 5);
                        break;
                    case 1:
                        orangeRect.Y = 0 - orangeRect.Height;
                        orangeRect.X = gen.Next(_graphics.PreferredBackBufferWidth - orangeRect.Width);
                        orangeSpeed.X = gen.Next(-4, 5);
                        orangeSpeed.Y = gen.Next(1, 5);
                        break;
                    case 2:
                        orangeRect.X = _graphics.PreferredBackBufferWidth;
                        orangeRect.Y = gen.Next(_graphics.PreferredBackBufferHeight - orangeRect.Height);
                        orangeSpeed.X = gen.Next(1, 5) * -1;
                        orangeSpeed.Y = gen.Next(-4, 5);
                        break;
                    case 3:
                        orangeRect.Y = _graphics.PreferredBackBufferHeight;
                        orangeRect.X = gen.Next(_graphics.PreferredBackBufferWidth - orangeRect.Width);
                        orangeSpeed.X = gen.Next(-4, 5);
                        orangeSpeed.Y = gen.Next(1, 5) * -1;
                        break;
                }
            }
            orangeRect.X += (int)orangeSpeed.X;
            orangeRect.Y += (int)orangeSpeed.Y;
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PapayaWhip);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(creamTexture, creamRect, Color.White);
            _spriteBatch.Draw(greyTexture, greyRect, Color.White);
            _spriteBatch.Draw(brownTexture, brownRect, Color.White);
            _spriteBatch.Draw(orangeTexture, orangeRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
