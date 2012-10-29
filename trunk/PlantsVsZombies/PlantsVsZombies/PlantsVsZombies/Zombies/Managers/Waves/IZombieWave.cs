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
    public interface IZombieWave : IUpdateable
    {
        #region Fields & Properties
        /// <summary>
        /// Determine when the wave is completed
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// The zombie manager of wave
        /// </summary>
        ZombiesManager ZombiesManager { get; }

        /// <summary>
        /// Start wave
        /// </summary>
        void Begin();

        /// <summary>
        /// End wave
        /// </summary>
        void End();

        /// <summary>
        /// Reset wave
        /// </summary>
        void Reset();
        #endregion
    }
}
