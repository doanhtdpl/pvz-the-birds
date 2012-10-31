using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameBaseXNA;

namespace PlantsVsZombies
{
    public class Partical
    {
        public Partical(Sprite partical, Vector2 position, Vector2 velocity, int timeToLive, Color colorOver,
                        float angle, float angular, Vector2 size)
        {
            this.partical = new Sprite(partical);
            this.Texture.Position = position;
            this.velocity = velocity;

            this.timeToLive = timeToLive;
//            this.Texture.Color = colorOver;

            this.Texture.Angle = angle;
            this.angular = angular;
            if (size != Vector2.Zero)
                this.Texture.Size = size;
        }

        public void UpdatePartical(GameTime gameTime)
        {
            this.Texture.Position += velocity;
            this.Texture.Angle += angular;
        }

        public void DrawPartical(GameTime gameTime)
        {
            partical.Draw(gameTime);
        }

        protected void CheckTimeToLive()
        {
        }

        // Fields
        protected Sprite partical;
        protected Vector2 velocity;
        protected float angular;

        protected int timeToLive;

        // Properties
        public Sprite Texture
        {
            get { return this.partical; }
            set { this.partical = value; }
        }
        public Vector2 Position
        {
            get { return this.Texture.Position; }
            set { this.Texture.Position = value; }
        }
        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        public int TimeToLive
        {
            get { return this.timeToLive; }
            set { this.timeToLive = value; }
        }

        public Color ColorOver
        {
            get { return this.Texture.Color; }
            set { this.Texture.Color = value; }
        }
        public byte ColorOverR
        {
            get { return this.Texture.ColorR; }
            set { this.Texture.ColorR = value; }
        }
        public byte ColorOverG
        {
            get { return this.Texture.ColorG; }
            set { this.Texture.ColorG = value; }
        }
        public byte ColorOverB
        {
            get { return this.Texture.ColorB; }
            set { this.Texture.ColorB = value; }
        }
        public byte ColorOverA
        {
            get { return this.Texture.ColorA; }
            set { this.Texture.ColorA = value; }
        }

        public float Angle
        {
            get { return this.Texture.Angle; }
            set { this.Texture.Angle = value; }
        }
        public float Angular
        {
            get { return this.angular; }
            set { this.angular = value; }
        }
        public Vector2 Size
        {
            get { return this.Texture.Size; }
            set { this.Texture.Size = value; }
        }
    }
}
