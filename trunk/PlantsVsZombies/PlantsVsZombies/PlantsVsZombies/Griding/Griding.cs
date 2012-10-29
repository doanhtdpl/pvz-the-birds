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
    /// Describe grid, contains components as IGridable
    /// and divide its components by cells
    /// </summary>
    public class Griding :
        GameComponent
    {
        #region Fields
        protected Rectangle range;

        private int cellWidth = 0, cellHeight = 0;
        private int nRows = 0, nColumns = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Grid components
        /// </summary>
        List<IGridable> Components { get; set; }

        /// <summary>
        /// Gird is divided by cells
        /// </summary>
        public Cell[,] Grid { get; set; }

        /// <summary>
        /// Range of grid
        /// </summary>
        public Rectangle Range { get { return range; } }

        /// <summary>
        /// Cell width
        /// </summary>
        public int CellWidth { get { return cellWidth; } }
        /// <summary>
        /// Cell height
        /// </summary>
        public int CellHeight { get { return cellHeight; } }

        /// <summary>
        /// Number of rows of grid
        /// </summary>
        public int NumberOfRows { get { return nRows; } }
        /// <summary>
        /// Number of columns of grid
        /// </summary>
        public int NumberOfColumns { get { return nColumns; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Create an empty grid
        /// </summary>
        public Griding(Game game)
            : base(game)
        {
            this.Components = new List<IGridable>();
            this.range = new Rectangle();
            Grid = new Cell[0, 0];

        }
        /// <summary>
        /// Create grid
        /// </summary>
        /// <param name="range">Range</param>
        /// <param name="nRows">Number of rows</param>
        /// <param name="nColumns">Number of columns</param>
        public Griding(Game game, Rectangle range, int nRows, int nColumns)
            : base(game)
        {
            this.Components = new List<IGridable>();
            this.range = range;
            Grid = new Cell[nRows, nColumns];
            this.nRows = nRows;
            this.nColumns = nColumns;

            this.Divide();
        }
        #endregion

        #region Methods
        protected virtual void Divide()
        {
            this.cellWidth = (int)(this.Range.Width / this.nColumns);
            this.cellHeight = (int)(this.Range.Height / this.nRows);
            this.range.Width -= this.range.Width % cellWidth;
            this.range.Height -= this.range.Height % cellHeight;

            for (int i = 0; i < nRows; ++i)
            {
                for (int j = 0; j < nColumns; ++j)
                {
                    Grid[i, j] = new Cell();
                    Grid[i, j].Index = new Vector2(i, j);
                    Grid[i, j].Range = new Rectangle(j * cellWidth + this.range.X, i * cellHeight + this.range.Y, cellWidth, cellHeight);
                }
            }
        }

        /// <summary>
        /// Return cell that contains specified position
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>Cell contains position</returns>
        public virtual Cell IndexOf(Vector2 position)
        {
            if (!GMath.IsContained(position, this.Range))
                return null;

            return this.Grid[(int )(position.Y / cellHeight), (int) (position.X / cellWidth)];
        }

        /// <summary>
        /// Return cell that contains specified grid component
        /// </summary>
        /// <param name="gr"></param>
        /// <returns></returns>
        public virtual Cell IndexOf(IGridable gr)
        {
            return IndexOf(gr.GridPosition);
        }

        /// <summary>
        /// Return cell that contains specified range (contains midpoint)
        /// </summary>
        /// <param name="range">Range</param>
        /// <returns>Cell contains range</returns>
        public virtual Cell IndexOf(Rectangle range)
        {
            Vector2 center = new Vector2(range.X + range.Width / 2, range.Y + range.Height / 2);

            return IndexOf(center);
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Components.Count;)
            {
                if (this.Components[i].PositionChanged)
                {
                    Cell newCell = this.IndexOf(this.Components[i].GridPosition);
                    if (newCell != null)
                    {
                        if (newCell != this.Components[i].Cell)
                        {
                            this.Components[i].Cell.Components.Remove(this.Components[i]);
                            newCell.Components.Add(this.Components[i]);
                            this.Components[i].Cell = newCell;
                        }

                        ++i;
                    }
                    else
                        this.Components.RemoveAt(i);
                }
                else
                    ++i;
            }

            base.Update(gameTime);
        }

        public virtual void Add(IGridable gr)
        {
            Cell cell = this.IndexOf(gr.GridPosition);

            if (cell != null)
            {
                this.Components.Add(gr);
                cell.Components.Add(gr);
                gr.Cell = cell;
            }
        }

        public virtual void Remove(IGridable gr)
        {
            if (gr.Cell != null)
                gr.Cell.Components.Remove(gr);

            Components.Remove(gr);
        }
        #endregion
    }
}
