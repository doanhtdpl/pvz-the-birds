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
    /// <summary>
    /// Perform Game Scene
    /// </summary>
    public class Scene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Scene Caller { get; set; }

        /// <summary>
        /// Scene Components will be updated (and drawed) automatically by scene
        /// </summary>
        protected List<GameComponent> components;

        /// <summary>
        /// References to Scene Manager
        /// </summary>
        protected SceneManager SceneManager { get; set; }

        /// <summary>
        /// Comtructor method of Scene class
        /// </summary>
        /// <param name="game">Provides a snapshot of timing values</param>
        /// <param name="managerScene">Scene Manager</param>
        public Scene(SceneManager managerScene)
            : base(managerScene.Game)
        {
            this.components = new List<GameComponent>();
            this.SceneManager = managerScene;

            Visible = false;
            Enabled = false;

            this.Initialize();
        }

        /// <summary>
        /// Initialize for scene
        /// </summary>
        public virtual void StartUp()
        {
            this.Components.Clear();
        }

        /// <summary>
        /// The Method turn on two pennant of Scene which you want to show in BackGround
        /// </summary>
        public virtual void ShowScene()
        {
            Visible = true;
            Enabled = true;
        }

        /// <summary>
        /// The Method turn of two pennant of Scene which you want to hide in BackGround 
        /// </summary>
        public virtual void HideScene()
        {
            Visible = false;
            Enabled = false;
        }

        /// <summary>
        /// The Method treat the Scene when you press pause in game
        /// </summary>
        public virtual void Pause()
        {
            Visible = true;
            Enabled = false;
        }

        /// <summary>
        /// Call to Update Method all Component when the pennant (Enable) of the Scene is true
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            List<GameComponent> copied = new List<GameComponent>(this.components);

            foreach (GameComponent gc in copied)
            {
                if (gc.Enabled)
                    gc.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Call to Draw Method all component when the pennant (Visible) of the Scene is true 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent gc in this.components)
            {
                DrawableGameComponent dgc = gc as DrawableGameComponent;
                if (dgc != null && dgc.Visible)
                    dgc.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        public virtual void Return()
        {
            if (this.Caller != null)
            {
                this.HideScene();
                this.Caller.ShowScene();
            }
        }

        /// <summary>
        /// Scene Components
        /// </summary>
        public List<GameComponent> Components
        {
            get { return components; }
        }
    }
}
