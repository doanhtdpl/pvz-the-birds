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
    public class SunManager : DrawableGameComponent
    {
        // Reference
        protected List<Sun> suns;
        protected Griding.Griding griding;
        // Sun Bank Location
        protected Vector2 sunBankLocation;
        // Fall SunEngine
        protected FallSunEngine fallSunEngine;

        public Griding.Griding SetGriding
        {
            set 
            {
                this.griding = value;
                this.fallSunEngine.SetGriding = value;
            }
        }
        public Vector2 SunBankLocation
        {
            get { return this.sunBankLocation; }
        }

        public SunManager(Game game)
            : base(game)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            suns = new List<Sun>();
            this.sunBankLocation = new Vector2(10f, 0f);
            fallSunEngine = new FallSunEngine(this.Game, this, sunBankLocation);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            AutoRemove();
            List<Sun> sunsCopy = new List<Sun>(suns);
            foreach (Sun sun in sunsCopy)
            {
                sun.Update(gameTime);
            }

            fallSunEngine.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            List<Sun> sunsCopy = new List<Sun>(suns);
            foreach (Sun sun in sunsCopy)
            {
                sun.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        public bool AddSun(Sun sun)
        {
            suns.Add(sun);
            return true;
        }

        public void AutoRemove()
        {
            List<Sun> sunsCopy = new List<Sun>(suns);
            foreach (Sun sun in sunsCopy)
            {
                if (sun.IsDone || sun.IsLost)
                {
                    this.suns.Remove(sun);
                }
            }
        }
    }
}
