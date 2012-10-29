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
    public class ZombieGenerator : GameComponent, IZombieWave
    {
        #region Fields & Properties
        public List<IZombieWave> Waves { get; set; }

        private int currentWave = -1;
        public int CurrentWave
        {
            get
            {
                return this.currentWave;
            }

            set
            {
                IZombieWave lastWave = this.CurrentZombieWave;
                currentWave = value;
                this.OnWaveChange(this.CurrentZombieWave, lastWave);
            }
        }

        public IZombieWave CurrentZombieWave
        {
            get
            {
                if ((0 <= this.currentWave) && (this.currentWave <= this.Waves.Count))
                    return (this.Waves[this.currentWave]);

                return null;
            }
        }

        #region IZombieWave
        public bool IsCompleted { get { return (this.currentWave >= this.Waves.Count); } }

        public ZombiesManager ZombiesManager { get; set; }
        #endregion
        #endregion

        #region Constructors
        public ZombieGenerator(ZombiesManager manager)
            : base(manager.Game)
        {
            this.ZombiesManager = manager;
            this.Waves = new List<IZombieWave>();
            this.currentWave = -1;
        }

        public ZombieGenerator(ZombiesManager manager, List<IZombieWave> waves)
            : base(manager.Game)
        {
            this.ZombiesManager = manager;
            this.currentWave = -1;
            this.Waves = waves;
        }

        public ZombieGenerator(ZombieGenerator Zgen)
            : base(Zgen.Game)
        {
            this.ZombiesManager = Zgen.ZombiesManager;
            this.currentWave = -1;

            this.Waves = new List<IZombieWave>();
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (this.CurrentZombieWave != null)
            {
                this.CurrentZombieWave.Update(gameTime);

                if (this.CurrentZombieWave.IsCompleted)
                    this.CurrentWave = this.CurrentWave + 1;
            }

            base.Update(gameTime);
        }

        public virtual void OnWaveChange(IZombieWave currentWave, IZombieWave lastWave)
        {
            if (lastWave != null)
                lastWave.End();

            if (currentWave != null)
                currentWave.Begin();
        }

        public virtual void Begin()
        {
            this.CurrentWave = 0;
        }

        public virtual void End()
        {

        }

        public virtual void Reset()
        {
            this.CurrentWave = 0;
            foreach (IZombieWave wave in this.Waves)
            {
                wave.Reset();
            }
        }
        #endregion
    }
}
