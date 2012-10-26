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

namespace GameBaseXNA
{
    public abstract class SceneManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Dictionary<string, Scene> sceneBank;

        public Dictionary<string, Scene> SceneBank
        {
            get
            {
                return this.sceneBank;
            }
        }

        /// <summary>
        /// Contructor of ManagerScene class
        /// </summary>
        /// <param name="game"></param>
        public SceneManager(Game game)
            : base(game)
        {
            this.sceneBank = new Dictionary<string, Scene>();
            this.LoadScene();
        }

        protected abstract void LoadScene();

        /// <summary>
        /// This is where it can query for any required services and load content
        /// </summary>
        public override void Initialize()
        {

            Dictionary<string, Scene>.ValueCollection Copied = this.sceneBank.Values;

            foreach (Scene Item in Copied)
                    Item.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// This is where call Update of Scene which will be updated
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Dictionary<string, Scene>.ValueCollection Copied = this.sceneBank.Values;

            foreach (Scene Item in Copied)
                if (Item.Enabled)
                    Item.Update(gameTime);
 
            base.Update(gameTime);
        }

        /// <summary>
        /// This is where call Draw of Scene which will be shown
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Dictionary<string, Scene>.ValueCollection Copied = this.sceneBank.Values;

            foreach (Scene Item in Copied)
                if (Item.Visible)
                    Item.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
