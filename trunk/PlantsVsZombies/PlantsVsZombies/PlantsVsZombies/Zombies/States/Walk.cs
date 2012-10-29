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

namespace PlantsVsZombies.Zombies.States
{
    public class Walk : ZombieState
    {
        #region Properties
        public float Velocity { get; set; }
        #endregion

        #region Constructors
        public Walk(Zombie zombie)
            : base(zombie)
        {
            this.Velocity = 0f;
        }

        public Walk(Zombie zombie, Animation image, float velocity)
            : base(zombie, image)
        {
            this.Velocity = velocity;
        }

        public Walk(Walk walk)
            : base(walk)
        {
            this.Velocity = walk.Velocity;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Vector2 position = this.Zombie.Position;
            position.X -= Velocity;
            this.Zombie.Position = position;

            base.Update(gameTime);
        }

        public override void CheckingState()
        {
            if (Zombie.LP <= 0)
            {
                this.Zombie.CurrentState = Zombie.ZombieState.Death;
                return;
            }

            if (this.Zombie.Cell == null)
                return;

            foreach (Griding.IGridable grc in this.Zombie.Cell.Components)
            {
                Zombie zombie = grc as Zombie;
                if ((zombie != null) && (zombie != this.Zombie) && (zombie.CurrentState != Zombie.ZombieState.Death))
                {
                    this.Zombie.CurrentState = Zombie.ZombieState.Attack;
                }
            }
        }
        #endregion
    }
}
