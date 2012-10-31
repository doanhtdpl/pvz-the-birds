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
    enum GameSceneState
    {
        MoveIn = 0,
        Choosing = 1,
        MoveOut = 2,
        Play = 3
    }

    public class GameScene : Scene
    {
        #region Fields & Properties
        private GameSceneState state = GameSceneState.MoveIn;
        private Counter.Timer moveTimer;

        Sprite Background { get; set; }

        public Griding.Griding Grid { get; set; }
        public Zombies.Managers.ZombiesManager ZombiesManager { get; set; }
        public Plants_Bullets.Plant.PlantManager PlantManager { get; set; }
        public GrowManager GrowManager { get; set; }

        public Controls.Button playButton;
        #endregion

        #region Constructors
        public GameScene(SceneManager manager)
            : base(manager)
        {
            //Test:
            this.Background = SpriteBank.GetSprite(@"Images\Controls\Background_Forest");

            this.moveTimer = new Counter.Timer(this.Game, 20);
            this.state = GameSceneState.MoveIn;
            this.moveTimer.OnMeet += new Counter.EventOnCounterMeet(this.MoveIn);
            this.moveTimer.Start();

            this.playButton = new Controls.Button(this.Game, SpriteBank.GetSprite(@"Images\Controls\Ready"), SpriteBank.GetSprite(@"Images\Controls\ReadyOver"), Vector2.Zero);
            this.playButton.Position = new Vector2(-315f, 220f);
            this.playButton.Clicked += new EventHandler(this.PlayButtonOnClick);

            this.Grid = new Griding.Griding(this.Game, new Rectangle(0, 60, 760, 380), 5, 9);
            this.PlantManager = new Plants_Bullets.Plant.PlantManager(this.Game, this.Grid);
            this.GrowManager = new Hospital.Hospital_GrowManager(this.PlantManager);
            this.PlantManager.GetSunManager.OnSunChanged += new SunManager.OnSunChangedProc(this.GrowManager.OnSunChanged);
            this.PlantManager.GetSunManager.NumberOfSuns = 100;

            this.ZombiesManager = new Zombies.Managers.ZombiesManager(this.Grid);
            this.ZombiesManager.Generator = new Hospital.Hospital_ZombiesGenerator(this.ZombiesManager);
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            switch (this.state)
            {
                case GameSceneState.MoveIn:
                case GameSceneState.MoveOut:
                    this.Background.Update(gameTime);
                    this.moveTimer.Update(gameTime);
                    break;

                case GameSceneState.Choosing:
                    this.GrowManager.Update(gameTime);
                    this.playButton.Update(gameTime);
                    break;

                case GameSceneState.Play:
                    this.GrowManager.Update(gameTime);
                    this.Grid.Update(gameTime);;
                    this.PlantManager.Update(gameTime);
                    this.ZombiesManager.Update(gameTime);
                    if (GMouse.IsRightButtonClicked)
                    {
                        Zombie zombie = new Zombies.Spiders.GiantSpider(this.Game);
                        zombie.Position = GMouse.MousePosition;
                        this.ZombiesManager.Add(zombie);
                    }

                    break;
            }


            if (this.IsEndScene())
                this.ShowNextScene();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.Background.Draw(gameTime);
            this.GrowManager.Draw(gameTime);
            this.playButton.Draw(gameTime);
            this.ZombiesManager.Draw(gameTime);
            this.PlantManager.Draw(gameTime);

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

        public virtual void MoveIn(Counter.ICounter timer)
        {
            this.Background.PositionX -= 6.54f;
            this.GrowManager.MoveIn();
            this.playButton.PositionX += 5f;

            if (this.Background.PositionX <= -654f)
            {
                this.moveTimer.Stop();
                this.moveTimer.OnMeet -= new Counter.EventOnCounterMeet(this.MoveIn);
                this.state = GameSceneState.Choosing;
            }
        }

        public virtual void MoveOut(Counter.ICounter timer)
        {
            this.Background.PositionX += 3.9f;
            this.GrowManager.MoveOut();
            this.playButton.PositionX -= 5f;

            if (this.Background.PositionX >= -264)
            {
                this.moveTimer.Stop();
                this.moveTimer.OnMeet -= new Counter.EventOnCounterMeet(this.MoveOut);
                this.GrowManager.Play();
                this.ZombiesManager.Generator.Begin();
                this.state = GameSceneState.Play;
            }
        }

        public virtual void PlayButtonOnClick(object sender, EventArgs e)
        {
            if ((this.state == GameSceneState.Choosing) && (this.GrowManager.BuyList.IsMax || this.GrowManager.ChooseList.listControl.Count == 0))
            {
                this.state = GameSceneState.MoveOut;
                this.moveTimer.OnMeet += new Counter.EventOnCounterMeet(this.MoveOut);
                this.moveTimer.Start();
            }
        }
        #endregion
    }
}
