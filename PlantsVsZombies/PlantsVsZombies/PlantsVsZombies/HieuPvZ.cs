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
            //SetAnimationData();

            base.Initialize();
        }

        // LoadContent will be called once per game and is the place to load
        // all of your content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

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
            SpriteBank.SetAnimationData("Image\\Cherry0", 127, 86, 8, 24);
            SpriteBank.SetAnimationData("Image\\CherryBullet0", 74, 80, 14, 17);

            SpriteBank.SetAnimationData("Image\\Chilli0", 78, 75, 13, 24);
            SpriteBank.SetAnimationData("Image\\ChilliBullet0", 31, 44, 22, 22);

            SpriteBank.SetAnimationData("Image\\DoublePeashooter0", 100, 55, 10, 40);

            SpriteBank.SetAnimationData("Image\\IcePeashooter0", 118, 63, 9, 33);
            SpriteBank.SetAnimationData("Image\\IcePeashooterBullet", 29, 22, 1, 1);

            SpriteBank.SetAnimationData("Image\\PeaShooter0", 92, 62, 11, 33);
            SpriteBank.SetAnimationData("Image\\PeaShooterBullet", 29, 22, 1, 1);

            SpriteBank.SetAnimationData("Image\\FreeMushroom0", 44, 34, 24, 35);
            SpriteBank.SetAnimationData("Image\\FreeMushroomBullet", 26, 11, 1, 1);

            SpriteBank.SetAnimationData("Image\\Mine0", 83, 47, 12, 17);
            SpriteBank.SetAnimationData("Image\\Mine1", 113, 52, 9, 30);

            SpriteBank.SetAnimationData("Image\\Stone0", 101, 101, 10, 38);

            SpriteBank.SetAnimationData("Image\\Sun0", 51, 40, 1, 1);

            SpriteBank.SetAnimationData("Image\\SunFlower0", 85, 60, 12, 48);

            SpriteBank.SetAnimationData("Image\\SunMushroom0", 46, 38, 23, 26);
            SpriteBank.SetAnimationData("Image\\SunMushroom1", 53, 44, 23, 26);

            SpriteBank.SetAnimationData("Image\\WaterMushroom0", 58, 51, 18, 48);
            SpriteBank.SetAnimationData("Image\\WaterMushroomBullet", 31, 24, 1, 1);
            // Heroes
            SpriteBank.SetAnimationData("Image\\Cyclop0", 111, 96, 9, 17);
            SpriteBank.SetAnimationData("Image\\Cyclop1", 131, 94, 9, 17);
            SpriteBank.SetAnimationData("Image\\CyclopStand0", 127, 86, 2, 2);

            SpriteBank.SetAnimationData("Image\\Gambit0", 138, 98, 4, 4);
            SpriteBank.SetAnimationData("Image\\Gambit1", 155, 106, 6, 6);
            SpriteBank.SetAnimationData("Image\\GambitStand0", 154, 95, 6, 24);

            SpriteBank.SetAnimationData("Image\\Iceman0", 146, 82, 6, 12);
            SpriteBank.SetAnimationData("Image\\Iceman1", 146, 100, 6, 9);
            SpriteBank.SetAnimationData("Image\\IcemanStand0", 146, 128, 6, 12);

            SpriteBank.SetAnimationData("Image\\Jubbernaut0", 212, 117, 4, 16);
            SpriteBank.SetAnimationData("Image\\Jubbernaut1", 279, 154, 3, 6);
            SpriteBank.SetAnimationData("Image\\JubbernautStand0", 185, 149, 5, 10);

            SpriteBank.SetAnimationData("Image\\Rogue0", 97, 90, 10, 20);
            SpriteBank.SetAnimationData("Image\\Rogue1", 160, 80, 6, 14);
            SpriteBank.SetAnimationData("Image\\RogueStand0", 125, 100, 8, 15);

            SpriteBank.SetAnimationData("Image\\Samurai0", 95, 74, 10, 12);
            SpriteBank.SetAnimationData("Image\\Samurai1", 159, 77, 6, 6);
            SpriteBank.SetAnimationData("Image\\SamuraiStand0", 77, 94, 2, 2);

            SpriteBank.SetAnimationData("Image\\Sentinel0", 178, 138, 5, 18);
            SpriteBank.SetAnimationData("Image\\Sentinel1", 272, 170, 3, 12);
            SpriteBank.SetAnimationData("Image\\SentinelStand0", 138, 136, 7, 31);

            SpriteBank.SetAnimationData("Image\\Shuma0", 133, 98, 7, 16);

            SpriteBank.SetAnimationData("Image\\Spiral0", 112, 86, 9, 27);
            SpriteBank.SetAnimationData("Image\\Spiral1", 133, 78, 6, 6);
            SpriteBank.SetAnimationData("Image\\SpiralStand0", 79, 89, 4, 4);

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