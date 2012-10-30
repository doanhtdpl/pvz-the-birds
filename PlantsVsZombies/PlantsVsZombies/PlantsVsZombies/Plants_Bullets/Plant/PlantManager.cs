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


namespace PlantsVsZombies.Plants_Bullets.Plant
{
    public class PlantManager : DrawableGameComponent
    {
        // Bullet manager all bullet in game
        protected Bullets.BulletManager bulletManager;
        // Reference to griding in game
        protected Griding.Griding griding;
        // Sun Manager all sun in game
        protected SunManager sunManager;

        // List Plant
        protected List<Plant> plants;

        // Properties
        public Bullets.BulletManager GetBulletManager
        {
            get { return this.bulletManager; }
        }
        public Griding.Griding GetGriding
        {
            get { return this.griding; }
        }
        public SunManager GetSunManager
        {
            get { return this.sunManager; }
        }

        // Constructor
        public PlantManager(Game game, Griding.Griding griding)
            : base(game)
        {
            this.bulletManager = new Bullets.BulletManager(game);
            this.griding = griding;
            this.sunManager = new SunManager(game);
            this.sunManager.SetGriding = griding;

            bulletManager.Griding = griding;

            this.Initialize();
        }

        public override void Initialize()
        {
            plants = new List<Plant>();
            base.Initialize();
        }

        // Update all plant of plant list
        public override void Update(GameTime gameTime)
        {
            // Update all bullet
            bulletManager.Update(gameTime);
            // Update all sun
            sunManager.Update(gameTime);

            // Update all plan
            List<Plant> plantsCopy = new List<Plant>(plants);
            foreach (Plant plant in plantsCopy)
            {
                plant.Update(gameTime);
            }

            // Check for dead plant
            DiedPlantDetect();

            base.Update(gameTime);
        }

        // Draw all plant of plant list
        public override void Draw(GameTime gameTime)
        {
            // Draw all bullet in game
            bulletManager.Draw(gameTime);
            // Draw all sun in game
            sunManager.Draw(gameTime);

            // Draw all plant
            List<Plant> plantsCopy = new List<Plant>(plants);
            foreach (Plants_Bullets.Plant.Plant plant in plantsCopy)
            {
                plant.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        // Add new Plant to plant list
        public void AddPlant(Plant plant)
        {
            this.plants.Add(plant);
            this.griding.Add(plant);
        }

        // Detect plant is died and remove it from plant list 
        public void DiedPlantDetect()
        {
            if(plants.Count != 0)
            {
                for (int i = 0; i < plants.Count; ++i)
                {
                    if (plants[i].IsDead || plants[i].Health <= 0)
                    {
                        griding.Remove(plants[i]);
                        plants.Remove(plants[i]);
                        --i;
                    }
                }
            }
        }

        // Remove all plant in list
        public void Remove()
        {
            foreach (Plant plant in plants)
            {
                 this.griding.Remove(plant);
            }
            this.plants.Clear();
        }
    }
}
