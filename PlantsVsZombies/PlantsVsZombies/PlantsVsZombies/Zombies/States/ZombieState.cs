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

namespace PlantsVsZombies.Zombies.States
{
    public abstract class ZombieState : DrawableGameComponent
    {
        #region Properties
        public Zombie Zombie { get; set; }

        public Animation Image { get; set; }

        public Vector2 Align = Vector2.Zero;
        #endregion

        #region Constructors
        public ZombieState(Zombie zombie)
            : base(zombie.Game)
        {
            Align = Vector2.Zero;
            this.Zombie = zombie;
        }

        public ZombieState(Zombie zombie, Animation image)
            : base(zombie.Game)
        {
            this.Image = image;
            Align = new Vector2(0, image.SizeY);
            this.Zombie = zombie;
        }

        public ZombieState(ZombieState state)
            : base(state.Game)
        {
            this.Image = new Animation(state.Image);
            Align = state.Align;
            this.Zombie = state.Zombie;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (this.Image != null)
                this.Image.Update(gameTime);

            this.CheckingState();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Image != null)
                this.Image.Draw(gameTime);

            base.Draw(gameTime);
        }

        public abstract void CheckingState();

        public virtual void Start()
        {
            this.Image.Reset();
        }

        public virtual void End()
        {
        }
        #endregion
    }
}