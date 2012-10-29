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

namespace PlantsVsZombies.Plants_Bullets.Bullets
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Bullet : DrawableGameComponent, Griding.IGridable
    {
        #region Fields

        // Name of Sprite or Animation
        protected string BName;

        // Name of Sprite effect
        protected string B_Effect;

        // Effect when collision happen
        protected Effect effect;

        // True if collision happen
        protected bool isCollided;

        // Damage of bullet
        protected int damage;

        // Position of bullet
        protected Vector2 position;

        #endregion

        #region Properties

        public abstract Rectangle Bound
        {
            get;
        }

        public bool IsCollided
        {
            get { return this.isCollided; }
        }

        public int Damage
        {
            get { return this.damage; }
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 GridPosition
        {
            get { return this.position; }
        }

        public bool PositionChanged { get; set; }

        public Griding.Cell Cell { get; set; }

        #endregion

        public Bullet(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
            this.Initialize();
        }

        // Init the bullet
        public override void Initialize()
        {
            isCollided = false;
            base.Initialize();
        }

        // Allows the game component to update itself.
        public override void Update(GameTime gameTime)
        {
            CollisionDetect();

            base.Update(gameTime);
        }

        // Detect the collision of bullet with enemy
        protected virtual void CollisionDetect()
        {
            if (this.Cell == null)
                return;

            foreach (Griding.IGridable grc in this.Cell.Components)
            {
                Zombies.Zombie zombie = grc as Zombies.Zombie;
                if (zombie != null)
                {
                    if (this.Bound.Intersects(zombie.CurrentZombieState.Image.Bound))
                        this.Collided(zombie);
                }
            }
        }

        // Do something when collision happened
        protected virtual void Collided(Zombies.Zombie zombie)
        {
            this.isCollided = true;
            Zombies.Impacts.Damaging dam = new Zombies.Impacts.Damaging(this.Game, this.damage);
            zombie.AddImpact(dam);
        }

        // Allow it auto remove itself
        public virtual void AutoRemove()
        {
        }

    }
}
