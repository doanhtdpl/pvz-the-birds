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

namespace PlantsVsZombies.Zombies.Impacts
{
    public class Damaging : GameComponent, IZombieImpact
    {
        #region Fields & Properties
        public int Damage { get; set; }

        public bool IsCompleted { get { return true; } }
        #endregion

        #region Constructors
        public Damaging(Game game)
            : base(game)
        {
            this.Damage = 0;
        }

        public Damaging(Game game, int damage)
            : base(game)
        {
            this.Damage = damage;
        }

        public Damaging(Damaging dam)
            : base(dam.Game)
        {
            this.Damage = dam.Damage;
        }
        #endregion

        #region Methods
        public virtual void Apply(States.ZombieState state)
        {
            state.Zombie.LP -= this.Damage;
        }

        public virtual void Remove(States.ZombieState state)
        {

        }

        public virtual void ChangeState(States.ZombieState currentState, States.ZombieState lastState)
        {

        }
        #endregion
    }
}
