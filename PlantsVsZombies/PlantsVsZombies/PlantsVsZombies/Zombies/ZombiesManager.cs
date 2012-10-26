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

namespace PlantsVsZombies.Zombies
{
    public class ZombiesManager : DrawableGameComponent
    {
        #region Fields
        protected List<Zombie> Zombies { get; set; }

        public Griding.Griding Grid { get; set; }
        #endregion

        #region Constructors
        public ZombiesManager(Griding.Griding grid)
            : base(grid.Game)
        {
            this.Zombies = new List<Zombie>();
            this.Grid = grid;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.Zombies.Count;)
            {
                if (Zombies[i].NeedRemove)
                {
                    this.Grid.Remove(this.Zombies[i]);
                    this.Zombies.RemoveAt(i);
                }
                else
                {
                    Zombies[i].Update(gameTime);
                    ++i;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Zombie zombie in this.Zombies)
            {
                zombie.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        public virtual void Add(Zombie zombie)
        {
            this.Zombies.Add(zombie);
            this.Grid.Add(zombie);
        }

        public virtual void Remove(Zombie zombie)
        {
            this.Zombies.Remove(zombie);
            this.Grid.Remove(zombie);
        }
        #endregion
    }
}
