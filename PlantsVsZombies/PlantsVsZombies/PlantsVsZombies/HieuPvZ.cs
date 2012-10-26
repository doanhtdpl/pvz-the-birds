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

        Plants_Bullets.Bullets.BulletManager bulletManager;
        Plants_Bullets.Plant.PlantManager plantManager;
        Griding.Griding griding;

        // Plants
        Plants_Bullets.Plant.Pea peashooter;

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

        // Allows the game to perform any initialization it needs to before starting to run.
        // This is where it can query for any required services and load any non-graphic
        // related content.  Calling base.Initialize will enumerate through any components
        // and initialize them as well.
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

            // Initialize game content
            bulletManager = new Plants_Bullets.Bullets.BulletManager(this);
            Components.Add(bulletManager);

            griding = new Griding.Griding(this, new Rectangle(0, 0, 540, 300),
                                            5, 9);
            Components.Add(griding);

            plantManager = new Plants_Bullets.Plant.PlantManager(this, bulletManager, griding);
            Components.Add(plantManager);

            peashooter = new Plants_Bullets.Plant.Pea(this, plantManager);
            plantManager.AddPlant(peashooter);
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

            base.Update(gameTime);
        }

        // This is called when the game should draw itself.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        private void SetAnimationData()
        {
            // Plants
            SpriteBank.SetAnimationData("Images\\Plants\\Cherry", 127, 86, 8, 24);
            SpriteBank.SetAnimationData("Images\\Plants\\B_Cherry", 74, 80, 14, 17);

            SpriteBank.SetAnimationData("Images\\Plants\\Chili", 78, 75, 13, 24);
            SpriteBank.SetAnimationData("Images\\Plants\\B_Chili", 31, 44, 22, 22);

            SpriteBank.SetAnimationData("Images\\Plants\\DPea", 100, 55, 10, 40);

            SpriteBank.SetAnimationData("Images\\Plants\\IcePea", 118, 63, 9, 33);
            SpriteBank.SetAnimationData("Images\\Plants\\B_IcePea", 29, 22, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\Pea", 92, 62, 11, 33);
            SpriteBank.SetAnimationData("Images\\Plants\\B_Pea", 29, 22, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\FreeMush", 44, 34, 24, 35);
            SpriteBank.SetAnimationData("Images\\Plants\\B_FreeMush", 26, 11, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\WaterMush", 58, 51, 18, 48);
            SpriteBank.SetAnimationData("Images\\Plants\\B_WaterMush", 31, 24, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\Mine0", 83, 47, 12, 17);
            SpriteBank.SetAnimationData("Images\\Plants\\Mine1", 113, 52, 9, 30);

            SpriteBank.SetAnimationData("Images\\Plants\\Stone", 101, 101, 10, 38);

            SpriteBank.SetAnimationData("Images\\Plants\\Sun", 51, 40, 1, 1);

            SpriteBank.SetAnimationData("Images\\Plants\\SunFlower", 85, 60, 12, 48);

            SpriteBank.SetAnimationData("Images\\Plants\\SunMush0", 46, 38, 23, 26);
            SpriteBank.SetAnimationData("Images\\Plants\\SunMush1", 53, 44, 23, 26);
        }

//         private void AddSprite()
//         {
//             sprites.Add(new Sprite(this, "IcePeashooterEffect"));
//             sprites.Add(new Sprite(this, "Particle"));
//             sprites.Add(new Sprite(this, "PeashooterEffect"));
//             sprites.Add(new Sprite(this, "WaterMushroomEffect"));
//         }
// 
//         private void InputProcess()
//         {
//             if (GMouse.IsRightButtonClicked)
//             {
//                 if (effect != null)
//                 {
//                     Components.Remove(effect);
//                     effect.Clear();
//                 }
//                 effect = new Effect(this, GMouse.MousePosition, GRandom.RandomInt(20) + 10, sprites, true, 30);
//                 Components.Add(effect);
//             }
//         }
// 
//         private void Check()
//         {
//             if (effect != null && effect.IsDead)
//             {
//                 effect.Clear();
//                 Components.Remove(effect);
//             }
//         }
    }
}