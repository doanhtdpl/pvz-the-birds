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

namespace PlantsVsZombies.Scenes
{
    public class ReturnalbeScene : Scene
    {
        #region Fields & Properties
        Scene Caller { get; set; }
        #endregion

        #region Constructors
        public ReturnalbeScene(SceneManager manager)
            : base(manager)
        {

        }
        #endregion
    }
}
