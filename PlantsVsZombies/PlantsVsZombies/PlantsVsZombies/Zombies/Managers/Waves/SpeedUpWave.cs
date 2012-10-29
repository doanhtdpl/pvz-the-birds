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

namespace PlantsVsZombies.Zombies.Managers.Waves
{
    public abstract class SpeedUpWave : LinearWave
    {
        #region Fields & Properties
        protected int speedUp = 0;
        #endregion

        #region Constructors
        public SpeedUpWave(ZombiesManager manager)
            : base(manager)
        {
            this.speedUp = 0;
        }

        public SpeedUpWave(ZombiesManager manager, int numZombie, int initDelay, int decreaseDelay, int time)
            : base(manager, numZombie, initDelay, time)
        {
            this.speedUp = decreaseDelay;
        }

        public SpeedUpWave(SpeedUpWave lnWave)
            : base(lnWave)
        {
            this.speedUp = lnWave.speedUp;
        }
        #endregion

        #region Methods
        protected override void OnGenerateTimerTick(Counter.ICounter timer)
        {
            base.OnGenerateTimerTick(timer);

            this.generateTimer.Interval -= TimeSpan.FromMilliseconds(speedUp);
        }
        #endregion
    }
}