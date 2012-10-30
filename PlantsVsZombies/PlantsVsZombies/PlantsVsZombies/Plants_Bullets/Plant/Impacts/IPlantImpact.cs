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

namespace PlantsVsZombies.Plants_Bullets.Plant.Impacts
{
    public interface IPlantImpact : IUpdateable
    {
        #region Fields & Properties
        /// <summary>
        /// Determine when impact completed
        /// </summary>
        bool IsCompleted { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Apply affect to a plant
        /// </summary>
        /// <param name="plant">Plant to apply</param>
        bool Apply(Plant plant);

        /// <summary>
        /// Remove affect from a plant
        /// </summary>
        /// <param name="plant">Plant to remove</param>
        void Remove(Plant plant);
        #endregion
    }
}
