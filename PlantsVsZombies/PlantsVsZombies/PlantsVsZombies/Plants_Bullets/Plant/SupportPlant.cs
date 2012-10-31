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
    // Class Support Plant is base for Sunflower, Sun-mush, Stone
    public class SupportPlant : Plant
    {
        // Delay helper
        protected int delay;

        // Timer delay
        protected Counter.Timer delayTimer;


        public new Rectangle Bound
        {
            get 
            {
                Griding.Cell cell = plantManager.GetGriding.IndexOf(this.position);
                return cell.Range;
            }
        }

        public SupportPlant(Game game, PlantManager plantManager, Vector2 position)
            : base(game, plantManager, position)
        {
            this.AddToPlantManager();
        }

        public override void Initialize()
        {
            delayTimer = new Counter.Timer(this.Game, delay);
            delayTimer.OnMeet += new Counter.EventOnCounterMeet(delayTimer_OnMeet); ;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void SetAnimation()
        {
        }

        protected override void SetPosition()
        {
            this.PositionChanged = true;
            Griding.Cell cell = plantManager.GetGriding.IndexOf(this.position);
            if (cell == null)
                return;

            Vector2 pos = new Vector2(cell.Range.Left, cell.Range.Top);
            this.position = pos;
            foreach (Animation ani in animations)
            {
                ani.PositionX = pos.X;
                ani.PositionY = pos.Y + plantManager.GetGriding.CellHeight - CurrentAnimation.Bound.Height;
            }

            CalculateCenter();
        }

        protected override void CalculateCenter()
        {
            this.center.X = this.position.X;
            this.center.Y = this.position.Y;
            base.CalculateCenter();
        }

        protected virtual void delayTimer_OnMeet(object o)
        {
        }
    }
}
