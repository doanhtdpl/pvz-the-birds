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
    public class FallSun : Sun
    {
        // Destination to sun stop fall
        protected Vector2 destination;
        // Velocity to fall
        protected Vector2 vFall;


        public FallSun(Game game, Griding.Griding griding, Vector2 bankDirection)
            : base(game, griding, bankDirection)
        {
            this.sunValue = 35;
        }

        public override void Initialize()
        {
            base.Initialize();

            Rectangle rect = griding.Range;

            animation.PositionX = GRandom.RandomFloat(rect.X + 10f, rect.X + rect.Width - animation.Bound.Width);
            animation.PositionY = -5f;

            destination.X = animation.PositionX;
            destination.Y = GRandom.RandomFloat(rect.Y, rect.Y + rect.Height);
            vFall = new Vector2(0f, 2f);

            SetDestToFall();
        }

        public override void Update(GameTime gameTime)
        {
            Fall();
            CheckAutoRemove();

            base.Update(gameTime);
        }

        protected void Fall()
        {
            this.animation.Position += vFall;
            if (isClicked || this.animation.PositionY >= destination.Y)
            {
                vFall = Vector2.Zero;
            }
        }

        protected void SetDestToFall()
        {
            Griding.Cell cell = griding.IndexOf(destination);
            destination.X = cell.Range.X;
            destination.Y = cell.Range.Y + griding.CellHeight - animation.Bound.Height;
        }

        protected override void CheckAutoRemove()
        {
            if (isClicked)
                autoremoveTimer.Stop();
            else if (this.animation.PositionY >= destination.Y)
                    autoremoveTimer.Start();
        }
    }

    public class FallSunEngine : SunEngine
    {
        public FallSunEngine(Game game, SunManager sunManager)
            : base(game, sunManager, sunManager.SunBankLocation)
        {
        }

        public override void Initialize()
        {
            this.sunCreateDelay = 10000;
            base.Initialize();
        }

        public override void AddSun()
        {
            this.Add(new FallSun(this.Game, griding, sunBankLocation));
        }
    }
}
