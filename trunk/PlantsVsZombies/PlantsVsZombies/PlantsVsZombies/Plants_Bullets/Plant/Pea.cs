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

namespace PlantsVsZombies.Plants_Bullets.Plant
{
    public class Pea : AttackPlant
    {
        public Pea(Game game, PlantManager plantManager)
            : base(game, plantManager)
        {
        }

        public override void Initialize()
        {
            this.bulletEngine = new Bullets.B_PeaEngine(this.Game, plantManager.GetBulletManager);
            this.ShootDelay = 1000;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void SetAnimation()
        {
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\Pea"));
            this.currentAnimation = this.animations[0];
            base.SetAnimation();
        }

        protected override void shootTimer_OnMeet(object o)
        {
            // Set the position of bullet is the center of plant
            // Can be repaired after
            bulletEngine.AddBullet(this.GridPosition);
            base.shootTimer_OnMeet(o);
        }
    }
}
