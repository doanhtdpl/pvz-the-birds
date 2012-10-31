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
    public class Vampire : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Vampire(Game game)
            : base(game)
        {
            this.Walk = new VampireWalk(this);
            this.Attack = new VampireAttack(this);
            this.Death = new VampireDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class VampireWalk : States.Walk
    {
        #region Constructors
        public VampireWalk(Vampire zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Vampire\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(-1f, 61f);
            this.Velocity = 3.5f;
        }
        #endregion
    }

    public class VampireAttack : States.Attack
    {
        public VampireAttack(Vampire zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Vampire\Attack");
            this.Image.Delay = 80;
            this.Align = new Vector2(4f, 66f);
            this.Damage = 7;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class VampireDeath : States.Death
    {
        public VampireDeath(Vampire zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Vampire\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(15f, 62f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}