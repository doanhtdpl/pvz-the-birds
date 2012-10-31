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
    public class Histachii : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Histachii(Game game)
            : base(game)
        {
            this.Walk = new HistachiiWalk(this);
            this.Attack = new HistachiiAttack(this);
            this.Death = new HistachiiDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class HistachiiWalk : States.Walk
    {
        #region Constructors
        public HistachiiWalk(Histachii zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Histachii\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(3f, 72f);
            this.Velocity = 2.5f;
        }
        #endregion
    }

    public class HistachiiAttack : States.Attack
    {
        public HistachiiAttack(Histachii zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Histachii\Attack");
            this.Image.Delay = 60;
            this.Align = new Vector2(23f, 73f);
            this.Damage = 15;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class HistachiiDeath : States.Death
    {
        public HistachiiDeath(Histachii zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Histachii\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(13f, 76f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}