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
    public class B_DPeaEngine : B_PeaEngine
    {
        // Fields
        protected Counter.Timer timer;
        protected int delay = 300;
        protected Vector2 position;
        
        public B_DPeaEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
            timer = new Counter.Timer(game, delay);
            timer.OnMeet += new Counter.EventOnCounterMeet(timer_OnMeet);
        }

        // Methods
        public override void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            base.Update(gameTime);
        }

        public override void AddBullet(Vector2 position)
        {
            this.position = position;

            // Reset delay
            timer.Start();
            base.AddBullet(position);
        }

        public void timer_OnMeet(object o)
        {
            AddBullet(position);

            // Reset timer
            ResetTimer();
        }

        // Reset timer for add bullet after
        protected void ResetTimer()
        {
            timer.Stop();
            timer.Reset();
        }
    }
}
