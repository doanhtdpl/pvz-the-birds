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
    public class Tattered : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Tattered(Game game)
            : base(game)
        {
            this.Walk = new TatteredWalk(this);
            this.Attack = new TatteredAttack(this);
            this.Death = new TatteredDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class TatteredWalk : States.Walk
    {
        #region Constructors
        public TatteredWalk(Tattered zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\Tattered\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(10f, 87f);
            this.Velocity = 2;
        }
        #endregion
    }

    public class TatteredAttack : States.Attack
    {
        public TatteredAttack(Tattered zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\Tattered\Attack");
            this.Image.Delay = 60;
            this.Align = new Vector2(20f, 99f);
            this.Damage = 10;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class TatteredDeath : States.Death
    {
        public TatteredDeath(Tattered zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\Tattered\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(26f, 93f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}