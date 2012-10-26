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
    public class B_IcePea : BulletShooter
    {
        // Fields
        protected string BName = @"Images\\BPea";
        protected string BEffect = @"Images\\BPeaEffect";

        public B_IcePea(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            this.BSprite = SpriteBank.GetSprite(BName);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    public class B_IcePeaEngine
    {
        // Fields
        protected BulletManager bulletManager;

        // Methods
        public B_IcePeaEngine()
        {
        }

        // Add new Pea bullet to bulletManager
        public bool AddB_Pea(B_IcePea bPea)
        {
            bulletManager.AddBullet(bPea);
            return true;
        }
    }
}
