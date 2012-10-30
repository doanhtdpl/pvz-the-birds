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

namespace PlantsVsZombies.Plants_Bullets.Grows.GrowButtons
{
    public class IcePeaButton : GrowButton
    {
        #region Constructors
        public IcePeaButton(GrowManager manager)
            : base(manager, SpriteBank.GetSprite(@"Images\Controls\IcePeaShooterButton"))
        {
            this.Price = 175;
            this.CoolDown.Interval = TimeSpan.FromMilliseconds(10000);
        }
        #endregion

        #region Methods
        protected override Plant.Plant CreatePlant()
        {
            return new Plant.IcePea(this.Game, this.Manager.Manager, Vector2.Zero);
        }
        #endregion
    }
}