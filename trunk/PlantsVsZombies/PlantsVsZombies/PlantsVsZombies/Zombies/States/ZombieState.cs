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
    public class ZombieState : DrawableGameComponent
    {
        #region Properties
        public Animation Image { get; set; }
        public Vector2 Position
        {
            get
            {
                return (Image.Position + Align);
            }
            set
            {
                Image.Position = value - Align;
            }
        }

        public Vector2 Align = Vector2.Zero;
        #endregion

        #region Constructors
        public ZombieState(Animation image)
            : base(image.Game)
        {
            this.Image = image;
            Align = new Vector2(0, image.SizeY);
        }

        public ZombieState(ZombieState state)
            : base(state.Game)
        {
            this.Image = new Animation(state.Image);
            Align = state.Align;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (this.Image != null)
                this.Image.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Image != null)
                this.Image.Draw(gameTime);

            base.Draw(gameTime);
        }
        #endregion
    }
}