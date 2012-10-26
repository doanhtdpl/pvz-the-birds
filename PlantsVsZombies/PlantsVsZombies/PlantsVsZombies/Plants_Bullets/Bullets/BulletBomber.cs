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
        protected Animation animation;

        public BulletBomber(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.animation.Position = position;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (animation.CurrentFrame == animation.Frames.Count - 1)
            {
                this.isCollided = true;
            }
            else 
                animation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            animation.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
