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
        //Impacts
        public List<Impacts.IPlantImpact> Impacts { get; set; }

        // List animation
        protected List<Animation> animations;
        // Center of plant
        protected Vector2 center;
        // Current Animation
        public Animation CurrentAnimation { get; set; }
        // Plant state
        protected PlantState plantState;
        // Variable report when plant is attacked by enemy
        // Position
        protected Vector2 position;

        // Health of plant
        protected int health;

        // Reference to Plant Manager
        protected PlantManager plantManager;

        #endregion

        #region Properties

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
        public virtual Vector2 Position
        {
            get { return this.CurrentAnimation.Position; }
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
            get { return this.CurrentAnimation.Bound; }
        }

        #endregion

        #region Constructor
        public Plant(Game game, PlantManager plantManager, Vector2 position)
            : base(game)
        {
            this.plantManager = plantManager;
            this.position = position;
            this.Impacts = new List<Impacts.IPlantImpact>();
            this.Initialize();
        }
        
        // Initialize the plant fields
        public override void Initialize()
        {
            animations = new List<Animation>();
            plantState = PlantState.NORMAL;
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
            CurrentAnimation.Update(gameTime);

            for (int i = 0; i < this.Impacts.Count; )
            {
                if (this.Impacts[i].IsCompleted)
                {
                    this.Impacts[i].Remove(this);
                    this.Impacts.RemoveAt(i);
                }
                else
                {
                    this.Impacts[i].Update(gameTime);
                    ++i;
                }
            }

            base.Update(gameTime);
        }

        public virtual void AddImpact(Impacts.IPlantImpact impact)
        {
            if (impact.Apply(this))
                this.Impacts.Add(impact);
        }

        // Draw the plant on screen
        public override void Draw(GameTime gameTime)
        {
            CurrentAnimation.Draw(gameTime);
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
