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
    public class BulletBomber : Bullet
    {
        // Fields
        protected List<Animation> animations = new List<Animation>();

        public override Rectangle Bound
        {
            get { return this.animations[0].Bound; }
        }

        public BulletBomber(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.PositionChanged = true;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Animation ani in animations)
            {
                if (ani.CurrentFrame == ani.Frames.Count - 1)
                {
                    this.isCollided = true;
                    break;
                }
                ani.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Animation ani in animations)
            {
                ani.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
