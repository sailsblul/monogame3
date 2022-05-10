using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace monogame3
{
    class Tribble
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        Random gen = new Random();

        public Tribble(Texture2D texture, Rectangle rectangle, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
        }
        public Tribble(Texture2D texture)
        {
            _texture = texture;
            _rectangle = new Rectangle(gen.Next(800), gen.Next(600), gen.Next(40, 120), gen.Next(40, 120));
            do
            {
                _speed = new Vector2(gen.Next(-4, 5), gen.Next(-4, 5));
            } while (_speed.Equals((0, 0)));
        }
        public Texture2D Texture
        {
            get { return _texture; }
        }
        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public void Move()
        {
            _rectangle.Offset(_speed);
        }
        public void BounceLeftRight()
        {
            _speed.X *= -1;
        }
        public void BounceTopBottom()
        {
            _speed.Y *= -1;
        }
    }
}
