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
        private int _edgeBehaviour;
        private bool _hitWall;
        /// 0-bounce 1-loop 2-teleport
        Random gen = new Random();

        public Tribble(Texture2D texture, Rectangle rectangle, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
            _edgeBehaviour = gen.Next(3);
        }
        public Tribble(Texture2D texture, Rectangle rectangle, Vector2 speed, int behaviour)
        {
            _texture = texture;
            _rectangle = rectangle;
            _speed = speed;
            _edgeBehaviour = behaviour;
        }
        public Tribble(Texture2D texture)
        {
            _texture = texture;
            _rectangle = new Rectangle(new Point(gen.Next(670), gen.Next(470)), new Point(gen.Next(40, 131)));
            do
            {
                _speed = new Vector2(gen.Next(-4, 5), gen.Next(-4, 5));
            } while (_speed.Equals((0, 0)));
            _edgeBehaviour = gen.Next(3);
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

        public bool HitWall
        {
            get { return _hitWall; }
        }

        public void Move(int width, int height)
        {
            _rectangle.Offset(_speed);
            _hitWall = false;
            if (_edgeBehaviour == 0)
            {
                if (Bounds.Right > width || Bounds.Left <= 0)
                {
                    _speed.X *= -1;
                    _hitWall = true;
                }
                if (Bounds.Bottom > height || Bounds.Top <= 0)
                {
                    _speed.Y *= -1;
                    _hitWall = true;
                }
            }
            else if (_edgeBehaviour == 1)
            {
                if (Bounds.Left > width)
                    Bounds = new Rectangle(new Point(0 - _rectangle.Width, _rectangle.Y), _rectangle.Size);
                if (Bounds.Right < 0)
                    Bounds = new Rectangle(new Point(width, _rectangle.Y), _rectangle.Size);
                if (Bounds.Top > height)
                    Bounds = new Rectangle(new Point(_rectangle.X, 0 - _rectangle.Height), _rectangle.Size);
                if (Bounds.Bottom < 0)
                    Bounds = new Rectangle(new Point(_rectangle.X, height), _rectangle.Size);
            }
            else
            {
                if (Bounds.Top > height || Bounds.Bottom < 0 || Bounds.Left > width || Bounds.Right < 0)
                {
                    int newX;
                    int newY;
                    int newSize = gen.Next(70, 131);
                    int side = gen.Next(4);
                    switch (side)
                    {
                        default:
                            newX = 0 - Bounds.Width;
                            newY = gen.Next(height - Bounds.Height);
                            _speed = new Vector2(gen.Next(1, 5), gen.Next(-4, 5));
                            break;
                        case 1:
                            newY = 0 - Bounds.Height;
                            newX = gen.Next(width - Bounds.Width);
                            _speed = new Vector2(gen.Next(-4, 5), gen.Next(1, 5));
                            break;
                        case 2:
                            newX = width;
                            newY = gen.Next(height - Bounds.Height);
                            _speed = new Vector2(gen.Next(1, 5) * -1, gen.Next(-4, 5));
                            break;
                        case 3:
                            newY = height;
                            newX = gen.Next(width - Bounds.Width);
                            _speed = new Vector2(gen.Next(-4, 5), gen.Next(1, 5) * -1);
                            break;
                    }
                    Bounds = new Rectangle(newX, newY, newSize, newSize);
                }
            }
        }
    }
}
