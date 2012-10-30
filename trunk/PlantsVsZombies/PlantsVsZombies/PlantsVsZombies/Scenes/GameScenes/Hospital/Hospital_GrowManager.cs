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
using PlantsVsZombies.Plants_Bullets;
using PlantsVsZombies.Plants_Bullets.Plant;
using PlantsVsZombies.Plants_Bullets.Grows;
using PlantsVsZombies.Plants_Bullets.Grows.GrowButtons;

namespace PlantsVsZombies.Scenes.GameScenes.Hospital
{
    public class Hospital_GrowManager : Plants_Bullets.Grows.GrowManager
    {
        #region Constructors
        public Hospital_GrowManager(Plants_Bullets.Plant.PlantManager manager)
            : base(manager)
        {
            this.ChooseList.AddGrowButton(new SunFlowerButton(this));
            this.ChooseList.AddGrowButton(new PeaButton(this));
            this.ChooseList.AddGrowButton(new DoublePeaButton(this));
            this.ChooseList.AddGrowButton(new IcePeaButton(this));
        }
        #endregion
    }
}
