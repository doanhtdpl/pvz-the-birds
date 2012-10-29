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

namespace PlantsVsZombies.Zombies.Impacts
{
    public interface IZombieImpact : IUpdateable
    {
        #region Fields & Properties
        /// <summary>
        /// Determine when impact completed
        /// </summary>
        bool IsCompleted { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Apply affect to a zombie state
        /// </summary>
        /// <param name="state">Zombie state to apply</param>
        bool Apply(States.ZombieState state);

        /// <summary>
        /// Remove affect from a zombie state
        /// </summary>
        /// <param name="state">Zombie state to remove</param>
        void Remove(States.ZombieState state);

        /// <summary>
        /// When zombie change state, re-apply affect for states
        /// </summary>
        /// <param name="currentState">New state of zombie</param>
        /// <param name="lastState">Last state of zombie</param>
        void ChangeState(States.ZombieState currentState, States.ZombieState lastState);
        #endregion
    }
}