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
    public class B_IcePea : BulletShooter
    {
        // Fields

        // Amount in percent to slow down the enemy when it's attacked
        protected float slowDown;

        // Properties
        public float SlowDown
        {
            get { return this.slowDown; }
        }

        public B_IcePea(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.BName = @"Images\\Bullets\\B_IcePea";
            this.B_Effect = @"Images\\Bullets\\B_IcePeaEffect";

            this.BSprite = SpriteBank.GetSprite(BName);
            this.damage = 100;
            this.slowDown = 0.5f;
            base.Initialize();
        }

        protected override void Collided(Zombies.Zombie zombie)
        {
            Zombies.Impacts.Cold cold = new Zombies.Impacts.Cold(this.Game, 3000, this.slowDown);
            zombie.AddImpact(cold);

            base.Collided(zombie);
        }
    }

    public class B_IcePeaEngine : BulletEngine
    {
        // Fields

        // Methods
        public B_IcePeaEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_IcePea b_pea = new B_IcePea(this.Game, position);
            this.Add(b_pea);
        }
    }
}
