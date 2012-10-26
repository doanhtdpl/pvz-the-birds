//#define Hieu

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

#if Hieu

namespace PlantsVsZombies
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PvZ : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public PvZ()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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

            // TODO: use this.Content to load your game content here
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

            // TODO: Add your update logic here

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

            base.Draw(gameTime);
        }

        private void SetAnimationData()
        {
        }
    }
}
#else

namespace PlantsVsZombies
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PvZ : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Zombies.States.Attack attackAnim;
        Zombies.States.Walk moveAnim;
        Zombies.States.Death deathAnim;
        Zombies.States.ZombieState currentAnim;
        List<Sprite> points = new List<Sprite>();
        Sprite root;
        bool run = true;
        Color textColor = Color.White;

        SpriteFont tahoma;

        public PvZ()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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

            // TODO: use this.Content to load your game content here
            attackAnim = new Zombies.States.Attack(SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Vampire\Attack"), 0);
            attackAnim.Image.Delay = 400000;
            moveAnim = new Zombies.States.Walk(SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Vampire\Walk"), 0f);
            moveAnim.Image.Delay = 400000;
            deathAnim = new Zombies.States.Death(SpriteBank.GetAnimation(@"Images\Zombies\Zombies\Vampire\Death"));
            deathAnim.Image.Delay = 400000;
            currentAnim = attackAnim;

            root = SpriteBank.GetSprite(@"Images\point");
            root.Position = new Vector2(400f, 250f);
            root.Color = Color.Red;

            tahoma = Content.Load<SpriteFont>("Tahoma");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.attackAnim.Image.DefaultFrame = 0;
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

            if (GKeyBoard.IsKeyPressed(Keys.Z))
            {
                currentAnim.Image.Reset();
                currentAnim = moveAnim;
            }
            if (GKeyBoard.IsKeyPressed(Keys.X))
            {
                currentAnim.Image.Reset();
                currentAnim = attackAnim;
            }
            if (GKeyBoard.IsKeyPressed(Keys.C))
            {
                currentAnim.Image.Reset();
                currentAnim = deathAnim;
            }

            if (GKeyBoard.IsKeyDown(Keys.Up))
                ++currentAnim.Align.Y;
            if (GKeyBoard.IsKeyDown(Keys.Down))
                --currentAnim.Align.Y;
            if (GKeyBoard.IsKeyDown(Keys.Left))
                ++currentAnim.Align.X;
            if (GKeyBoard.IsKeyDown(Keys.Right))
                --currentAnim.Align.X;

            currentAnim.Position = new Vector2(400f, 250f);

            // TODO: Add your update logic here
            if (GMouse.IsLeftButtonDoubleClick || GKeyBoard.IsKeyPressed(Keys.S))
            {
                if (File.Exists(@"C:\Users\Kieu Anh\Desktop\Align.txt"))
                    File.Delete(@"C:\Users\Kieu Anh\Desktop\Align.txt");

                StreamWriter wr = File.CreateText(@"C:\Users\Kieu Anh\Desktop\Align.txt");
                
                wr.WriteLine(string.Concat("Walk: ", moveAnim.Align.X, "\t", moveAnim.Align.Y));
                wr.WriteLine(string.Concat("Attack: ", attackAnim.Align.X, "\t", attackAnim.Align.Y));
                wr.WriteLine(string.Concat("Death: ", deathAnim.Align.X, "\t", deathAnim.Align.Y));
                wr.Close();

                points.Clear();
                textColor = GRandom.RandomSolidColor();
            }
            else if (GMouse.IsLeftButtonClicked)
            {
                Sprite point = SpriteBank.GetSprite(@"Images\point");
                point.Color = GRandom.RandomSolidColor();
                point.Position = GMouse.MousePosition;
                this.points.Add(point);
            }

            if (run)
                currentAnim.Update(gameTime);

            if (GKeyBoard.IsKeyPressed(Keys.Space))
            {
                currentAnim.Image.Reset();
                run = !run;
            }

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

            currentAnim.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(tahoma, string.Concat("Current align: ", currentAnim.Align.X, ", ", currentAnim.Align.Y), Vector2.Zero, textColor);
            spriteBatch.End();

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
#endif