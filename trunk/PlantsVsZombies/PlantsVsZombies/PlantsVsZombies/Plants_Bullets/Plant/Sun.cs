using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;

namespace PlantsVsZombies.Plants_Bullets.Plant
{
    public abstract class Sun : DrawableGameComponent
    {
        #region Fields
        // Animations for sun
        protected Animation animation;
        // Velocity to go to bank
        protected Vector2 vBank;
        // Direction of bank
        protected Vector2 bankDirection;
        // Is Sun clicked
        protected bool isClicked;
        // Report true when sun have gone to bank
        protected bool toBank;
        // Report to sun-manager if sun fade out success
        protected bool done;
        // True when sun auto Remove
        protected bool lost;
        // Reference to Game Griding
        protected Griding.Griding griding;
        // Time to auto remove
        protected int autoRemoveDelay;
        protected Counter.Timer autoremoveTimer;

        // Sun's value
        protected int sunValue;

        // Properties
        public bool IsDone
        {
            get { return this.done; }
        }

        public bool IsLost
        {
            get { return this.lost; }
        }

        public Vector2 BankDirection
        {
            set { this.bankDirection = value; }
        }

        public int SunValue
        {
            get { return this.sunValue; }
            set { this.sunValue = value; }
        }

        #endregion

        // Constructor
        public Sun(Game game, Griding.Griding griding, Vector2 bankDirection)
            : base(game)
        {
            this.bankDirection = bankDirection;
            this.griding = griding;
            this.Initialize();
        }

        public override void Initialize()
        {
            animation = SpriteBank.GetAnimation("Images\\Plants\\SunMush0");
            vBank = Vector2.Zero;
            done = false;
            toBank = false;
            isClicked = false;
            lost = false;

            this.autoRemoveDelay = 5000;
            this.autoremoveTimer = new Counter.Timer(this.Game, this.autoRemoveDelay);
            this.autoremoveTimer.OnMeet += new Counter.EventOnCounterMeet(autoremoveTimer_OnMeet);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 mousePos = GMouse.MousePosition;
            // Check for click on sun
            if(GMouse.IsLeftButtonClicked &&
                animation.PositionX <= mousePos.X && mousePos.X <= animation.PositionX + animation.Bound.X &&
                animation.PositionY <= mousePos.Y && mousePos.Y <= animation.PositionY + animation.Bound.Y)
            {
                isClicked = true;
                // Set animation transparent to FULL
                this.animation.ColorA = 255;
                this.toBank = false;
                SetVBank();
            }
            MoveToBank();
            FadeOut();

            animation.Update(gameTime);
            autoremoveTimer.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            animation.Draw(gameTime);
            base.Draw(gameTime);
        }

        // Allow the sun move to bank
        public virtual void MoveToBank()
        {
            this.animation.Position -= vBank;
            if (this.animation.PositionX <= bankDirection.X && 
                this.animation.PositionY <= bankDirection.Y)
            {
                this.toBank = true;
                this.animation.Position = bankDirection;

                vBank = Vector2.Zero;
            }
        }

        // Set the velocity for sun to move
        private void SetVBank()
        {
            vBank = this.animation.Position - bankDirection;
            vBank.Normalize();
            // Increase velocity move of sun
            vBank.X *= 20f;
            vBank.Y *= 20f;
        }

        // Fade out the sun gradually
        protected void FadeOut()
        {
            if (this.toBank && this.animation.ColorA > 0)
            {
                if (this.animation.ColorA < 10)
                {
                    if (this.animation.Position == bankDirection)
                        this.done = true;
                    else
                        this.lost = true;
                }
                else
                    this.animation.ColorA -= 10;
            }
        }

        // Do anything if sun down on destination and don't click on it
        protected abstract void CheckAutoRemove();

        protected void autoremoveTimer_OnMeet(object o)
        {
            this.toBank = true;
        }
    }
}
