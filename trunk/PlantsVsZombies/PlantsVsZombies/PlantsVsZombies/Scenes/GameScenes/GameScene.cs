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
using PlantsVsZombies.Plants_Bullets.Plant;
using PlantsVsZombies.Plants_Bullets.Grows;
using PlantsVsZombies.Zombies;

namespace PlantsVsZombies.Scenes.GameScenes
{
    public class GameScene : Scene
    {
        #region Fields & Properties
        Sprite Background { get; set; }

        public Griding.Griding Grid { get; set; }
        public Zombies.Managers.ZombiesManager ZombiesManager { get; set; }
        public Plants_Bullets.Plant.PlantManager PlantManager { get; set; }
        public GrowManager GrowManager { get; set; }
        #endregion

        #region Constructors
        public GameScene(SceneManager manager)
            : base(manager)
        {
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            this.Grid.Update(gameTime);
            this.ZombiesManager.Update(gameTime);
            this.PlantManager.Update(gameTime);
            this.GrowManager.Update(gameTime);

            if (this.IsEndScene())
                this.ShowNextScene();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.ZombiesManager.Draw(gameTime);
            this.PlantManager.Draw(gameTime);
            this.GrowManager.Draw(gameTime);

            base.Draw(gameTime);
        }

        public virtual bool IsEndScene()
        {
            return (this.ZombiesManager.IsCompleted);
        }

        public virtual void ShowNextScene()
        {
            this.HideScene();
        }
        #endregion
    }
}
