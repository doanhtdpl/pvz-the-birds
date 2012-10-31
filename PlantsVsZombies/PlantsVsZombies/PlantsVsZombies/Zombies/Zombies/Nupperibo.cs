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
    public class Nupperibo : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Nupperibo(Game game)
            : base(game)
        {
            this.Walk = new NupperiboWalk(this);
            this.Attack = new NupperiboAttack(this);
            this.Death = new NupperiboDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class NupperiboWalk : States.Walk
    {
        #region Constructors
        public NupperiboWalk(Nupperibo zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Nupperibo\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(10f, 62f);
            this.Velocity = 0.75f;
        }
        #endregion
    }

    public class NupperiboAttack : States.Attack
    {
        public NupperiboAttack(Nupperibo zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Nupperibo\Attack");
            this.Image.Delay = 100;
            this.Align = new Vector2(19f, 82f);
            this.Damage = 13;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class NupperiboDeath : States.Death
    {
        public NupperiboDeath(Nupperibo zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Nupperibo\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(18f, 71f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}