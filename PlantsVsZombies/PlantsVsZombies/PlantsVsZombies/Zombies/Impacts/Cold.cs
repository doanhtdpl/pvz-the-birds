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
        public float ColdAffect { get; set; }

        public bool IsCompleted { get { return this.isCompleted; } }
        private bool isCompleted = false;
        #endregion

        #region Constructors
        public Cold(Game game)
            : base(game)
        {
            this.Timer = new Counter.Timer(game, 3000);
            this.Timer.OnMeet += new Counter.EventOnCounterMeet(this.OnTimerTick);
            this.ColdAffect = 0.5f;
        }

        public Cold(Game game, int coldTime, float coldAffect)
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

        public virtual bool Apply(States.ZombieState state)
        {
            this.Timer.Start();

            if (!(state is States.Death) && GetZombieColds(state.Zombie) == 0)
            {
                state.Image.Color = GMath.GammaBlend(state.Image.Color, Color.Blue, 0.5f);
                state.Image.Delay = (long)(state.Image.Delay / ColdAffect);

                States.Walk walk = state as States.Walk;
                if (walk != null)
                {
                    walk.Velocity *= ColdAffect;
                }

                States.Attack att = state as States.Attack;
                if (att != null)
                {
                    att.AttackTimer.Interval = TimeSpan.FromMilliseconds(att.AttackTimer.Interval.TotalMilliseconds / ColdAffect);
                }

                return true;
            }

            return false;
        }

        public virtual void Remove(States.ZombieState state)
        {
            this.Timer.Stop();

            if (!(state is States.Death))
            {
                state.Image.Color = GMath.DeGammaBlend(state.Image.Color, Color.Blue, 0.5f);
                state.Image.Delay = (long)(state.Image.Delay * ColdAffect);

                States.Walk walk = state as States.Walk;
                if (walk != null)
                {
                    walk.Velocity /= ColdAffect;
                }

                States.Attack att = state as States.Attack;
                if (att != null)
                {
                    att.AttackTimer.Interval = TimeSpan.FromMilliseconds(att.AttackTimer.Interval.TotalMilliseconds * ColdAffect);
                }
            }
        }

        public virtual void ChangeState(States.ZombieState currentState, States.ZombieState lastState)
        {
                if (!(currentState is States.Death))
                {
                    currentState.Image.Color = GMath.GammaBlend(currentState.Image.Color, Color.Blue, 0.5f);
                    currentState.Image.Delay = (long)(currentState.Image.Delay / ColdAffect);

                    States.Walk cwalk = currentState as States.Walk;
                    if (cwalk != null)
                    {
                        cwalk.Velocity *= ColdAffect;
                    }

                    States.Attack catt = currentState as States.Attack;
                    if (catt != null)
                    {
                        catt.AttackTimer.Interval = TimeSpan.FromMilliseconds(catt.AttackTimer.Interval.TotalMilliseconds / ColdAffect);
                    }
                }


                if (!(lastState is States.Death))
                {
                    lastState.Image.Color = GMath.DeGammaBlend(lastState.Image.Color, Color.Blue, 0.5f);
                    lastState.Image.Delay = (long)(lastState.Image.Delay * ColdAffect);

                    States.Walk lwalk = lastState as States.Walk;
                    if (lwalk != null)
                    {
                        lwalk.Velocity /= ColdAffect;
                    }

                    States.Attack latt = lastState as States.Attack;
                    if (latt != null)
                    {
                        latt.AttackTimer.Interval = TimeSpan.FromMilliseconds(latt.AttackTimer.Interval.TotalMilliseconds * ColdAffect);
                    }
                }
        }

        public override void Update(GameTime gameTime)
        {
            Timer.Update(gameTime);

            base.Update(gameTime);
        }
        #endregion
    }
}