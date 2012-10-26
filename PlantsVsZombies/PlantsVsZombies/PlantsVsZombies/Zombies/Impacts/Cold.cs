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
    public class Cold : GameComponent, IZombieImpact
    {
        #region Fields & Properties
        public Counter.Timer Timer { get; set; }
        public int ColdAffect { get; set; }
        private int appliedAffect;

        public bool IsCompleted { get { return this.isCompleted; } }
        private bool isCompleted = false;
        #endregion

        #region Constructors
        public Cold(Game game)
            : base(game)
        {
            this.Timer = new Counter.Timer(game, 0);
            this.Timer.OnMeet += new Counter.EventOnCounterMeet(this.OnTimerTick);
            this.ColdAffect = 0;
        }

        public Cold(Game game, int coldTime, int coldAffect)
            : base(game)
        {
            this.Timer = new Counter.Timer(game, coldTime);
            this.Timer.OnMeet += new Counter.EventOnCounterMeet(this.OnTimerTick);
            this.ColdAffect = coldAffect;
        }

        public Cold(Cold cold)
            : base(cold.Game)
        {
            this.Timer = new Counter.Timer(cold.Timer);
            this.Timer.OnMeet += new Counter.EventOnCounterMeet(this.OnTimerTick);
            this.ColdAffect = cold.ColdAffect;
        }
        #endregion

        #region Methods
        private void OnTimerTick(Counter.ICounter timer)
        {
            this.isCompleted = true;
            this.Timer.Stop();
        }

        private int GetZombieColds(Zombie zombie)
        {
            int nCold = 0;
            foreach (IZombieImpact impact in zombie.Impacts)
            {
                if (impact is Cold)
                    ++nCold;
            }

            return nCold;
        }

        public virtual void Apply(States.ZombieState state)
        {
            int nCold = GetZombieColds(state.Zombie);

            if (nCold == 0)
                state.Image.Color = Color.CornflowerBlue;

            this.appliedAffect = (int) ((1f / (nCold + 1)) * ColdAffect);
            state.Image.Delay = state.Image.Delay + appliedAffect;


            States.Walk walk = state as States.Walk;
            if (walk != null)
            {
                double vel = walk.Velocity * this.Game.TargetElapsedTime.TotalMilliseconds;
                vel /= (this.Game.TargetElapsedTime.TotalMilliseconds + appliedAffect);
                walk.Velocity = (int)vel;
            }
        }

        public virtual void Remove(States.ZombieState state)
        {
            int nCold = GetZombieColds(state.Zombie);

            if (nCold == 1)
                state.Image.Color = Color.White;

            state.Image.Delay = state.Image.Delay - this.appliedAffect;

            States.Walk walk = state as States.Walk;
            if (walk != null)
            {
                double vel = walk.Velocity * (this.Game.TargetElapsedTime.TotalMilliseconds + this.appliedAffect);
                vel /= (this.Game.TargetElapsedTime.TotalMilliseconds + (long)((1f / (nCold + 1)) * ColdAffect));
                walk.Velocity = (int)vel;
            }
        }

        public virtual void ChangeState(States.ZombieState currentState, States.ZombieState lastState)
        {

        }
        #endregion
    }
}