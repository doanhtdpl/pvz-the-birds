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
    public class LuongPvZ : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Sprite> points = new List<Sprite>();
        Sprite root;
        Color textColor = Color.White;
        Counter.Timer timer;
        Zombies.ZombiesManager ZMan;
        Griding.Griding grid;

        SpriteFont tahoma;

        public LuongPvZ()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromTicks(333333);
            this.IsMouseVisible = true;

            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 480;

            this.graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SpriteBank.SetGame(this);
            GSound.SetGame(this);
            this.SetAnimationData();

            grid = new Griding.Griding(this, this.GraphicsDevice.Viewport.Bounds, 16, 12);
            ZMan = new Zombies.ZombiesManager(this.grid);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            // TODO: use this.Content to load your game content here\
            root = SpriteBank.GetSprite(@"Images\point");
            root.Position = new Vector2(400f, 250f);
            root.Color = Color.Red;

            timer = new Counter.Timer(this, 0);
            timer.OnMeet += new Counter.EventOnCounterMeet(this.TimerTick);
            timer.Start();

            tahoma = Content.Load<SpriteFont>("Tahoma");
            this.Services.AddService(typeof(SpriteFont), tahoma);
        }

        void TimerTick(Counter.ICounter counter)
        {
            Zombies.Zombie zombie;
            if (GRandom.RandomLogic(0.2))
            {
                zombie = new Zombies.Skeletons.Skeleton(this);
            }
            else
            {
                zombie = new Zombies.Skeletons.BarrowWight(this);
            }

            zombie.Position = GRandom.RandomVector(0, 800, 0, 480);
            ZMan.Add(zombie);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Input Update:
            GMouse.Update(gameTime);
            GKeyBoard.Update(gameTime);

            // Allows the game to exit
            if (GKeyBoard.IsKeyPressed(Keys.Escape))
                this.Exit();

            if (GMouse.IsLeftButtonClicked)
            {
                Zombies.Skeletons.Skeleton zombie = new Zombies.Skeletons.Skeleton(this);
                zombie.Position = GMouse.MousePosition;
                ZMan.Add(zombie);
            }

            timer.Update(gameTime);
            grid.Update(gameTime);
            ZMan.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (Sprite point in this.points)
            {
                point.Draw(gameTime);
            }
            root.Draw(gameTime);

            ZMan.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void SetAnimationData()
        {
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
        }
    }
}