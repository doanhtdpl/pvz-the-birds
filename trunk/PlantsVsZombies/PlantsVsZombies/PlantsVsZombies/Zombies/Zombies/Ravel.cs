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
    public class Ravel : Zombie
    {
        #region Fields
        #endregion

        #region Constructors
        public Ravel(Game game)
            : base(game)
        {
            this.Walk = new RavelWalk(this);
            this.Attack = new RavelAttack(this);
            this.Death = new RavelDeath(this);
        }
        #endregion

        #region Methods
        #endregion
    }

    public class RavelWalk : States.Walk
    {
        #region Constructors
        public RavelWalk(Ravel zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Ravel\Walk");
            this.Image.Delay = 40;
            this.Align = new Vector2(0f, 74f);
            this.Velocity = GRandom.RandomInt(5, 15);
        }
        #endregion
    }

    public class RavelAttack : States.Attack
    {
        public RavelAttack(Ravel zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Ravel\Attack");
            this.Image.Delay = 40;
            this.Align = new Vector2(32f, 84f);
            this.Damage = 2;
            this.AttackTimer.Interval = TimeSpan.FromMilliseconds(this.Image.Delay * (this.Image.Frames.Count + 1));
        }
    }

    public class RavelDeath : States.Death
    {
        public RavelDeath(Ravel zombie)
            : base(zombie)
        {
            this.Image = SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Ravel\Death");
            this.Image.Delay = 40;
            this.Align = new Vector2(26f, 78f);
            this.Timer.Interval = TimeSpan.FromMilliseconds(2000);
        }
    }
}