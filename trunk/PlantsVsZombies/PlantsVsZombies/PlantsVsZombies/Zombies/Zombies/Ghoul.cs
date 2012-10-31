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
    public class Ghoul : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Ghoul(Game game)
            : base(game)
        {
            this.Walk = new GhoulWalk(this);
            this.Attack = new GhoulAttack(this);
            this.Death = new GhoulDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class GhoulWalk : States.Walk
    {
        #region Constructors
        public GhoulWalk(Ghoul zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Ghoul\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(13f, 84f);
            this.Velocity = 2.5f;
        }
        #endregion
    }

    public class GhoulAttack : States.Attack
    {
        public GhoulAttack(Ghoul zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Ghoul\Attack");
            this.Image.Delay = 50;
            this.Align = new Vector2(18f, 98f);
            this.Damage = 9;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class GhoulDeath : States.Death
    {
        public GhoulDeath(Ghoul zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Ghoul\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(67f, 89f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}