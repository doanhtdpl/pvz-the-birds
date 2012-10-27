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
    public class B_Chili : BulletBomber
    {
        protected int range;
        protected List<Animation> animations;

        public B_Chili(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.BName = "Images\\Plants\\B_Chili";
            //this.B_Effect = @"Images\\Plants\\B_ChilliEffect";
            this.animation = SpriteBank.GetAnimation(this.BName);
            this.damage = 500;

            this.range = 9;

            base.Initialize();

            // Initialize list animations
            this.animations = new List<Animation>();
            for (int i = 0; i < 8; ++i)
            {
                animations.Add(SpriteBank.GetAnimation(this.BName));
            }
            int count = 1;
            foreach (Animation ani in animations)
            {
                ani.PositionX = position.X + count * 60;
                ani.PositionY = position.Y;
                count++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Animation ani in animations)
            {
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

    public class  B_ChiliEngine : BulletEngine
    {
        // Methods
        public B_ChiliEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_Chili b_pea = new B_Chili(this.Game, position);
            this.Add(b_pea);
        }
    }
}
