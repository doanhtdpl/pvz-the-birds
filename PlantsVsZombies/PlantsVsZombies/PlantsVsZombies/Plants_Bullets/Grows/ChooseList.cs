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
    public class ChooseList : ListView
    {
        #region Fields & Properties
        public GrowManager Manager { get; set; }
        #endregion

        #region Constructors
        public ChooseList(GrowManager manager)
            : base(manager.Game)
        {
            this.Manager = manager;
            this.ControlBackground = SpriteBank.GetSprite(@"Images\Controls\ListChoosePlant");
        }
        #endregion

        #region Methods
        public virtual bool AddGrowButton(GrowButtons.GrowButton grButton)
        {
            if (!this.Add(grButton))
                return false;

            grButton.Clicked += new EventHandler(this.GrowButtonChoose);
            return true;
        }

        public virtual void RemoveGrowButton(GrowButtons.GrowButton grButton)
        {
            this.RemoveControl(grButton);
            grButton.Clicked -= new EventHandler(this.GrowButtonChoose);
        }

        public virtual void GrowButtonChoose(object sender, EventArgs e)
        {
            if (this.Manager.BuyList.Add((GrowButtons.GrowButton)sender))
            {
                this.RemoveControl((GrowButtons.GrowButton)sender);
            }
        }
        #endregion
    }
}
