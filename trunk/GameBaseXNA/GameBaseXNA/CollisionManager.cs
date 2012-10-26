using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBaseXNA
{
    /// <summary>
    /// This class represent a manager of collision of the game
    /// This class use quadtree structure to detect collision of
    /// per two Collidable component and call collided process of
    /// the automatically.
    /// </summary>
    public class CollisionManager : GameComponent
    {
        #region Fields
        /// <summary>
        /// A quadtree use to detect collision
        /// </summary>
        protected CollisionQuadTreeNode CollisionTree { get; set; }

        /// <summary>
        /// Collidable components
        /// </summary>
        protected List<ICollidable> Components { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create new collision manager
        /// </summary>
        /// <param name="game">Current game</param>
        public CollisionManager(Game game, Vector2 Size)
            : base(game)
        {
            this.CollisionTree = new CollisionQuadTreeNode((int) Size.X, (int) Size.Y);
            this.Components = new List<ICollidable>();
        }

        /// <summary>
        /// Create new collision manager
        /// </summary>
        /// <param name="game">Current game</param>
        public CollisionManager(Game game)
            : base(game)
        {
            this.CollisionTree = new CollisionQuadTreeNode(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            this.Components = new List<ICollidable>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a Collidable component to manager
        /// (Note: this component will be added in next Update)
        /// </summary>
        /// <param name="cp">Component</param>
        public void Add(ICollidable cp)
        {
            this.Components.Add(cp);
        }

        /// <summary>
        /// Remove a Collidable component from manager
        /// (Note: this component will be removed valid in next Update)
        /// </summary>
        /// <param name="cp">Component to remove</param>
        public void Remove(ICollidable cp)
        {
            cp.Cleaning();
            this.Components.Remove(cp);
        }

        /// <summary>
        /// Clear all component from manager
        /// (Note: this function will clear all components imediately)
        /// </summary>
        public void Clear()
        {
            this.CollisionTree.Clear();
            this.Components.Clear();
        }

        /// <summary>
        /// Update collision tree and
        /// perform collision detect
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.CollisionTree.Clear();
            foreach (ICollidable cp in this.Components)
            {
                if (!this.CollisionTree.Insert(cp))
                    this.CollisionTree.ImediatelyInsert(cp);
            }
            this.CollisionTree.Optimize();

            this.CollisionTree.CollisionDetect();
        }
        #endregion
    }
}