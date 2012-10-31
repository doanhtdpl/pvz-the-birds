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
    public class Spider : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Spider(Game game)
            : base(game)
        {
            this.Walk = new SpiderWalk(this);
            this.Attack = new SpiderAttack(this);
            this.Death = new SpiderDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class SpiderWalk : States.Walk
    {
        #region Constructors
        public SpiderWalk(Spider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Spider\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(5f, 29f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class SpiderAttack : States.Attack
    {
        public SpiderAttack(Spider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Spider\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(12f, 38f);
            this.Damage = 10;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class SpiderDeath : States.Death
    {
        public SpiderDeath(Spider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Spider\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(2f, 36f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}