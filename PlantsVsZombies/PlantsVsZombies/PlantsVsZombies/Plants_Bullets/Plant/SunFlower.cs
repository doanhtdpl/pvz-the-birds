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

namespace PlantsVsZombies.Plants_Bullets.Plant
{
    public class SunFlower : SupportPlant
    {
        // SunEngine
        protected PlantSunEngine sunEngine;

        public SunFlower(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
            sunEngine = new PlantSunEngine(game, plantManager.GetSunManager,
                plantManager.GetSunManager.SunBankLocation, this.position);
            sunEngine.SetGriding = plantManager.GetGriding;
            sunEngine.SunValue = 50;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            sunEngine.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void SetAnimation()
        {
            this.animations.Add(SpriteBank.GetAnimation("Images\\Plants\\Sunflower"));
            this.currentAnimation = this.animations[0];
            base.SetAnimation();
        }
    }
}
