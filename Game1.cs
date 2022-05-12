using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace monogame3
{
    public class Game1 : Game
    {
        float time;
        float timeStamp;
        float cooldown = 0;
        Random gen = new Random();
        Texture2D[] colours = new Texture2D[4];
        List<Tribble> tribbles = new List<Tribble>();
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
            screen = Screen.Intro;
            base.Initialize();
            tribbles.Add(new Tribble(colours[0], new Rectangle(300, 10, 100, 100), new Vector2(2, 2), 0));
            tribbles.Add(new Tribble(colours[1], new Rectangle(0, 430, 100, 100), new Vector2(2, 3), 0));
            tribbles.Add(new Tribble(colours[2], new Rectangle(0, 0, 50, 50), new Vector2(5, 5), 1));
            tribbles.Add(new Tribble(colours[3], new Rectangle(350, 250, 100, 100), new Vector2(3, 4), 2));
            tribbles.Add(new Tribble(colours[1]));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
          
            colours[0] = Content.Load<Texture2D>("tribbleGrey");
            colours[2] = Content.Load<Texture2D>("tribbleCream");
            colours[3] = Content.Load<Texture2D>("tribbleOrange");
            colours[1] = Content.Load<Texture2D>("tribbleBrown");

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
                foreach (Tribble tribble in tribbles)
                {
                    tribble.Move(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
                    if (tribble.HitWall)
                        coo.Play();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && (float)gameTime.TotalGameTime.TotalSeconds - cooldown >= 0.5)
                {
                    tribbles.Add(new Tribble(colours[gen.Next(4)]));
                    cooldown = (float)gameTime.TotalGameTime.TotalSeconds;
                }
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
                _spriteBatch.DrawString(textFont, "spacebar for more", new Vector2(0, 570), Color.Green);
                foreach (Tribble tribble in tribbles)
                    _spriteBatch.Draw(tribble.Texture, tribble.Bounds, Color.White);
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
