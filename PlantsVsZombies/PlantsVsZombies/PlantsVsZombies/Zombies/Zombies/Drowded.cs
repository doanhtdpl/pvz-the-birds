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
    public class Drowded : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Drowded(Game game)
            : base(game)
        {
            this.Walk = new DrowdedWalk(this);
            this.Attack = new DrowdedAttack(this);
            this.Death = new DrowdedDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class DrowdedWalk : States.Walk
    {
        #region Constructors
        public DrowdedWalk(Drowded zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Drowded\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(14f, 77f);
            this.Velocity = 0.75f;
        }
        #endregion
    }

    public class DrowdedAttack : States.Attack
    {
        public DrowdedAttack(Drowded zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Drowded\Attack");
            this.Image.Delay = 100;
            this.Align = new Vector2(14f, 78f);
            this.Damage = 16;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class DrowdedDeath : States.Death
    {
        public DrowdedDeath(Drowded zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Drowded\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(19f, 76f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}