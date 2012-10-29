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
using PlantsVsZombies.Zombies.Managers.Waves;
using PlantsVsZombies.Zombies.Managers;

namespace PlantsVsZombies.Scenes.GameScenes.Hospital
{
    public class Hospital_ZombiesGenerator : ZombieGenerator
    {
        #region Constructors
        public Hospital_ZombiesGenerator(ZombiesManager manager)
            : base(manager)
        {
            this.Waves.Add(new IdleWave(manager, 15000));
        }
        #endregion
    }
}
