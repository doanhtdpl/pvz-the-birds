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
    public class B_Cherry : BulletBomber
    {
        protected Vector2 cellSize;
        protected Rectangle mapRange;
        protected Rectangle bound;

        protected new Rectangle Bound
        {
            get { return this.bound; }
        }

        public B_Cherry(Game game, Vector2 position, Vector2 cellSize, Rectangle mapRange)
            : base(game, position)
        {
            this.cellSize = cellSize;
            this.mapRange = mapRange;
            CalculateRange();
        }

        public override void Initialize()
        {
            this.BName = "Images\\Plants\\B_Cherry";
            //this.B_Effect = @"Images\\Plants\\B_CherryEffect";
            this.damage = 500;
            base.Initialize();
        }

        protected void CalculateRange()
        {
            Vector2 center = this.position;
            Animation aniCenter = SpriteBank.GetAnimation(this.BName);
            aniCenter.Position = center;
            bound = new Rectangle((int)center.X, (int)center.Y, 2 * (int)cellSize.X, 2 * (int)cellSize.Y);
            animations.Add(aniCenter);

            Vector2 left = Vector2.Zero;
            bool isLeft = false;
            if (center.X - cellSize.X >= mapRange.X)
            {
                left = new Vector2(center.X - cellSize.X, center.Y);
                Animation aniLeft = SpriteBank.GetAnimation(this.BName);
                aniLeft.Position = left;
                animations.Add(aniLeft);
                isLeft = true;
                bound.X = (int)left.X;
            }

            Vector2 right = Vector2.Zero;
            bool isRight = false;
            if (center.X <= mapRange.X + mapRange.Width - cellSize.X)
            {
                right = new Vector2(center.X + cellSize.X, center.Y);
                Animation aniRight = SpriteBank.GetAnimation(this.BName);
                aniRight.Position = right;
                animations.Add(aniRight);
                isRight = true;
                bound.Y = (int)center.Y;
            }

            Vector2 top = Vector2.Zero;
            bool isTop = false;
            if (center.Y - cellSize.Y >= mapRange.Y)
            {
                top = new Vector2(center.X, center.Y - cellSize.Y);
                Animation aniTop = SpriteBank.GetAnimation(this.BName);
                aniTop.Position = top;
                animations.Add(aniTop);
                isTop = true;
            }

            Vector2 bottom = Vector2.Zero;
            bool isBottom = false;
            if (center.Y <= mapRange.Y + mapRange.Height - cellSize.Y)
            {
                bottom = new Vector2(center.X, center.Y + cellSize.Y);
                Animation aniBot = SpriteBank.GetAnimation(this.BName);
                aniBot.Position = bottom;
                animations.Add(aniBot);
                isBottom = true;
            }

            // Calculate bound
            if (isLeft && isRight)
                bound.Width = 3 * (int)cellSize.X;
            if (isTop && isBottom)
                bound.Height = 3 * (int)cellSize.Y;

            // Around
            if (isLeft && isTop)
            {
                Animation aniLeftTop = SpriteBank.GetAnimation(this.BName);
                aniLeftTop.Position = new Vector2(left.X , top.Y);
                animations.Add(aniLeftTop);
            }
            if (isLeft && isBottom)
            {
                Animation aniLeftBot = SpriteBank.GetAnimation(this.BName);
                aniLeftBot.Position = new Vector2(left.X, bottom.Y);
                animations.Add(aniLeftBot);
            }
            if (isRight && isTop)
            {
                Animation aniRightTop = SpriteBank.GetAnimation(this.BName);
                aniRightTop.Position = new Vector2(right.X, top.Y);
                animations.Add(aniRightTop);
            }
            if (isRight && isBottom)
            {
                Animation aniRightBot = SpriteBank.GetAnimation(this.BName);
                aniRightBot.Position = new Vector2(right.X, bottom.Y);
                animations.Add(aniRightBot);
            }
        }
    }

    public class B_CherryEngine : BulletEngine
    {
        // Methods
        public B_CherryEngine(Game game, BulletManager bulletManager)
            : base(game, bulletManager)
        {
        }

        // Add Pea Bullet
        public override void AddBullet(Vector2 position)
        {
            B_Cherry b_pea = new B_Cherry(this.Game, position, 
                new Vector2(bulletManager.Griding.CellWidth, bulletManager.Griding.CellHeight),
                bulletManager.Griding.Range);
            this.Add(b_pea);
        }
    }
}
