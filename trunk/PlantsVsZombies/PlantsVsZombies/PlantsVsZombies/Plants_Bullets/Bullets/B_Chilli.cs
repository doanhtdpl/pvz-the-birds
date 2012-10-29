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

namespace PlantsVsZombies.Plants_Bullets.Bullets
{
    public class B_Chili : BulletBomber
    {
        protected Vector2 cellSize;
        protected Rectangle mapRange;

        public new Rectangle Bound
        {
            get 
            {
                int height = ((int)this.position.Y / (int)cellSize.Y) * (int)cellSize.Y + mapRange.Y - (int)this.position.Y;
                return new Rectangle(mapRange.X, (int)position.Y, mapRange.Width, height); 
            }
        }

        public B_Chili(Game game, Vector2 position, Vector2 cellSize, Rectangle mapRange)
            : base(game, position)
        {
            this.cellSize = cellSize;
            this.mapRange = mapRange;
            CalculateRange();
        }

        public override void Initialize()
        {
            this.BName = "Images\\Plants\\B_Chili";
            //this.B_Effect = @"Images\\Plants\\B_ChilliEffect";
            this.damage = 500;

            base.Initialize();
        }

        protected void CalculateRange()
        {
            float tmp = this.position.X;
            while (tmp + cellSize.X <= mapRange.X + mapRange.Width)
            {
                Animation ani = SpriteBank.GetAnimation(this.BName);
                ani.Position = new Vector2(tmp, position.Y);
                animations.Add(ani);
                tmp += cellSize.X;
            }

            tmp = this.position.X;
            while (tmp - cellSize.X >= mapRange.X)
            {
                tmp -= cellSize.X;
                Animation ani = SpriteBank.GetAnimation(this.BName);
                ani.Position = new Vector2(tmp, position.Y);
                animations.Add(ani);
            }
        }
    }

    public class  B_ChiliEngine : BulletEngine
    {
        // Methods
        public B_ChiliEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_Chili b_pea = new B_Chili(this.Game, position,
                new Vector2(bulletManager.Griding.CellWidth, bulletManager.Griding.CellHeight),
                bulletManager.Griding.Range);
            this.Add(b_pea);
        }
    }
}
