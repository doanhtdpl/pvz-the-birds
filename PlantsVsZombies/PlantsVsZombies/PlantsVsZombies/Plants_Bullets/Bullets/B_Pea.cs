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
    public class B_Pea : BulletShooter
    {
        // Fields
        public B_Pea(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.BName = @"Images\\Plants\\B_Pea";
            this.B_Effect = @"Images\\Plants\\B_PeaEffect";
            this.BSprite = SpriteBank.GetSprite(this.BName);
            this.damage = 100;
            base.Initialize();
        }
    }

    public class B_PeaEngine : BulletEngine
    {
        // Fields
        protected BulletManager bulletManager;

        // Methods
        public B_PeaEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_Pea b_pea = new B_Pea(this.Game, position);
            this.Add(b_pea);
        }
    }
}
