using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;


namespace PlantsVsZombies.Plants_Bullets.Bullets
{
    public class BulletShooter : Bullet
    {
        // Fields
        protected Sprite sprite;
        protected Vector2 velocity = new Vector2(10f, 0f);

        // Properties
        public Sprite BSprite
        {
            get { return this.sprite; }
            set { this.sprite = value; }
        }

        public override Rectangle Bound
        {
            get { return this.sprite.Bound; }
        }

        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        public new Vector2 Position
        {
            get { return this.sprite.Position; }
            set 
            {
                this.sprite.Position = value;
                this.PositionChanged = true;
            }
        }

        // Constructor
        public BulletShooter(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.sprite.Position = position;
            this.PositionChanged = true;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            if (effect != null)
            {
                effect.Update(gameTime);
                if (effect.IsDead)
                    this.isCollided = true;
            }
            else
            {
                base.Update(gameTime);
                CollisionDetect();
            }
        }

        protected virtual void Move()
        {
            this.position += velocity;
            this.sprite.Position = position;
        }

        public override void Draw(GameTime gameTime)
        {
            if (effect != null)
            {
                effect.Draw(gameTime);
            }
            else
            {
                sprite.Draw(gameTime);
                base.Draw(gameTime);
            }
        }
    }
}
