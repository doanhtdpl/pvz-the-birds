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
    public class Skeleton : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Skeleton(Game game)
            : base(game)
        {
            this.Walk = new SkeletonWalk(this);
            this.Attack = new SkeletonAttack(this);
            this.Death = new SkeletonDeath(this);
        }
        #endregion
    }

    public class SkeletonWalk : States.Walk
    {
        #region Constructors
        public SkeletonWalk(Skeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\Skeleton\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(3f, 74f);
            this.Velocity = 1;
        }
        #endregion
    }

    public class SkeletonAttack : States.Attack
    {
        public SkeletonAttack(Skeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\Skeleton\Attack");
            this.Image.Delay = 80;
            this.Align = new Vector2(32f, 89f);
            this.Damage = 10;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class SkeletonDeath : States.Death
    {
        public SkeletonDeath(Skeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\Skeleton\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(52f, 97f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}
