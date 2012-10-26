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

namespace PlantsVsZombies.Zombies.States
{
    public class Death : ZombieState
    {
        #region Properties
        bool IsComplete { get; set; }

        public Counter.Timer Timer { get; set; }
        #endregion

        #region Constructors
        public Death(Animation image)
            : base(image)
        {
            this.IsComplete = false;
            Timer = new Counter.Timer(this.Game, int.MaxValue);
        }

        public Death(Animation image, int timeToLive)
            : base(image)
        {
            this.IsComplete = false;
            Timer = new Counter.Timer(this.Game, timeToLive);
        }

        public Death(Animation image, Counter.Timer timer)
            : base(image)
        {
            this.IsComplete = false;
            this.Timer = timer;
        }

        public Death(Death death)
            : base(death)
        {
            this.IsComplete = death.IsComplete;
            Timer = new Counter.Timer(death.Timer);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Timer.Update(gameTime);

            if (this.Image.CurrentFrame != this.Image.Frames.Count - 1)
                this.Image.Update(gameTime);

            if (this.Timer.IsMeet)
                this.IsComplete = true;
        }
        #endregion
    }
}
