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

        private int numberOfSuns = 0;
        public delegate void OnSunChangedProc();
        public event OnSunChangedProc OnSunChanged;
        public int NumberOfSuns
        {
            get
            {
                return this.numberOfSuns;
            }
            set
            {
                this.numberOfSuns = value;
                if (this.numberOfSuns < 0)
                    this.numberOfSuns = 0;
                if (this.numberOfSuns > 9999)
                    this.numberOfSuns = 9999;

                if (this.OnSunChanged != null)
                    OnSunChanged();
                
            }
        }

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
            set { this.sunBankLocation = value; }
        }

        public SunManager(Game game)
            : base(game)
        {
            this.NumberOfSuns = 0;
            this.Initialize();
        }

        public override void Initialize()
        {
            suns = new List<Sun>();
            this.sunBankLocation = new Vector2(10f, 0f);
            fallSunEngine = new FallSunEngine(this.Game, this);
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
                    if (sun.IsDone)
                        this.NumberOfSuns += sun.SunValue;

                    this.suns.Remove(sun);
                }
            }
        }
    }
}
