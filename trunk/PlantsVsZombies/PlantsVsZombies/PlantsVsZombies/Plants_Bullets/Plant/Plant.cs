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
        // Center of plant
        protected Vector2 center;
        // Current Animation
        protected Animation currentAnimation;
        // Plant state
        protected PlantState plantState;
        // Variable report when plant is attacked by enemy
        protected bool isAttacked;
        // Position
        protected Vector2 position;

        // Health of plant
        protected int health;

        // Reference to Plant Manager
        protected PlantManager plantManager;

        #endregion

        #region Properties

        public bool IsAttacked { get { return this.isAttacked; }  }

        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public bool IsDead
        {
            get 
            {
                if (this.plantState == PlantState.DIE)
                    return true;
                else
                    return false;
            }
        }

        // Position changed
        public Vector2 Position
        {
            get { return this.currentAnimation.Position; }
            set
            {
                this.PositionChanged = true;
                this.position = value;
                this.SetPosition();
            }
        }

        public Vector2 GridPosition { get  { return center;  }  }

        public bool PositionChanged {  get; set; }

        public Griding.Cell Cell {  get; set; }

        public Rectangle Bound
        {
            get { return this.currentAnimation.Bound; }
        }

        #endregion

        #region Constructor
        public Plant(Game game, PlantManager plantManager, Vector2 position)
            : base(game)
        {
            this.plantManager = plantManager;
            this.position = position;
            this.Initialize();
        }
        
        // Initialize the plant fields
        public override void Initialize()
        {
            animations = new List<Animation>();
            plantState = PlantState.NORMAL;
            isAttacked = false;
            health = 100;

            SetAnimation();
            SetPosition();
            base.Initialize();
        }

        #endregion

        #region Methods
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
            this.isAttacked = false;
        }

        protected abstract void SetAnimation();

        protected abstract void SetPosition();

        protected void AddToPlantManager()
        {
            this.plantManager.AddPlant(this);
        }

        protected virtual void CalculateCenter()
        {
        }

        #endregion
    }
}
