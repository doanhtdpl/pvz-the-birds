using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;

namespace PlantsVsZombies.Plants_Bullets.Plant
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Plant : DrawableGameComponent, Griding.IGridable
    {
        public enum PlantState
        {
            NORMAL,
            ATTACK,
            SUPPORT,
            DIE,
        }

        #region Fields

        // List animation
        protected List<Animation> animations;
        // Current Animation
        protected Animation currentAnimation;
        // Plant state
        protected PlantState plantState;
        // Variable report when plant is attacked by enemy
        protected bool isAttacked;

        // Health of plant
        int health;

        // Reference to Plant Manager
        protected PlantManager plantManager;

        #endregion

        #region Properties

        public bool IsAttacked
        {
            get { return this.isAttacked; }
        }
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public Vector2 GridPosition
        {
            get { return Vector2.Zero; }
        }
        public bool PositionChanged
        {
            get;
            set;
        }
        public Griding.Cell Cell
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public Plant(Game game, PlantManager plantManager)
            : base(game)
        {
            this.plantManager = plantManager;
            this.Initialize();
        }
        
        // Initialize the plant fields
        public override void Initialize()
        {
            animations = new List<Animation>();
            plantState = PlantState.NORMAL;
            isAttacked = false;

            SetAnimation();
            base.Initialize();
        }

        #endregion

        // Update the plant
        public override void Update(GameTime gameTime)
        {
            CollisionDetect();
            Flash();

            currentAnimation.Update(gameTime);
            base.Update(gameTime);
        }

        // Draw the plant on screen
        public override void Draw(GameTime gameTime)
        {
            currentAnimation.Draw(gameTime);
            base.Draw(gameTime);
        }

        // Change plant state
        protected virtual void ChangeState(PlantState plantState)
        {
            this.plantState = plantState;
        }

        // Decrease plant's health
        public void DecreaseHP(int HP)
        {
            this.health -= HP;
        }

        // Allow flash plant when enemy hit plant
        public virtual void Flash()
        {
            if (this.isAttacked)
                this.currentAnimation.ColorA = 100;
            else
                this.currentAnimation.ColorA = 255;
        }

        // Detect the collision between plant and enemy
        public virtual void CollisionDetect()
        {
            // Just for test
            if (GMouse.MousePosition.X >= currentAnimation.Bound.Left &&
                GMouse.MousePosition.X <= currentAnimation.Bound.Right &&
                GMouse.MousePosition.Y <= currentAnimation.Bound.Bottom &&
                GMouse.MousePosition.Y >= currentAnimation.Bound.Top)
            {
                this.isAttacked = true;
            }
            else
                this.isAttacked = false;
        }

        protected abstract void SetAnimation();
    }
}
