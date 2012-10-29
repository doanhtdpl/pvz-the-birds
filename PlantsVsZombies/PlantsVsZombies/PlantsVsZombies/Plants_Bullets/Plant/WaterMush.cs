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
    public class WaterMush : AttackPlant
    {
        public WaterMush(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
        }

        public override void Initialize()
        {
            this.bulletEngine = new Bullets.B_WaterMushEngine(this.Game, plantManager.GetBulletManager);
            this.ShootDelay = 2000;
            this.Range = 9;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void SetAnimation()
        {
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\SeaMushroom"));
            this.currentAnimation = this.animations[0];
            base.SetAnimation();
        }

        protected override void SetBulletPosition()
        {
            // Bullet position
            this.bulletPosition.X = this.currentAnimation.PositionX + this.currentAnimation.SizeX - 20f;
            this.bulletPosition.Y = this.currentAnimation.PositionY + 1f / 2 * (float)this.currentAnimation.SizeY;
        }

        protected new void shootTimer_OnMeet(object o)
        {
            shootTimer.Stop();

            base.shootTimer_OnMeet(o);
        }
    }
}
