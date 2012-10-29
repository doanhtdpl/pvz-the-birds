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
    public class B_Mine : BulletBomber
    {
        protected Rectangle bound;

        public override Rectangle Bound
        {
            get
            {
                return this.bound;
            }
        }

        public B_Mine(Game game, Vector2 position)
            : base(game, position)
        {
            CalculateRange();
        }

        public override void Initialize()
        {
            this.BName = "Images\\Bullets\\B_Cherry";
            //this.B_Effect = @"Images\\Plants\\B_MineEffect";
            this.damage = 500;
            base.Initialize();
        }

        protected void CalculateRange()
        {
            Animation ani = SpriteBank.GetAnimation(this.BName);
            ani.Position = this.position;
            animations.Add(ani);
        }

        protected override void CollisionDetect()
        {
            if (this.Cell == null)
                return;

            this.CollisionDetectOnCell(this.Cell);
        }
    }

    public class B_MineEngine : BulletEngine
    {
        // Methods
        public B_MineEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_Mine b_mine = new B_Mine(this.Game, position);
            this.Add(b_mine);
        }
    }
}
