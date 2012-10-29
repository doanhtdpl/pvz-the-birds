﻿using System;
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

        public ChooseList ChooseList { get; set; }
        public BuyList BuyList { get; set; }

        protected GrowButtons.GrowButton waitedButton = null;
        #endregion

        #region Constructors
        public GrowManager(PlantManager manager)
            : base(manager.Game)
        {
            this.Manager = manager;
            this.ChooseList = new ChooseList(this);
            this.BuyList = new BuyList(this);
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
                    this.Manager.AddPlant(plant);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
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
        #endregion
    }
}