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
    public class Attack : ZombieState
    {
        #region Properties
        int Damage { get; set; }
        #endregion

        #region Constructors
        public Attack(Animation image, int damage)
            : base(image)
        {
            this.Damage = damage;
        }

        public Attack(Attack attack)
            : base(attack)
        {
            this.Damage = attack.Damage;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion
    }
}
