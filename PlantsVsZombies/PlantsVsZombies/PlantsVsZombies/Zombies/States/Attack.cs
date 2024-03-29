﻿using System;
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
using PlantsVsZombies.Plants_Bullets.Plant;

namespace PlantsVsZombies.Zombies.States
{
    public class Attack : ZombieState
    {
        #region Properties
        public int Damage { get; set; }

        public Counter.Timer AttackTimer { get; set; }
        #endregion

        #region Constructors
        public Attack(Zombie zombie)
            : base(zombie)
        {
            AttackTimer = new Counter.Timer(this.Game, int.MaxValue);
            AttackTimer.OnMeet +=new Counter.EventOnCounterMeet(OnAttackTimerTick);
            this.Damage = 0;
        }

        public Attack(Zombie zombie, Animation image, int damage, float attSpeed)
            : base(zombie, image)
        {
            this.Damage = damage;
            AttackTimer = new Counter.Timer(this.Game, (int)(1000f / attSpeed));
            AttackTimer.OnMeet += new Counter.EventOnCounterMeet(OnAttackTimerTick);
        }

        public Attack(Attack attack)
            : base(attack)
        {
            this.Damage = attack.Damage;
            AttackTimer.OnMeet += new Counter.EventOnCounterMeet(OnAttackTimerTick);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            AttackTimer.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Start()
        {
            this.AttackTimer.Start();

            base.Start();
        }

        public override void End()
        {
            this.AttackTimer.Stop();

            base.End();
        }

        public virtual void OnAttackTimerTick(Counter.ICounter timer)
        {
            foreach (Griding.IGridable grc in this.Zombie.Cell.Components)
            {
                Plant plant = grc as Plant;
                if ((plant != null) && !plant.IsDead)
                {
                    this.Damaging(plant);
                }
            }
        }

        public virtual void Damaging(Plant plant)
        {
            Plants_Bullets.Plant.Impacts.Damaging impDamage = new Plants_Bullets.Plant.Impacts.Damaging(this.Game, this.Damage);
            plant.AddImpact(impDamage);
        }

        public override void CheckingState()
        {
            if (Zombie.LP <= 0)
            {
                this.Zombie.CurrentState = Zombie.ZombieState.Death;
            }
            else
            {
                if (this.Zombie.Cell == null)
                    return;

                foreach (Griding.IGridable grc in this.Zombie.Cell.Components)
                {
                    Plant plant = grc as Plant;
                    if ((plant != null) && !plant.IsDead)
                    {
                        return;
                    }
                }

                this.Zombie.CurrentState = Zombie.ZombieState.Walk;
            }
        }
        #endregion
    }
}
