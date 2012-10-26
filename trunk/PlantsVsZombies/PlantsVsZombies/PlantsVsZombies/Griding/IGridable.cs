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

namespace PlantsVsZombies.Griding
{
    /// <summary>
    /// Interface: Implements grid components
    /// </summary>
    public interface IGridable
    {
        #region Properties
        /// <summary>
        /// Component's position
        /// </summary>
        Vector2 GridPosition { get; }

        /// <summary>
        /// True if component position is changed
        /// The grid will automatically change its cell
        /// </summary>
        bool PositionChanged { get; }

        /// <summary>
        /// Cell of component
        /// </summary>
        Cell Cell { get; set; }
        #endregion
    }
}