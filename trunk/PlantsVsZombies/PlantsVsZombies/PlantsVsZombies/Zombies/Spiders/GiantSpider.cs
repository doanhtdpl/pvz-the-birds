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

namespace PlantsVsZombies.Zombies.Spiders
{
    public class GiantSpider : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public GiantSpider(Game game)
            : base(game)
        {
            this.Walk = new GiantSpiderWalk(this);
            this.Attack = new GiantSpiderAttack(this);
            this.Death = new GiantSpiderDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class GiantSpiderWalk : States.Walk
    {
        #region Constructors
        public GiantSpiderWalk(GiantSpider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Giant\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(24f, 56f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class GiantSpiderAttack : States.Attack
    {
        public GiantSpiderAttack(GiantSpider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Giant\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(38f, 74f);
            this.Damage = 2;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class GiantSpiderDeath : States.Death
    {
        public GiantSpiderDeath(GiantSpider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Giant\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(8f, 69f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}