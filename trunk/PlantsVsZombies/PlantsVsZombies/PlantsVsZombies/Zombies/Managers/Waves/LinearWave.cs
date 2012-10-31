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
    public abstract class LinearWave : GameComponent, IZombieWave
    {
        #region Fields & Properties
        /// <summary>
        /// Determine when the wave is completed
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// The zombie manager of wave
        /// </summary>
        public ZombiesManager ZombiesManager { get; set; }

        protected Counter.Timer generateTimer;
        protected Counter.Timer totalTimer;
        protected int nCreatedZombie = 0, maxZombie = 0;
        #endregion

        #region Constructors
        public LinearWave(ZombiesManager manager)
            : base(manager.Game)
        {
            this.ZombiesManager = manager;
            this.IsCompleted = false;
            this.generateTimer = new Counter.Timer(manager.Game, int.MaxValue);
            this.totalTimer = new Counter.Timer(manager.Game, 0);

            this.generateTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnGenerateTimerTick);
            this.totalTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnTotalTimerTick);
        }

        public LinearWave(ZombiesManager manager, int numZombie, int createDelay, int time)
            : base(manager.Game)
        {
            this.ZombiesManager = manager;
            this.IsCompleted = false;
            this.generateTimer = new Counter.Timer(manager.Game, createDelay);
            this.totalTimer = new Counter.Timer(manager.Game, time);
            maxZombie = numZombie;

            this.generateTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnGenerateTimerTick);
            this.totalTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnTotalTimerTick);
        }

        public LinearWave(LinearWave lnWave)
            : base(lnWave.Game)
        {
            this.ZombiesManager = lnWave.ZombiesManager;
            this.IsCompleted = lnWave.IsCompleted;
            this.generateTimer = new Counter.Timer(lnWave.generateTimer);
            this.totalTimer = new Counter.Timer(lnWave.totalTimer);

            this.generateTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnGenerateTimerTick);
            this.totalTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnTotalTimerTick);
        }
        #endregion

        #region Methods
        protected virtual void OnTotalTimerTick(Counter.ICounter timer)
        {
            this.IsCompleted = true;
        }

        protected virtual void OnGenerateTimerTick(Counter.ICounter timer)
        {
            ++nCreatedZombie;
            if (this.nCreatedZombie < this.maxZombie)
                this.Generate();
        }

        public abstract Zombie Generate();

        public override void Update(GameTime gameTime)
        {
            generateTimer.Update(gameTime);
            totalTimer.Update(gameTime);

            base.Update(gameTime);
        }

        public virtual void Begin()
        {
            generateTimer.Start(true);
            totalTimer.Start();
        }

        public virtual void End()
        {
            generateTimer.Stop();
            totalTimer.Stop();
        }

        public virtual void Reset()
        {
            generateTimer.Reset();
            totalTimer.Reset();

            this.nCreatedZombie = 0;
        }
        #endregion
    }
}
