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
    public abstract class SunEngine : GameComponent
    {
        // Fields
        protected SunManager sunManager;
        protected Griding.Griding griding;
        protected Vector2 sunBankLocation;

        // Timer to create sun
        protected int sunCreateDelay;
        protected Counter.Timer sunCreateTimer;

        public Griding.Griding SetGriding
        {
            set { this.griding = value; }
        }

        public SunEngine(Game game, SunManager sunManager, Vector2 sunBankLocation)
            : base(game)
        {
            this.sunManager = sunManager;
            this.sunBankLocation = sunBankLocation;
            this.Initialize();
        }

        public override void Initialize()
        {
            sunCreateTimer = new Counter.Timer(this.Game, sunCreateDelay);
            sunCreateTimer.OnMeet += new Counter.EventOnCounterMeet(sunCreateTimer_OnMeet);
            sunCreateTimer.Start();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            sunCreateTimer.Update(gameTime);
            base.Update(gameTime);
        }

        public abstract void AddSun();

        public virtual void Add(Sun sun)
        {
            this.sunManager.AddSun(sun);
        }

        public virtual void sunCreateTimer_OnMeet(object o)
        {
            this.AddSun();
            sunCreateTimer.Reset();
        }

        public void Stop()
        {
            sunCreateTimer.Stop();
        }
    }
}
