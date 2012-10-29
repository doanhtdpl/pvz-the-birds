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
    public class BuyList : ListView
    {
        #region Fields & Properties
        public GrowManager Manager { get; set; }

        public int MaxItem { get; set; }
        public bool IsMax { get { return (this.listControl.Count >= this.MaxItem); } }
        #endregion

        #region Constructors
        public BuyList(GrowManager manager)
            : base(manager.Game)
        {
            this.Manager = manager;
            this.ControlBackground = SpriteBank.GetSprite(@"Images\Controls\ListBuyPlant");
            this.MaxItem = 8;
        }
        #endregion

        #region Methods
        public virtual bool AddGrowButton(GrowButtons.GrowButton grButton)
        {
            if (this.listControl.Count >= MaxItem)
                return false;

            if (!this.Add(grButton))
                return false;

            grButton.Clicked += new EventHandler(this.GrowButtonClickWhenChoosing);
            return true;
        }

        public virtual void GrowButtonClickWhenChoosing(object sender, EventArgs e)
        {
            if (this.Manager.ChooseList.Add((GrowButtons.GrowButton)sender))
            {
                this.RemoveControl((GrowButtons.GrowButton)sender);
            }
        }

        public virtual void RemoveGrowButton(GrowButtons.GrowButton grButton)
        {
            this.RemoveControl(grButton);
            grButton.Clicked -= new EventHandler(this.GrowButtonClickWhenChoosing);
        }

        public virtual void GrowButtonClickWhenPlaying(object sender, EventArgs e)
        {
            GrowButtons.GrowButton grButton = sender as GrowButtons.GrowButton;
            if ((grButton != null) && grButton.CanBuy)
                this.Manager.Buy(grButton);
        }

        public virtual void Play()
        {
            foreach (Control control in this.listControl)
            {
                GrowButtons.GrowButton grButton = control as GrowButtons.GrowButton;
                if (grButton != null)
                {
                    grButton.Clicked -= new EventHandler(this.GrowButtonClickWhenChoosing);
                    grButton.Clicked += new EventHandler(this.GrowButtonClickWhenPlaying);
                }
            }
        }

        #endregion
    }
}
