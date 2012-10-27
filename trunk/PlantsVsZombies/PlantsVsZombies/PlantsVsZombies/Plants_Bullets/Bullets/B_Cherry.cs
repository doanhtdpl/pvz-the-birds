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
    public class B_Cherry : BulletBomber
    {
        public B_Cherry(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.BName = "Images\\Plants\\B_Cherry";
            //this.B_Effect = @"Images\\Plants\\B_CherryEffect";
            this.animation = SpriteBank.GetAnimation(this.BName);
            this.damage = 500;
            base.Initialize();
        }
    }

    public class B_CherryEngine : BulletEngine
    {
        // Methods
        public B_CherryEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_Cherry b_pea = new B_Cherry(this.Game, position);
            this.Add(b_pea);
        }
    }
}