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
    public class SwordSkeleton : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public SwordSkeleton(Game game)
            : base(game)
        {
            this.Walk = new SwordSkeletonWalk(this);
            this.Attack = new SwordSkeletonAttack(this);
            this.Death = new SwordSkeletonDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class SwordSkeletonWalk : States.Walk
    {
        #region Constructors
        public SwordSkeletonWalk(SwordSkeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\GiantSkeletonSword\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(27f, 94f);
            this.Velocity = 2;
        }
        #endregion
    }

    public class SwordSkeletonAttack : States.Attack
    {
        public SwordSkeletonAttack(SwordSkeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\GiantSkeletonSword\Attack");
            this.Image.Delay = 60;
            this.Align = new Vector2(30f, 112f);
            this.Damage = 12;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class SwordSkeletonDeath : States.Death
    {
        public SwordSkeletonDeath(SwordSkeleton zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\GiantSkeletonSword\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(35f, 99f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}