using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBaseXNA
{
    /// <summary>
    /// The Collidable component
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// Check collide with two Collidable Component
        /// </summary>
        /// <param name="cp">Component to check</param>
        /// <returns>True if they're collided</returns>
        bool Collide(ICollidable collideable);

        /// <summary>
        /// Collided processing
        /// </summary>
        /// <param name="cp">Component collided</param>
        void Collided(ICollidable collideable);

        /// <summary>
        /// Check intersect of component with tree bound
        /// </summary>
        /// <param name="bound">Tree bound</param>
        /// <returns>True if they're intersect</returns>
        bool Intersects(Rectangle bound);

        /// <summary>
        /// Check component are contained in tree bound or not
        /// </summary>
        /// <param name="bound">Tree bound</param>
        /// <returns>True if component are contained in bound</returns>
        bool IsContained(Rectangle bound);

        /// <summary>
        /// Removes all references
        /// </summary>
        void Cleaning();
    }
}
