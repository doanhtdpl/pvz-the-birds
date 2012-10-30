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
    public class PlantsVsZombies : SceneManager
    {
        #region Fields & Properties
        #endregion

        #region Constructors
        public PlantsVsZombies(Game game)
            : base(game)
        {

        }

        protected override void LoadScene()
        {
            this.sceneBank.Add("PlayLevel", new GameScenes.GameScene(this));

            this.sceneBank["PlayLevel"].StartUp();
            this.sceneBank["PlayLevel"].ShowScene();
        }
        #endregion

        #region Methods
        #endregion
    }
}
