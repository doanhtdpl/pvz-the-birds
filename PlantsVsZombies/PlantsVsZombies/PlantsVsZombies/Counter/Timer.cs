using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;

namespace PlantsVsZombies.Counter
{
    public class Timer : GameComponent, ICounter
    {
        #region Fields
        public TimeSpan Interval;

        protected TimeSpan timeCounter;
        #endregion

        #region Properties
        public bool IsMeet { get { return timeCounter >= Interval; } }

        public event EventOnCounterMeet OnMeet;
        #endregion

        #region Constructors
        public Timer(Game game)
            : base(game) { this.Enabled = false; }

        public Timer(Game game, int interval)
            : base(game)
        {
            Interval = TimeSpan.FromMilliseconds(interval);
            this.Enabled = false;
        }

        Timer(Game game, int interval, bool autoStart)
            : base(game)
        {
            Interval = TimeSpan.FromMilliseconds(interval);
            this.Enabled = autoStart;
        }

        public Timer(Timer timer)
            : base(timer.Game)
        {
            this.Enabled = timer.Enabled;
            this.Interval = timer.Interval;
            this.timeCounter = timer.timeCounter;
        }
        #endregion

        #region Methods
        public void Start()
        {
            this.Enabled = true;
        }

        public void Reset()
        {
            timeCounter = TimeSpan.Zero;
        }

        public void Pause()
        {
            this.Enabled = false;
        }

        public void Stop()
        {
            Reset();
            this.Enabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                timeCounter += gameTime.ElapsedGameTime;
                if (IsMeet)
                {
                    OnMeet(this);
                    timeCounter = TimeSpan.Zero;
                }
            }
        }
        #endregion
    }
}
