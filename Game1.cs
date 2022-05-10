using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace monogame3
{
    public class Game1 : Game
    {
        float time;
        float timeStamp;
        Random gen = new Random();
        /*
        
        Rectangle Tribble1.Bounds;
        Vector2 greySpeed;
        
        Rectangle Tribble2.Bounds;
        Vector2 brownSpeed;
        
        Rectangle tribble3.Bounds;
        Vector2 creamSpeed;
        
        Rectangle tribble4.Bounds;
        Vector2 orangeSpeed;
        */
        Texture2D brownTexture;
        Texture2D orangeTexture;
        Texture2D creamTexture;
        Texture2D greyTexture;
        Tribble tribble1;
        Tribble tribble2;
        Tribble tribble3;
        Tribble tribble4;
        SoundEffect cooSound;
        SoundEffectInstance coo;
        SoundEffect music;
        SoundEffectInstance playMusic;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        enum Screen
        {
            Intro,
            TribbleYard,
            End
        }
        Screen screen;
        Texture2D introTexture;
        MouseState mouseState;
        SpriteFont textFont;

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
            /*
            Tribble1.Bounds = new Rectangle(300, 10, 100, 100);
            greySpeed = new Vector2(2, 2);
            Tribble2.Bounds = new Rectangle(0, 430, 100, 100);
            brownSpeed = new Vector2(2, 3);
            tribble3.Bounds = new Rectangle(0, 0, 50, 50);
            creamSpeed = new Vector2(5, 5);
            tribble4.Bounds = new Rectangle(350, 250, 100, 100);
            do
            {
                orangeSpeed = new Vector2(gen.Next(-4, 5), gen.Next(-4, 5));
            } while (orangeSpeed.Equals((0, 0)));
            */
            screen = Screen.Intro;
            base.Initialize();
            tribble1 = new Tribble(greyTexture, new Rectangle(300, 10, 100, 100), new Vector2(2, 2));
            tribble2 = new Tribble(brownTexture, new Rectangle(0, 430, 100, 100), new Vector2(2, 3));
            tribble3 = new Tribble(creamTexture, new Rectangle(0, 0, 50, 50), new Vector2(5, 5));
            tribble4 = new Tribble(orangeTexture, new Rectangle(350, 250, 100, 100), new Vector2(3, 4));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
          
            greyTexture = Content.Load<Texture2D>("tribbleGrey");
            creamTexture = Content.Load<Texture2D>("tribbleCream");
            orangeTexture = Content.Load<Texture2D>("tribbleOrange");
            brownTexture = Content.Load<Texture2D>("tribbleBrown");
            
            cooSound = Content.Load<SoundEffect>("tribble_coo");
            coo = cooSound.CreateInstance();
            coo.IsLooped = false;
            music = Content.Load<SoundEffect>("music");
            playMusic = music.CreateInstance();
            playMusic.IsLooped = false;
            introTexture = Content.Load<Texture2D>("tribble_intro");
            textFont = Content.Load<SpriteFont>("text");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            // TODO: Add your update logic here
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.TribbleYard;
                    playMusic.Play();
                    timeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                }
            }
            else if (screen == Screen.TribbleYard)
            {
                time = (float)gameTime.TotalGameTime.TotalSeconds - timeStamp;

                tribble1.Move();
                if (tribble1.Bounds.Right >= _graphics.PreferredBackBufferWidth || tribble1.Bounds.Left <= 0)
                {
                    tribble1.BounceLeftRight();
                    coo.Play();
                }
                if (tribble1.Bounds.Bottom > _graphics.PreferredBackBufferHeight || tribble1.Bounds.Top <= 0)
                {
                    tribble1.BounceTopBottom();
                    coo.Play();
                }

                tribble3.Move();
                if (tribble3.Bounds.Left >= _graphics.PreferredBackBufferWidth)
                    tribble3.Bounds = new Rectangle(-50, tribble3.Bounds.Y, 50, 50);
                if (tribble3.Bounds.Top >= _graphics.PreferredBackBufferHeight)
                    tribble3.Bounds = new Rectangle(tribble3.Bounds.X, -50, 50, 50);

                tribble2.Move();
                if (tribble2.Bounds.Right >= _graphics.PreferredBackBufferWidth || tribble2.Bounds.Left <= 0)
                {
                    tribble2.BounceLeftRight();
                    coo.Play();
                }
                if (tribble2.Bounds.Bottom > _graphics.PreferredBackBufferHeight || tribble2.Bounds.Top <= 0)
                {
                    tribble2.BounceTopBottom();
                    coo.Play();
                }

                if (tribble4.Bounds.Top > _graphics.PreferredBackBufferHeight || tribble4.Bounds.Bottom < 0 || tribble4.Bounds.Left > _graphics.PreferredBackBufferWidth || tribble4.Bounds.Right < 0)
                {
                    int newX;
                    int newY;
                    int newSize = gen.Next(70, 131);
                    int side = gen.Next(4);
                    switch (side)
                    {
                        default:
                            newX = 0 - tribble4.Bounds.Width;
                            newY = gen.Next(_graphics.PreferredBackBufferHeight - tribble4.Bounds.Height);
                            tribble4.Speed = new Vector2(gen.Next(1, 5), gen.Next(-4, 5));
                            break;
                        case 1:
                            newY = 0 - tribble4.Bounds.Height;
                            newX = gen.Next(_graphics.PreferredBackBufferWidth - tribble4.Bounds.Width);
                            tribble4.Speed = new Vector2(gen.Next(-4, 5), gen.Next(1, 5));
                            break;
                        case 2:
                            newX = _graphics.PreferredBackBufferWidth;
                            newY = gen.Next(_graphics.PreferredBackBufferHeight - tribble4.Bounds.Height);
                            tribble4.Speed = new Vector2(gen.Next(1, 5) * -1, gen.Next(-4, 5));
                            break;
                        case 3:
                            newY = _graphics.PreferredBackBufferHeight;
                            newX = gen.Next(_graphics.PreferredBackBufferWidth - tribble4.Bounds.Width);
                            tribble4.Speed = new Vector2(gen.Next(-4, 5), gen.Next(1, 5) * -1);
                            break;
                    }
                    tribble4.Bounds = new Rectangle(newX, newY, newSize, newSize);
                }
                tribble4.Move();
                
                if (playMusic.State == SoundState.Stopped)
                {
                    screen = Screen.End;
                    coo.Stop();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PapayaWhip);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(textFont, "Click to initiate Tribble Time", new Vector2(10, 520), Color.Blue);
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.DrawString(textFont, (music.Duration.TotalSeconds - time).ToString("N0"), new Vector2(0, 0), Color.Green);
                _spriteBatch.Draw(tribble3.Texture, tribble3.Bounds, Color.White);
                _spriteBatch.Draw(tribble1.Texture, tribble1.Bounds, Color.White);
                _spriteBatch.Draw(tribble2.Texture, tribble2.Bounds, Color.White);
                _spriteBatch.Draw(tribble4.Texture, tribble4.Bounds, Color.White);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.DrawString(textFont, "Tribble Time has concluded", new Vector2(230, 250), Color.Red);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
