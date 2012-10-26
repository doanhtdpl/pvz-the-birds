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
using PlantsVsZombies.Griding;

namespace PlantsVsZombies.Zombies
{
    public class Zombie : DrawableGameComponent, IGridable
    {
        public enum ZombieState
        {
            Walk = 0,
            Attack = 1,
            Death = 2
        }

        #region Properties
        public ZombieState currentState;
        public ZombieState CurrentState
        {
            get
            {
                return this.currentState;
            }
            set
            {
                this.CurrentZombieState.Image.Reset();
                this.currentState = value;
            }
        }
        public States.ZombieState CurrentZombieState
        {
            get
            {
                switch (currentState)
                {
                    case ZombieState.Walk:
                        return this.Walk;
                    case ZombieState.Attack:
                        return this.Attack;
                    case ZombieState.Death:
                        return this.Death;
                    default:
                        return null;
                }
            }
        }

        public States.Attack Attack { get; set; }
        public States.Walk Walk { get; set; }
        public States.Death Death { get; set; }

        public int LP { get; set; }
        public bool IsDie { get { return this.LP <= 0; } }
        #endregion

        #region IGridable
        public Vector2 GridPosition { get { return new Vector2(this.CurrentZombieState.Image.Bound.Left, this.CurrentZombieState.Image.Bound.Bottom); } }
        public bool PositionChanged { get; set; }
        public Griding.Cell Cell { get; set; }
        #endregion

        #region Constructors
        public Zombie(Game game)
            : base(game)
        {
            this.currentState = ZombieState.Walk;
            this.PositionChanged = false;
        }

        public Zombie(Zombie zombie)
            : base(zombie.Game)
        {
            this.currentState = zombie.currentState;

            this.Walk = new States.Walk(zombie.Walk);
            this.Attack = new States.Attack(zombie.Attack);
            this.Death = new States.Death(zombie.Death);
            this.PositionChanged = false;
        }

        public override void Draw(GameTime gameTime)
        {
            this.CurrentZombieState.Draw(gameTime);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.CurrentZombieState.Update(gameTime);

            base.Draw(gameTime);
        }
        #endregion
    }
}