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
    public class B_WaterMush : BulletShooter
    {
        // Fields
        public B_WaterMush(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.BName = @"Images\\Bullets\\B_WaterMush";
            this.B_Effect = @"Images\\Bullets\\B_WaterMushEffect";
            this.BSprite = SpriteBank.GetSprite(this.BName);
            this.damage = 10;
            base.Initialize();
        }
    }

    public class B_WaterMushEngine : BulletEngine
    {
        // Methods
        public B_WaterMushEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_WaterMush b_pea = new B_WaterMush(this.Game, position);
            this.Add(b_pea);
        }
    }
}
