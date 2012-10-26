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
            this.Walk = new BarrowWightWalk(this);
            this.Attack = new BarrowWightAttack(this);
            this.Death = new BarrowWightDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class BarrowWightWalk : States.Walk
    {
        #region Constructors
        public BarrowWightWalk(BarrowWight zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BarrowWight\Walk");
            this.Align = new Vector2(10, 72);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class BarrowWightAttack : States.Attack
    {
        public BarrowWightAttack(BarrowWight zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BarrowWight\Attack");
            this.Align = new Vector2(18f, 92f);
            this.Damage = 20;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(300);
        }
    }

    public class BarrowWightDeath : States.Death
    {
        public BarrowWightDeath(BarrowWight zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BarrowWight\Death");
            this.Align = new Vector2(18f, 92f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}
