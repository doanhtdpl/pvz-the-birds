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

namespace PlantsVsZombies.Plants_Bullets.Grows
{
    public class GrowManager : DrawableGameComponent
    {
        #region Sub-types
        #endregion

        #region Fields & Properties
        public Plant.PlantManager Manager { get; set; }

        private Counter.Timer moveTimer { get; set; }
        public bool IsPlaying { get; set; }

        public ChooseList ChooseList { get; set; }
        public BuyList BuyList { get; set; }
        
        protected GrowButtons.GrowButton waitedButton = null;
        #endregion

        #region Constructors
        public GrowManager(PlantManager manager)
            : base(manager.Game)
        {
            this.Manager = manager;
            this.IsPlaying = false;
            this.ChooseList = new ChooseList(this);
            this.ChooseList.Position = new Vector2(-500f, 110f);
            this.Manager.GetSunManager.SunBankLocation = new Vector2(35f, 5f);
            this.BuyList = new BuyList(this);
            this.BuyList.Position = new Vector2(110f, -100f);
        }
        #endregion

        #region Methods
        public void Buy(GrowButtons.GrowButton grButton)
        {
            this.waitedButton = grButton;
            if (this.waitedButton != null)
            {
                this.waitedButton.WaitForGrow();
            }
        }

        public void Grow(Vector2 position)
        {
            if (this.waitedButton == null)
                return;

            this.waitedButton.StopWait();
            GrowButtons.GrowButton grButton = this.waitedButton;
            this.waitedButton = null;

            Griding.Cell cell = this.Manager.GetGriding.IndexOf(position);
            if (cell != null)
            {
                foreach (Griding.IGridable grc in cell.Components)
                {
                    if (grc is Plant.Plant)
                        return;
                }

                Plant.Plant plant = grButton.Buy();
                if (plant != null)
                {
                    plant.Position = position;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if ((this.waitedButton != null) && GMouse.IsLeftButtonClicked)
                this.Grow(GMouse.MousePosition);

            this.ChooseList.Update(gameTime);
            this.BuyList.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.ChooseList.Draw(gameTime);
            this.BuyList.Draw(gameTime);

            base.Draw(gameTime);
        }

        public virtual void MoveIn()
        {
            this.ChooseList.PositionX += 5f;
            this.BuyList.PositionY += 1f;
        }

        public virtual void MoveOut()
        {
            this.ChooseList.PositionX -= 5f;
        }

        public virtual void OnSunChanged()
        {
            foreach (Control control in this.BuyList.listControl)
            {
                GrowButtons.GrowButton grBt = control as GrowButtons.GrowButton;
                if (grBt != null)
                {
                    if (!grBt.LastCanBuy && (grBt.Price <= this.Manager.GetSunManager.NumberOfSuns))
                    {
                        grBt.LastCanBuy = true;
                        grBt.ControlBackground.Color = GMath.DeGammaBlend(grBt.ControlBackground.Color, Color.Black, 0.4f);
                    }
                    else if (grBt.LastCanBuy && (grBt.Price > this.Manager.GetSunManager.NumberOfSuns))
                    {
                        grBt.LastCanBuy = false;
                        grBt.ControlBackground.Color = GMath.GammaBlend(grBt.ControlBackground.Color, Color.Black, 0.4f);
                    }
                }
            }
        }

        public virtual void Play()
        {
            this.BuyList.Play();
            this.OnSunChanged();
        }
        #endregion
    }
}
