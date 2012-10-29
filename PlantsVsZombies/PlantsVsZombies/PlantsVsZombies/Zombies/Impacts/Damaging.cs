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

        public bool IsCompleted { get; set; }

        private Counter.Timer flashTimer;
        #endregion

        #region Constructors
        public Damaging(Game game)
            : base(game)
        {
            this.Damage = 0;
            flashTimer = new Counter.Timer(this.Game, 200);
            flashTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnFlashTimerTick);
            this.IsCompleted = false;
        }

        public Damaging(Game game, int damage)
            : base(game)
        {
            this.Damage = damage;
            flashTimer = new Counter.Timer(this.Game, 200);
            flashTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnFlashTimerTick);
            this.IsCompleted = false;
        }

        public Damaging(Damaging dam)
            : base(dam.Game)
        {
            this.Damage = dam.Damage;
            flashTimer = new Counter.Timer(dam.flashTimer);
            flashTimer.OnMeet += new Counter.EventOnCounterMeet(this.OnFlashTimerTick);
            this.IsCompleted = false;
        }
        #endregion

        #region Methods
        public virtual bool Apply(States.ZombieState state)
        {
            flashTimer.Start();
            if (!(state is States.Death))
                state.Image.Color = GMath.GammaBlend(state.Image.Color, Color.Red, 0.5f);

            state.Zombie.LP -= this.Damage;
            return true;
        }

        public virtual void Remove(States.ZombieState state)
        {
            flashTimer.Stop();

            if (!(state is States.Death))
                state.Image.Color = GMath.DeGammaBlend(state.Image.Color, Color.Red, 0.5f);
        }

        public virtual void ChangeState(States.ZombieState currentState, States.ZombieState lastState)
        {
            lastState.Image.Color = GMath.DeGammaBlend(lastState.Image.Color, Color.Red, 0.5f);

            if (!(currentState is States.Death))
                currentState.Image.Color = GMath.GammaBlend(currentState.Image.Color, Color.Red, 0.5f);
        }

        public virtual void OnFlashTimerTick(Counter.ICounter timer)
        {
            this.IsCompleted = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            flashTimer.Update(gameTime);

            base.Update(gameTime);
        }
        #endregion
    }
}
