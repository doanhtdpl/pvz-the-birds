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
    public class Remorhaz : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Remorhaz(Game game)
            : base(game)
        {
            this.Walk = new RemorhazWalk(this);
            this.Attack = new RemorhazAttack(this);
            this.Death = new RemorhazDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class RemorhazWalk : States.Walk
    {
        #region Constructors
        public RemorhazWalk(Remorhaz zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Remorhaz\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(18f, 146f);
            this.Velocity = 0.75f;
        }
        #endregion
    }

    public class RemorhazAttack : States.Attack
    {
        public RemorhazAttack(Remorhaz zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Remorhaz\Attack");
            this.Image.Delay = 100;
            this.Align = new Vector2(57f, 159f);
            this.Damage = 20;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class RemorhazDeath : States.Death
    {
        public RemorhazDeath(Remorhaz zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Spiders\Remorhaz\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(18f, 129f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}