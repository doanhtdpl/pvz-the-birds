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
    public class BoneGolem : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public BoneGolem(Game game)
            : base(game)
        {
            this.Walk = new BoneGolemWalk(this);
            this.Attack = new BoneGolemAttack(this);
            this.Death = new BoneGolemDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class BoneGolemWalk : States.Walk
    {
        #region Constructors
        public BoneGolemWalk(BoneGolem zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BoneGolem\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(13f, 109f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class BoneGolemAttack : States.Attack
    {
        public BoneGolemAttack(BoneGolem zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BoneGolem\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(68f, 186f);
            this.Damage = 2;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class BoneGolemDeath : States.Death
    {
        public BoneGolemDeath(BoneGolem zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Skeletons\BoneGolem\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(-9f, 103f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}