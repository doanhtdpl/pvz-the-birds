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
    public class AttackPlant : Plant
    {
        // Range to attack of Attack Plant, in box
        protected int range;
        // Shoot delay
        protected int shootDelay;
        // Timer for shoot delay
        protected Counter.Timer shootTimer;
        // Bullet Engine of Plant
        protected Bullets.BulletEngine bulletEngine;

        // Properties
        public int Range
        {
            get { return this.range; }
            set { this.range = value; }
        }

        public int ShootDelay
        {
            get { return this.shootDelay; }
            set { this.shootDelay = value; }
        }

        public Bullets.BulletEngine BulletEngine
        {
            get { return this.bulletEngine; }
        }

        // Constructor
        public AttackPlant(Game game, PlantManager plantManager)
            : base(game, plantManager)
        {
        }

        public override void Initialize()
        {
            shootTimer = new Counter.Timer(this.Game, shootDelay);
            shootTimer.OnMeet += new Counter.EventOnCounterMeet(shootTimer_OnMeet);

            base.Initialize();
        }

        // Update allow check RangeDetect
        public override void Update(GameTime gameTime)
        {
            RangeDetect();

            bulletEngine.Update(gameTime);
            base.Update(gameTime);
        }

        // If it detect the enemy in range, set shootTimer to start
        // Else, set it to stop then reset them
        protected virtual void RangeDetect()
        {
            if(GMouse.MousePosition.X >= this.currentAnimation.Position.X &&
                GMouse.MousePosition.X <= this.currentAnimation.Position.X + range * 60 &&
                GMouse.MousePosition.Y >= this.currentAnimation.Position.Y &&
                GMouse.MousePosition.Y <= this.currentAnimation.Position.Y + 60)
            {
                shootTimer.Start();
                ChangeState(Plant.PlantState.ATTACK);
            }
            else
            {
                ChangeState(Plant.PlantState.NORMAL);
                shootTimer.Stop();
                shootTimer.Reset();
            }
        }

        // The method do when time to shoot
        protected virtual void shootTimer_OnMeet(object o)
        {
            shootTimer.Reset();
        }

        protected override void SetAnimation()
        {
        }
    }
}
