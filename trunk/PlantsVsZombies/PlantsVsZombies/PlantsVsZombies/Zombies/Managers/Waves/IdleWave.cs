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
    public class IdleWave : GameComponent, IZombieWave
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

        public Counter.Timer WaveTimer { get; set; }
        #endregion

        #region Constructors
        public IdleWave(ZombiesManager manager)
            : base(manager.Game)
        {
            this.ZombiesManager = manager;
            this.WaveTimer = new Counter.Timer(manager.Game, 0);
            this.WaveTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnWaveTimerTick);
            this.IsCompleted = false;
        }

        public IdleWave(ZombiesManager manager, int idleTime)
            : base(manager.Game)
        {
            this.ZombiesManager = manager;
            this.WaveTimer = new Counter.Timer(manager.Game, idleTime);
            this.WaveTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnWaveTimerTick);
            this.IsCompleted = false;
        }

        public IdleWave(IdleWave idle)
            : base(idle.Game)
        {
            this.ZombiesManager = idle.ZombiesManager;
            this.WaveTimer = new Counter.Timer(idle.WaveTimer);
            this.WaveTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnWaveTimerTick);
            this.IsCompleted = idle.IsCompleted;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            this.WaveTimer.Update(gameTime);

            base.Update(gameTime);
        }

        public virtual void Begin()
        {
            this.WaveTimer.Start();
        }

        public virtual void End()
        {
            this.WaveTimer.Stop();
        }

        public virtual void Reset()
        {
            this.WaveTimer.Reset();
        }

        public void OnWaveTimerTick(Counter.ICounter timer)
        {
            this.IsCompleted = true;
        }
        #endregion
    }
}
