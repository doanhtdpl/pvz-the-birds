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

namespace PlantsVsZombies.Zombies.Zombies
{
    public class Mummy : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Mummy(Game game)
            : base(game)
        {
            this.Walk = new MummyWalk(this);
            this.Attack = new MummyAttack(this);
            this.Death = new MummyDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class MummyWalk : States.Walk
    {
        #region Constructors
        public MummyWalk(Mummy zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Mummy\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(5f, 72f);
            this.Velocity = 1.5f;
        }
        #endregion
    }

    public class MummyAttack : States.Attack
    {
        public MummyAttack(Mummy zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Mummy\Attack");
            this.Image.Delay = 50;
            this.Align = new Vector2(32f, 81f);
            this.Damage = 12;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class MummyDeath : States.Death
    {
        public MummyDeath(Mummy zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Mummy\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(20f, 79f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}