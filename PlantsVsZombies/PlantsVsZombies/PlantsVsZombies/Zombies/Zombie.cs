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
                if (value != currentState)
                {
                    States.ZombieState lastState = this.CurrentZombieState;
                    Vector2 lastPos = this.Position;
                    lastState.End();
                    this.currentState = value;
                    this.CurrentZombieState.Start();
                    this.Position = lastPos;

                    foreach (Impacts.IZombieImpact impact in this.Impacts)
                    {
                        impact.ChangeState(this.CurrentZombieState, lastState);
                    }
                }
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

        public Vector2 Position
        {
            get
            {
                return (CurrentZombieState.Image.Position + CurrentZombieState.Align);
            }
            set
            {
                CurrentZombieState.Image.Position = value - CurrentZombieState.Align;
            }
        }

        public int LP { get; set; }
        public bool NeedRemove { get { return ((this.currentState == ZombieState.Death) && (this.Death.IsComplete)); } }
        public List<Impacts.IZombieImpact> Impacts { get; set; }
        #endregion

        #region IGridable
        public Vector2 GridPosition { get { return this.Position; } }
        public bool PositionChanged { get { return (this.CurrentState == ZombieState.Walk); } }
        public Griding.Cell Cell { get; set; }
        #endregion

        #region Constructors
        public Zombie(Game game)
            : base(game)
        {
            this.Impacts = new List<Impacts.IZombieImpact>();
            this.currentState = ZombieState.Walk;
            this.LP = 100;
        }

        public Zombie(Zombie zombie)
            : base(zombie.Game)
        {
            this.currentState = zombie.currentState;

            this.Walk = new States.Walk(zombie.Walk);
            this.Attack = new States.Attack(zombie.Attack);
            this.Death = new States.Death(zombie.Death);
            this.LP = zombie.LP;
            this.Impacts = new List<Impacts.IZombieImpact>();
        }

        public override void Draw(GameTime gameTime)
        {
            this.CurrentZombieState.Draw(gameTime);

//             SpriteBatch sprBatch = (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
//             SpriteFont font = (SpriteFont)this.Game.Services.GetService(typeof(SpriteFont));
// 
//             sprBatch.Begin();
//             sprBatch.DrawString(font, String.Concat(this.Cell.Index.X, ", ", this.Cell.Index.Y), this.Position, Color.White);
//             sprBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.CurrentZombieState.Update(gameTime);

            for (int i = 0; i < this.Impacts.Count;)
            {
                if (this.Impacts[i].IsCompleted)
                {
                    this.Impacts[i].Remove(this.CurrentZombieState);
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

        public virtual void AddImpact(Impacts.IZombieImpact impact)
        {
            if (impact.Apply(this.CurrentZombieState))
                this.Impacts.Add(impact);
        }
        #endregion
    }
}