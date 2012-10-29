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
    public class Nameless : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Nameless(Game game)
            : base(game)
        {
            this.Walk = new NamelessWalk(this);
            this.Attack = new NamelessAttack(this);
            this.Death = new NamelessDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class NamelessWalk : States.Walk
    {
        #region Constructors
        public NamelessWalk(Nameless zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Nameless\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(3f, 93f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class NamelessAttack : States.Attack
    {
        public NamelessAttack(Nameless zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Nameless\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(15f, 101f);
            this.Damage = 2;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class NamelessDeath : States.Death
    {
        public NamelessDeath(Nameless zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Nameless\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(48f, 95f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}