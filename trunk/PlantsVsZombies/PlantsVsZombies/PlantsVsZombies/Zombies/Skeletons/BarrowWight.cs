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
            this.Image.Delay = 40;
            this.Align = new Vector2(10, 72);
            this.Velocity = 2;
        }
        #endregion
    }

    public class BarrowWightAttack : States.Attack
    {
        public BarrowWightAttack(BarrowWight zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BarrowWight\Attack");
            this.Image.Delay = 60;
            this.Align = new Vector2(18f, 92f);
            this.Damage = 7;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class BarrowWightDeath : States.Death
    {
        public BarrowWightDeath(BarrowWight zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BarrowWight\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(18f, 92f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}
