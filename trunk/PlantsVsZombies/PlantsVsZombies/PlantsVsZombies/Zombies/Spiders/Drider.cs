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
    public class Drider : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Drider(Game game)
            : base(game)
        {
            this.Walk = new DriderWalk(this);
            this.Attack = new DriderAttack(this);
            this.Death = new DriderDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class DriderWalk : States.Walk
    {
        #region Constructors
        public DriderWalk(Drider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Drider\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(55f, 92f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class DriderAttack : States.Attack
    {
        public DriderAttack(Drider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Drider\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(25f, 85f);
            this.Damage = 2;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class DriderDeath : States.Death
    {
        public DriderDeath(Drider zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Drider\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(44f, 79f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}