using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace WeaveSilk
{
    public class Trail : DrawableGameComponent
    {
        public float LifeTime { get; set; } = 20;
        private List<Component> _points;
        private SpriteBatch _spriteBatch;

        public Trail(Game game, SpriteBatch sb) : base(game)
        {
            _spriteBatch = sb;
            _points = new List<Component>();
        }

        public override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();

            if (_points.Count > 0 && _points.First().IsDead)
                _points.RemoveAt(0);

            _points.Add(new Component(this, state.X, state.Y));

            _points.ForEach(c => c.Update());

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for(int i = 0; i < _points.Count - 1; i++)
            {
                Line.DrawLine(Game.GraphicsDevice, _spriteBatch, _points[i].Location, _points[i + 1].Location, 3);
                for (int j = i+ 1; j < _points.Count - 1; j++)
                    Line.DrawLine(Game.GraphicsDevice, _spriteBatch, _points[i].Location, _points[j].Location, 1);
            }
            base.Draw(gameTime);
        }

        private class Component
        {
            public float X { get; set; }
            public float Y { get; set; }
            public Vector2 Location => new Vector2(X, Y);
            public bool IsDead => _current > _trail.LifeTime;

            private Trail _trail;
            private float _current;

            public Component(Trail trail, float x = 0, float y = 0)
            {
                _trail = trail;
                X = x;
                Y = y;
            }

            public void Update() => _current++;
        }
    }
}
