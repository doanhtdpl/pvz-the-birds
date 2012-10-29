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
    public class BladeSkeleton : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public BladeSkeleton(Game game)
            : base(game)
        {
            this.Walk = new BladeSkeletonWalk(this);
            this.Attack = new BladeSkeletonAttack(this);
            this.Death = new BladeSkeletonDeath(this);
        }
        #endregion
    }

    public class BladeSkeletonWalk : States.Walk
    {
        #region Constructors
        public BladeSkeletonWalk(BladeSkeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\GiantSkeletonBlade\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(7f, 106f);
            this.Velocity = GRandom.RandomInt(2, 8);
        }
        #endregion
    }

    public class BladeSkeletonAttack : States.Attack
    {
        public BladeSkeletonAttack(BladeSkeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\GiantSkeletonBlade\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(49f, 153f);
            this.Damage = 50;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class BladeSkeletonDeath : States.Death
    {
        public BladeSkeletonDeath(BladeSkeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\GiantSkeletonBlade\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(33f, 145f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}
