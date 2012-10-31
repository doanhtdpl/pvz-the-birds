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
    public class B_FreeMush : BulletShooter
    {
        public B_FreeMush(Game game, Vector2 position)
            : base(game, position)
        {
        }

        public override void Initialize()
        {
            this.BName = @"Images\Bullets\B_FreeMush";
            this.B_Effect = @"Images\B_FreeMushEffect";
            this.sprite = SpriteBank.GetSprite(BName);
            this.damage = 10;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    public class B_FreeMushEngine : BulletEngine
    {
        // Methods
        public B_FreeMushEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_FreeMush b_pea = new B_FreeMush(this.Game, position);
            this.Add(b_pea);
        }
    }
}
