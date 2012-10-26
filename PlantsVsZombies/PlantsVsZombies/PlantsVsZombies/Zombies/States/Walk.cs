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
        float Velocity { get; set; }
        #endregion

        #region Constructors
        public Walk(Animation image, float velocity)
            : base(image)
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
            Vector2 position = this.Position;
            position.X -= Velocity;
            this.Position = position;

            base.Update(gameTime);
        }
        #endregion
    }
}
