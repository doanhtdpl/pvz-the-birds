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
using PlantsVsZombies.Griding;

namespace PlantsVsZombies.Zombies.Skeletons
{
    public class BarrowWight : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public BarrowWight(Game game)
            : base(game)
        {
            this.Walk = new States.Walk(SpriteBank.GetAnimation(@"Images\Skeletons\BarrowWight\Walk"), 15f);
            this.Attack = new States.Attack(SpriteBank.GetAnimation(@"Images\Skeletons\BarrowWight\Attack"), 20);
            this.Death = new States.Death(SpriteBank.GetAnimation(@"Images\Skeletons\BarrowWight\Death"), 5000);
        }
        #endregion
    }
}
