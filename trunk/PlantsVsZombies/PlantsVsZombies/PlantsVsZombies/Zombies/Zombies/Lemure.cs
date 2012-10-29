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
    public class Lemure : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Lemure(Game game)
            : base(game)
        {
            this.Walk = new LemureWalk(this);
            this.Attack = new LemureAttack(this);
            this.Death = new LemureDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class LemureWalk : States.Walk
    {
        #region Constructors
        public LemureWalk(Lemure zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Lemure\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(6f, 78f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class LemureAttack : States.Attack
    {
        public LemureAttack(Lemure zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Lemure\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(22f, 97f);
            this.Damage = 2;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class LemureDeath : States.Death
    {
        public LemureDeath(Lemure zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Lemure\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(0f, 85f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}