using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;
using System.IO;

namespace PlantsVsZombies
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class HieuPvZ : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Plants_Bullets.Bullets.BulletManager bulletManager;
        Plants_Bullets.Plant.PlantManager plantManager;
        Zombies.Managers.ZombiesManager ZMan;
        Griding.Griding griding;

        Sprite background;

        // Plants
//        Plants_Bullets.Plant.SunFlower sunflower;
        Plants_Bullets.Plant.Pea pea;

        // Sun Manager
        Plants_Bullets.Plant.SunManager sunManager;

        public HieuPvZ()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.TargetElapsedTime = TimeSpan.FromTicks(333333);
            this.IsMouseVisible = true;

            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 480;

            this.graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            GSound.SetGame(this);
            SpriteBank.SetGame(this);
            SetAnimationData();

            base.Initialize();
        }

        // LoadContent will be called once per game and is the place to load
        // all of your content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            font = Content.Load<SpriteFont>("font");

            background = SpriteBank.GetSprite("Images\\Background");
            background.Position = new Vector2(10f, 10f);
            Components.Add(background);

            // Initialize game content
            bulletManager = new Plants_Bullets.Bullets.BulletManager(this);
            Components.Add(bulletManager);

            // Griding
            griding = new Griding.Griding(this, new Rectangle(10, 10, 771, 420),
                                            5, 9);
            Components.Add(griding);

            // Init sun manager
            sunManager = new Plants_Bullets.Plant.SunManager(this);
            sunManager.SetGriding = griding;

            // Plant Manager
            plantManager = new Plants_Bullets.Plant.PlantManager(this, bulletManager, griding, sunManager);
            Components.Add(plantManager);
            Components.Add(sunManager);

            ZMan = new Zombies.Managers.ZombiesManager(this.griding);
            this.Components.Add(ZMan);
        }

        // UnloadContent will be called once per game and is the place to unload
        // all content.
        protected override void UnloadContent()
        {
        }

        // Allows the game to run logic such as updating the world,
        // checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            //Input Update:
            GMouse.Update(gameTime);
            GKeyBoard.Update(gameTime);

            if (GKeyBoard.IsKeyPressed(Keys.Escape))
            {
                this.Exit();
            }

            if (GMouse.IsRightButtonClicked)
            {
                Plants_Bullets.Plant.Plant plant = new Plants_Bullets.Plant.DPea(this, plantManager, GMouse.MousePosition);
            }

            if (GMouse.IsLeftButtonClicked)
            {
                Zombies.Zombie zombie = new Zombies.Skeletons.BladeSkeleton(this);
                zombie.Position = GMouse.MousePosition;
                this.ZMan.Add(zombie);
            }

            base.Update(gameTime);
        }

        // This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Mouse " + GMouse.MousePosition.X + "," + GMouse.MousePosition.Y, 
                                    new Vector2(100f, 0f), Color.Chocolate);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SetAnimationData()
        {
            //Zombies
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\BarrowWight\Attack", 73, 101, 14, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\BarrowWight\Death", 103, 105, 9, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\BarrowWight\Walk", 64, 81, 16, 17);

            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\Skeleton\Attack", 129, 102, 7, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\Skeleton\Death", 156, 134, 6, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\Skeleton\Walk", 62, 82, 16, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\Tattered\Attack", 90, 105, 11, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\Tattered\Death", 103, 114, 9, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\Tattered\Walk", 64, 95, 16, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\GiantSkeletonSword\Attack", 131, 125, 7, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\GiantSkeletonSword\Death", 139, 146, 7, 21);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\GiantSkeletonSword\Walk", 112, 99, 9, 19);

            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\BoneGolem\Attack", 275, 273, 3, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\BoneGolem\Death", 121, 134, 8, 18);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\BoneGolem\Walk", 153, 149, 6, 23);

            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\GiantSkeletonBlade\Attack", 202, 191, 5, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\GiantSkeletonBlade\Death", 169, 193, 6, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Skeletons\GiantSkeletonBlade\Walk", 159, 141, 6, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Spider\Attack", 65, 50, 11, 11);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Spider\Death", 43, 44, 11, 11);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Spider\Walk", 51, 43, 12, 12);

            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Giant\Attack", 141, 104, 7, 11);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Giant\Death", 89, 93, 11, 11);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Giant\Walk", 107, 86, 9, 12);

            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Remorhaz\Attack", 236, 209, 4, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Remorhaz\Death", 190, 190, 5, 21);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Remorhaz\Walk", 190, 190, 5, 28);

            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Drider\Attack", 126, 115, 8, 14);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Drider\Death", 163, 134, 6, 29);
            SpriteBank.SetAnimationData(@"Images\Zombies\Spiders\Drider\Walk", 186, 156, 5, 18);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Drowded\Attack", 86, 78, 11, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Drowded\Death", 97, 91, 10, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Drowded\Walk", 83, 87, 12, 20);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Ghoul\Attack", 107, 107, 9, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Ghoul\Death", 199, 136, 5, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Ghoul\Walk", 81, 91, 12, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Histachii\Attack", 106, 91, 9, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Histachii\Death", 87, 113, 11, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Histachii\Walk", 53, 92, 12, 12);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Lemure\Attack", 133, 108, 7, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Lemure\Death", 97, 107, 10, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Lemure\Walk", 109, 87, 9, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Mummy\Attack", 103, 92, 9, 15);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Mummy\Death", 79, 105, 12, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Mummy\Walk", 53, 88, 19, 20);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Nameless\Attack", 89, 101, 11, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Nameless\Death", 155, 130, 6, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Nameless\Walk", 73, 100, 14, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Nupperibo\Attack", 81, 82, 12, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Nupperibo\Death", 80, 83, 12, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Nupperibo\Walk", 69, 69, 14, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Ravel\Attack", 115, 84, 8, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Ravel\Death", 105, 96, 9, 16);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Ravel\Walk", 51, 74, 16, 16);

            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Vampire\Attack", 65, 76, 13, 13);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Vampire\Death", 89, 92, 11, 21);
            SpriteBank.SetAnimationData(@"Images\Zombies\Zombies\Vampire\Walk", 81, 79, 11, 11);

            //Plants & Bullets
            SpriteBank.SetAnimationData("Images\\Plants\\Cherry", 126, 86, 8, 24);
            SpriteBank.SetAnimationData("Images\\Bullets\\B_Cherry", 74, 80, 14, 17);

            SpriteBank.SetAnimationData("Images\\Plants\\Chilly", 78, 75, 13, 24);
            SpriteBank.SetAnimationData("Images\\Bullets\\B_Chili", 31, 44, 22, 22);

            SpriteBank.SetAnimationData("Images\\Plants\\DoublePea", 100, 55, 10, 40);

            SpriteBank.SetAnimationData("Images\\Plants\\IcePea", 113, 79, 9, 33);
            SpriteBank.SetAnimationData("Images\\Bullets\\B_IcePea", 29, 22, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\Pea", 92, 62, 11, 33);
            SpriteBank.SetAnimationData("Images\\Bullets\\B_Pea", 29, 22, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\FreeMush", 44, 34, 24, 35);
            SpriteBank.SetAnimationData("Images\\Bullets\\B_FreeMush", 26, 11, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\SeaMushroom", 58, 51, 18, 48);
            SpriteBank.SetAnimationData("Images\\Bullets\\B_WaterMush", 31, 24, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\Mine", 83, 47, 12, 17);
            SpriteBank.SetAnimationData("Images\\Plants\\MineGrow", 113, 53, 9, 30);

            SpriteBank.SetAnimationData("Images\\Plants\\Stone", 101, 101, 10, 38);

            SpriteBank.SetAnimationData("Images\\Plants\\Sun", 51, 40, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\Sunflower", 84, 60, 12, 48);

            SpriteBank.SetAnimationData("Images\\Plants\\SunMushroom", 46, 38, 23, 26);
            SpriteBank.SetAnimationData("Images\\Plants\\SunMushroomGreater", 60, 49, 17, 26);
        }
    }
}