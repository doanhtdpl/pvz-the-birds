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
    // Class manage all bullet in game
    public class BulletManager : DrawableGameComponent
    {
        // Fields
        List<Bullet> bullets;

        public BulletManager(Game game)
            : base(game)
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            bullets = new List<Bullet>();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            List<Bullet> bulletsCopy = new List<Bullet>(bullets);
            foreach (Bullet bullet in bulletsCopy)
            {
                bullet.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            List<Bullet> bulletsCopy = new List<Bullet>(bullets);
            foreach (Bullet bullet in bulletsCopy)
            {
                bullet.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        public bool AddBullet(Bullet bullet)
        {
            bullets.Add((Bullet)bullet);
            return true;
        }

        public void AutoRemove()
        {
//             List<Bullet> bulletsCopy = new List<Bullet>(bullets);
//             foreach (Bullet bullet in bulletsCopy)
//             {
//             }
        }
    }
}
