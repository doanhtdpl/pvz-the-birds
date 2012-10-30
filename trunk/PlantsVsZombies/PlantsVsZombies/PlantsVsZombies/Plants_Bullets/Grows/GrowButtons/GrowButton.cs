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
using PlantsVsZombies.Controls;
using PlantsVsZombies.Plants_Bullets.Plant;

namespace PlantsVsZombies.Plants_Bullets.Grows.GrowButtons
{
    public abstract class GrowButton : Button
    {
        #region Fields & Properties
        public int Price { get; set; }

        public Counter.Timer CoolDown { get; set; }
        bool isCoolDowning = false;

        protected Counter.Timer visualCoolDown;
        protected Sprite coolDownImage;

        public GrowManager Manager { get; set; }

        public bool CanBuy
        {
            get
            {
                return (!this.isCoolDowning && (this.Manager.Manager.GetSunManager.NumberOfSuns >= this.Price));
            }
        }
        #endregion

        #region Constructors
        public GrowButton(GrowManager manager, Sprite background)
            : base(manager.Game, background, background, Vector2.Zero)
        {
            this.Manager = manager;
            this.Price = 0;
            this.CoolDown = new Counter.Timer(manager.Game, 0);
            this.CoolDown.OnMeet += new Counter.EventOnCounterMeet(this.OnCoolDownTick);
            this.visualCoolDown = new Counter.Timer(manager.Game, 0);
            this.visualCoolDown.OnMeet += new Counter.EventOnCounterMeet(this.OnVisualCoolDownTick);
            this.coolDownImage = SpriteBank.GetSprite(@"Images\Controls\CoolDown");
        }

        public GrowButton(GrowButton grButton)
            : base(grButton)
        {
            this.Manager = grButton.Manager;
            this.Price = grButton.Price;
            this.CoolDown = new Counter.Timer(grButton.CoolDown);
            this.visualCoolDown = new Counter.Timer(grButton.visualCoolDown);
            this.coolDownImage = new Sprite(grButton.coolDownImage);
            this.visualCoolDown.OnMeet += new Counter.EventOnCounterMeet(this.OnVisualCoolDownTick);
            this.CoolDown.OnMeet += new Counter.EventOnCounterMeet(this.OnCoolDownTick);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (isCoolDowning)
            {
                this.CoolDown.Update(gameTime);
                this.visualCoolDown.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (isCoolDowning)
            {
                this.coolDownImage.Position = new Vector2(this.controlBackground.PositionX, this.controlBackground.PositionY + this.controlBackground.SizeY - this.coolDownImage.SizeY);
                this.coolDownImage.Draw(gameTime);
            }
        }

        protected abstract Plant.Plant CreatePlant();

        private void OnCoolDownTick(Counter.ICounter timer)
        {
            this.isCoolDowning = false;
            this.CoolDown.Stop();
            this.visualCoolDown.Stop();
            this.coolDownImage.SizeY = this.coolDownImage.Texture.Height;
        }

        private void OnVisualCoolDownTick(Counter.ICounter timer)
        {
            this.coolDownImage.SizeY -= this.coolDownImage.Texture.Height / 20f;
        }

        public Plant.Plant Buy()
        {
            if (!this.CanBuy)
                return null;

            this.Manager.Manager.GetSunManager.NumberOfSuns -= this.Price;
            this.isCoolDowning = true;
            this.visualCoolDown.Interval = TimeSpan.FromMilliseconds(this.CoolDown.Interval.TotalMilliseconds / 20.0);
            this.CoolDown.Start();
            this.visualCoolDown.Start();

            return this.CreatePlant();
        }

        public virtual void WaitForGrow()
        {
            this.controlBackground.Color = Color.DarkRed;
        }

        public virtual void StopWait()
        {
            this.controlBackground.Color = Color.White;
        }
        #endregion
    }
}
