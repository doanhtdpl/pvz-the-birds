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
    public class Effect : DrawableGameComponent
    {
        public Effect(Game game, Vector2 position, int size, List<string> names, bool isTrans, int timeToLive)
            : base(game)
        {
            this.originalPos = position;
            this.size = size;
            this.isTrans = isTrans;
            this.timeToLive = timeToLive;
            this.spriteEngine = new List<Sprite>();
            foreach (string name in names)
            {
                spriteEngine.Add(new Sprite(game, name));
            }
            this.Initialize();
        }

        public Effect(Game game, Vector2 position, int size, List<Sprite> sprites, bool isTrans, int timeToLive)
            : base(game)
        {
            this.originalPos = position;
            this.size = size;
            this.isTrans = isTrans;
            this.timeToLive = timeToLive;
            this.spriteEngine = new List<Sprite>(sprites);
            this.Initialize();
        }

        public override void Initialize()
        {
            this.particalEngine = new List<Partical>();
            this.isDead = false;
            this.AddNewPartical();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Partical partical in particalEngine)
            {
                partical.UpdatePartical(gameTime);
                partical.TimeToLive--;
                if (isTrans)
                    partical.ColorOverA--;
            }
            IsComplete();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Partical partical in particalEngine)
            {
                partical.DrawPartical(gameTime);
            }
        }

        public void Clear()
        {
            particalEngine.Clear();
            spriteEngine.Clear();
        }

        protected void AddNewPartical()
        {
            for (int i = 0; i < size; ++i)
            {
                Vector2 velocity = new Vector2(1f * (float)GRandom.RandomDouble() * 4 - 1,
                                                1f * (float)GRandom.RandomDouble() * 4 - 1);
                int TTL = timeToLive;
                Color color = GRandom.RandomColor();
                float angular = 0.1f * (GRandom.RandomFloat() * 2f - 1f);
                Partical newPartical = new Partical(spriteEngine[GRandom.RandomInt(spriteEngine.Count - 1)], originalPos, velocity,
                                                    TTL, color, 0f, angular, Vector2.Zero);
                particalEngine.Add(newPartical);
            }
        }

        protected void IsComplete()
        {
            foreach (Partical partical in particalEngine)
            {
                if (partical.TimeToLive > 0)
                {
                    isDead = false;
                    return;
                }
            }
            isDead = true;
            return;
        }

        // Fields
        protected List<Partical> particalEngine;
        protected List<Sprite> spriteEngine;
        // The original of particalEngine
        protected Vector2 originalPos;
        // Report true if all partical is dead
        protected bool isDead;
        // The number of partical
        protected int size;
        // Allow the partical transparent gradually
        protected bool isTrans;
        // Time to live of effect
        protected int timeToLive;

        // Properties
        protected List<Sprite> SpriteEngine
        {
            get { return this.spriteEngine; }
            set { this.spriteEngine = value; }
        }
        public Vector2 OriginalPos
        {
            get { return this.originalPos; }
            set { this.originalPos = value; }
        }
        public bool IsDead
        {
            get { return this.isDead; }
        }
    }
}
