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
    public class IcePea : AttackPlant
    {
        public IcePea(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
        }

        public override void Initialize()
        {
            this.bulletEngine = new Bullets.B_IcePeaEngine(this.Game, plantManager.GetBulletManager);
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
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\IcePea"));
            this.currentAnimation = this.animations[0];
            base.SetAnimation();
        }

        protected override void RangeDetect()
        {
            if (GMouse.MousePosition.X >= this.currentAnimation.Position.X &&
                GMouse.MousePosition.X <= range &&
                GMouse.MousePosition.Y >= this.currentAnimation.Position.Y &&
                GMouse.MousePosition.Y <= this.currentAnimation.Position.Y + plantManager.GetGriding.CellHeight)
            {
                ChangeState(Plant.PlantState.ATTACK);
            }
            else if (this.health == 0)
            {
                ChangeState(Plant.PlantState.DIE);
            }
            else
            {
                ChangeState(Plant.PlantState.NORMAL);
            }
        }

        protected override void CalculateRange()
        {
            Griding.Cell cell = plantManager.GetGriding.IndexOf(Position);
            range = (plantManager.GetGriding.NumberOfColumns - (int)cell.Index.Y) * plantManager.GetGriding.CellWidth;
            base.CalculateRange();
        }

        protected override void SetBulletPosition()
        {
            // Bullet position
            this.bulletPosition.X = this.currentAnimation.PositionX + this.currentAnimation.SizeX - 20f;
            this.bulletPosition.Y = this.currentAnimation.PositionY + 1f / 4 * (float)this.currentAnimation.SizeY;
        }

        protected new void shootTimer_OnMeet(object o)
        {
            ChangeState(Plant.PlantState.DIE);
            shootTimer.Reset();

            base.shootTimer_OnMeet(o);
        }
    }
}
