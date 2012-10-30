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
    public class PlantSun : Sun
    {
        // Plant's position
        protected Vector2 position;
        // Destination to sun stop fall
        protected Vector2 destination;
        // Velocity to fall
        protected Vector2 vFall;
        // Velocity to jump
        protected Vector2 vJump;
        protected Vector2 high;
        protected bool isJumpToDest;

        public PlantSun(Game game, Griding.Griding griding, Vector2 bankDirection, Vector2 position)
            : base(game, griding, bankDirection)
        {
            this.position = position;

            animation.PositionX = position.X + 50f;
            animation.PositionY = position.Y;

            SetDestToFall();
            SetDestToJump();
        }

        public override void Initialize()
        {
            base.Initialize();

            vFall = new Vector2(0f, 4f);
            vJump = new Vector2(0f, 3f);
        }

        public override void Update(GameTime gameTime)
        {
            Jump();
            Fall();
            CheckAutoRemove();

            base.Update(gameTime);
        }

        protected void Jump()
        {
            this.animation.Position -= vJump;
            if (isClicked || this.animation.PositionY <= high.Y)
            {
                vJump = Vector2.Zero;
                isJumpToDest = true;
            }
        }

        protected void Fall()
        {
            if(isJumpToDest)
            {
                this.animation.Position += vFall;
                if (isClicked || this.animation.PositionY >= destination.Y)
                {
                    vFall = Vector2.Zero;
                }
            }
        }

        protected void SetDestToFall()
        {
            destination.X = this.position.X + 20f;
            destination.Y = this.position.Y + griding.CellHeight - animation.Bound.Height;
        }

        protected void SetDestToJump()
        {
            this.high = new Vector2(destination.X, destination.Y - 20f);
            isJumpToDest = false;
        }

        protected override void CheckAutoRemove()
        {
            if (isClicked)
                autoremoveTimer.Stop();
            else if (this.animation.PositionY >= destination.Y)
                autoremoveTimer.Start();
        }
    }

    public class PlantSunEngine : SunEngine
    {
        // Plant's position
        public Vector2 Position;
        protected int sunValue;

        public int SunValue { set { this.sunValue = value; } }

        public PlantSunEngine(Game game, SunManager sunManager, Vector2 sunBankLocation, Vector2 position)
            : base(game, sunManager, sunBankLocation)
        {
            this.Position = position;
        }

        public override void Initialize()
        {
            this.sunCreateDelay = 15000;
            base.Initialize();
        }

        public override void AddSun()
        {
            PlantSun plantSun = new PlantSun(this.Game, griding, sunBankLocation, Position);
            plantSun.SunValue = sunValue;
            this.Add(plantSun);
        }
    }
}
