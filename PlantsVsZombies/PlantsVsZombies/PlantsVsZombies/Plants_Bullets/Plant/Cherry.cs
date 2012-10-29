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
    public class Cherry : AttackPlant
    {
        public Cherry(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
        }

        public override void Initialize()
        {
            this.bulletEngine = new Bullets.B_CherryEngine(this.Game, plantManager.GetBulletManager);
            this.range = 9;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void AttackDetect()
        {
            if (currentAnimation.CurrentFrame == currentAnimation.Frames.Count - 1)
            {
                this.plantState = PlantState.ATTACK;
            }
        }

        protected override void SetAnimation()
        {
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\Cherry"));
            this.currentAnimation = this.animations[0];
            base.SetAnimation();
        }

        protected override void SetBulletPosition()
        {
            // Bullet position
            Griding.Cell cell = plantManager.GetGriding.IndexOf(this);
            this.bulletPosition.X = cell.Range.X;
            this.bulletPosition.Y = cell.Range.Y;
        }

        protected override void shootTimer_OnMeet(object o)
        {
            this.ChangeState(PlantState.DIE);
            shootTimer.Stop();

            base.shootTimer_OnMeet(o);
        }
    }
}