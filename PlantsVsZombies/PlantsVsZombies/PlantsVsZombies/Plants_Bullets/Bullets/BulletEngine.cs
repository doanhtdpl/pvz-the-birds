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


namespace PlantsVsZombies.Plants_Bullets.Bullets
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class BulletEngine : GameComponent
    {
        // Fields
        protected BulletManager bulletManager;

        public BulletEngine(Game game, BulletManager bulletManager)
            : base(game)
        {
            this.bulletManager = bulletManager;
            this.Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public abstract void AddBullet(Vector2 position);

        public virtual void Add(Bullet bullet)
        {
            this.bulletManager.AddBullet(bullet);
        }
    }
}
