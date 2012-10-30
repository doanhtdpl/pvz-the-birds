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
    public abstract class AttackPlant : Plant
    {
        // Range to attack of Attack Plant, in box
        protected int range;
        // Shoot delay
        protected int shootDelay;
        // Timer for shoot delay
        protected Counter.Timer shootTimer;
        // Bullet Engine of Plant
        protected Bullets.BulletEngine bulletEngine;
        // Position of bullet to fire
        protected Vector2 bulletPosition;

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
        public AttackPlant(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
            this.AddToPlantManager();
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
            AttackDetect();
            PlantOnState();

            shootTimer.Update(gameTime);
            bulletEngine.Update(gameTime);

            base.Update(gameTime);
        }

        // If it detect the enemy in range, set shootTimer to start
        // Else, set it to stop then reset them
        protected virtual void AttackDetect()
        {
            this.ChangeState(PlantState.NORMAL);

            if (this.health <= 0)
            {
                this.ChangeState(PlantState.DIE);
                return;
            }

            if (this.Cell != null)
            {
                Griding.Cell[] line = this.Cell.Line;
                for (int i = (int)this.Cell.Index.X; i < line.Length; ++i)
                {
                    foreach (Griding.IGridable grc in line[i].Components)
                    {
                        if ((grc is Zombies.Zombie) && (grc as Zombies.Zombie).CurrentState != Zombies.Zombie.ZombieState.Death)
                        {
                            this.ChangeState(PlantState.ATTACK);
                            return;
                        }
                    }
                }
            }
        }

        // The method do when time to shoot
        protected virtual void shootTimer_OnMeet(object o)
        {
            AddBullet();
            shootTimer.Stop();
        }

        protected void PlantOnState()
        {
            if (this.plantState == Plant.PlantState.ATTACK)
            {
                if (!shootTimer.Enabled)
                    shootTimer.Start();
            }
        }

        protected override void SetAnimation()
        {
        }

        protected override void SetPosition()
        {
            this.PositionChanged = true;
            Griding.Cell cell = plantManager.GetGriding.IndexOf(this.position);
            if (cell == null)
                return;

            Vector2 pos = new Vector2(cell.Range.Left, cell.Range.Top);
            this.position = pos;
            foreach (Animation ani in animations)
            {
                ani.PositionX = pos.X;
                ani.PositionY = pos.Y + plantManager.GetGriding.CellHeight - CurrentAnimation.Bound.Height;
            }
            CalculateCenter();
            SetBulletPosition();
        }

        protected void AddBullet()
        {
            bulletEngine.AddBullet(this.bulletPosition);
        }

        protected abstract void SetBulletPosition();

        protected override void CalculateCenter()
        {
            this.center.X = this.position.X + this.CurrentAnimation.Bound.Width / 2;
            this.center.Y = this.position.Y + this.CurrentAnimation.Bound.Height;
            base.CalculateCenter();
        }
    }
}
