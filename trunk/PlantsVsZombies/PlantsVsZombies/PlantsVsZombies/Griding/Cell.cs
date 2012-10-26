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
    /// Implements cell used by grid
    /// </summary>
    public class Cell
    {
        #region Fields
        /// <summary>
        /// Range of Cell
        /// </summary>
        public Rectangle Range;

        /// <summary>
        /// Index of cell in grid
        /// </summary>
        public Vector2 Index;

        /// <summary>
        /// Components of cell
        /// </summary>
        public List<IGridable> Components;

        public Cell[] Line
        {
            get
            {
                Cell[] line = new Cell[this.Grid.NumberOfColumns];
                for (int i = 0; i < this.Grid.NumberOfColumns; ++i)
                {
                    line[i] = this.Grid.Grid[(int) this.Index.Y, i];
                }

                return line;
            }
        }

        public Cell[] Columns
        {
            get
            {
                Cell[] columns = new Cell[this.Grid.NumberOfColumns];
                for (int i = 0; i < this.Grid.NumberOfColumns; ++i)
                {
                    columns[i] = this.Grid.Grid[i, (int)this.Index.X];
                }

                return columns;
            }
        }

        protected Griding Grid { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// Size of cell
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(this.Range.Width, this.Range.Height);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create empty cell
        /// </summary>
        public Cell()
        {
            this.Components = new List<IGridable>();
            this.Range = new Rectangle();
            this.Index = new Vector2(-1f, -1f);
        }

        /// <summary>
        /// Create cell of grid
        /// </summary>
        /// <param name="left">Left bound</param>
        /// <param name="top">Top bound</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="index">Location of cell in grid</param>
        public Cell(Griding grid, int left, int top, int width, int height, Vector2 index)
        {
            this.Components = new List<IGridable>();
            this.Grid = grid;
            this.Range = new Rectangle(left, top, width, height);
            this.Index = index;
        }
        #endregion

        #region Methods
        #endregion
    }
}
