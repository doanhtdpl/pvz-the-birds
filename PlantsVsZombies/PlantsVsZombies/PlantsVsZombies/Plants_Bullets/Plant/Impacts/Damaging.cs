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

namespace PlantsVsZombies.Plants_Bullets.Plant.Impacts
{
    public class Damaging : GameComponent, IPlantImpact
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
        public virtual bool Apply(Plant plant)
        {
            flashTimer.Start();
            plant.CurrentAnimation.Color = GMath.GammaBlend(plant.CurrentAnimation.Color, Color.Red, 0.5f);

            plant.DecreaseHP(this.Damage);
            return true;
        }

        public virtual void Remove(Plant plant)
        {
            flashTimer.Stop();

            plant.CurrentAnimation.Color = GMath.DeGammaBlend(plant.CurrentAnimation.Color, Color.Red, 0.5f);
        }

        public virtual void OnFlashTimerTick(Counter.ICounter timer)
        {
            this.IsCompleted = true;
        }

        public override void Update(GameTime gameTime)
        {
            flashTimer.Update(gameTime);

            base.Update(gameTime);
        }
        #endregion
    }
}
