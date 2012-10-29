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
    public class Mine : AttackPlant
    {
        // Timer to grow up
        protected int delayGrowUp;
        protected Counter.Timer growUpTimer;

        public Mine(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
        }

        public override void Initialize()
        {
            this.bulletEngine = new Bullets.B_MineEngine(this.Game, plantManager.GetBulletManager);
            this.range = 1;

            delayGrowUp = 1000;
            growUpTimer = new Counter.Timer(this.Game, delayGrowUp);
            growUpTimer.OnMeet += new Counter.EventOnCounterMeet(growUpTimer_OnMeet);

            shootDelay = 0;

            base.Initialize();
        }

        protected void growUpTimer_OnMeet(Counter.ICounter counter)
        {
            this.currentAnimation.Enable = true;
            growUpTimer.Stop();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentAnimation.CurrentFrame == currentAnimation.Frames.Count - 1)
            {
                this.currentAnimation = animations[1];
            }
            growUpTimer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void SetAnimation()
        {
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\Mine1"));
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\Mine0"));
            this.currentAnimation = this.animations[0];
            this.currentAnimation.Enable = false;
            this.growUpTimer.Start();
            base.SetAnimation();
        }

        protected override void RangeDetect()
        {
            if (GMouse.MousePosition.X >= this.currentAnimation.Position.X &&
                GMouse.MousePosition.X <= range &&
                GMouse.MousePosition.Y >= this.currentAnimation.Position.Y &&
                GMouse.MousePosition.Y <= this.currentAnimation.Position.Y + plantManager.GetGriding.CellHeight)
            {
                if(currentAnimation == animations[1])
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
            range = (int)this.Position.X + plantManager.GetGriding.CellWidth;
            base.CalculateRange();
        }

        protected override void SetBulletPosition()
        {
            // Bullet position
            Griding.Cell cell = plantManager.GetGriding.IndexOf(currentAnimation.Position);
            this.bulletPosition.X = cell.Range.X;
            this.bulletPosition.Y = cell.Range.Y;
        }

        protected override void shootTimer_OnMeet(object o)
        {
            ChangeState(Plant.PlantState.DIE);
            shootTimer.Stop();

            base.shootTimer_OnMeet(o);
        }
    }
}
