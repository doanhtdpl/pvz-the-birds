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
using System.IO;

namespace GameBaseXNA
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Sprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Attributes

        protected SpriteBatch spriteBatch;
        protected Texture2D texture;
        private Vector2 position = Vector2.Zero;
        private Vector2 size = Vector2.Zero;

        public Rectangle destRect = Rectangle.Empty;
        protected Rectangle srcRect = Rectangle.Empty;

        protected float angle = 0f;
        private Vector2 origin = Vector2.Zero;

        protected Color color = Color.White;
        protected float depth = 0f;
        protected SpriteEffects spriteEffect = SpriteEffects.None;

        protected BlendState blendState = BlendState.NonPremultiplied;

        #endregion

        #region Properties

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set 
            {
                this.position = value;
                destRect.X = GMath.Round(position.X + this.ScaleX * origin.X);
                destRect.Y = GMath.Round(position.Y + this.ScaleY * origin.Y);
            }
        }
		public float PositionX
		{
			get {return this.position.X;}
			set 
			{
				this.position.X = value;
                destRect.X = GMath.Round(position.X + this.ScaleX * origin.X);
			}
		}
		public float PositionY
		{
			get {return this.position.Y;}
			set 
			{
				this.position.Y = value;
                destRect.Y = GMath.Round(position.Y + this.ScaleY * origin.Y);
			}
		}
		
        public Vector2 Size
        {
            get { return this.size; }
            set
            {
                destRect.X = GMath.Round(this.origin.X * value.X / size.X + this.position.X);
                destRect.Y = GMath.Round(this.origin.Y * value.Y / size.Y + this.position.Y);
                this.size = value;
                destRect.Width = GMath.Round(size.X);
                destRect.Height = GMath.Round(size.Y);
            }
        }

        public float SizeX
        {
            get { return this.size.X; }
            set
            {
                destRect.X = GMath.Round(this.origin.X * value / size.X + this.position.X);
                this.size.X = value;
                destRect.Width = GMath.Round(size.X);
            }
        }

        public float SizeY
        {
            get { return this.size.Y; }
            set
            {
                destRect.Y = GMath.Round(this.origin.Y * value / size.Y + this.position.Y);
                this.size.Y = value;
                destRect.Height = GMath.Round(size.Y);
            }
        }

        public Vector2 Center
        {
            get { return new Vector2(this.position.X + this.size.X / 2, this.position.Y + size.Y / 2); }
        }
        public Vector2 SourceCenter
        {
            get { return new Vector2(srcRect.Width / 2, srcRect.Height / 2); }
        }

        public float Width
        {
            get { return this.texture.Width; }
        }

        public float Height
        {
            get { return this.texture.Height; }
        }

        public Vector2 Scale
        {
            get { return new Vector2(size.X / srcRect.Width, size.Y / srcRect.Height); }
            set
            {
                SizeX = value.X * srcRect.Width;
                SizeY = value.Y * srcRect.Height;
            }
        }
        public float ScaleX
        {
            get { return this.size.X / srcRect.Width; }
            set
            {
                this.SizeX = value * srcRect.Width;
            }
        }
        public float ScaleY
        {
            get { return this.size.Y / srcRect.Height; }
            set
            {
                this.SizeY = value * srcRect.Height;
            }
        }

        public Rectangle Bound
        {
            get { return new Rectangle(GMath.Round(position.X), GMath.Round(position.Y), destRect.Width, destRect.Height); }
        }

        public Rectangle SourceRectangle
        {
            get { return this.srcRect; }
            set
            {
                Vector2 scale = this.Scale;
                this.srcRect = value;
                SizeX = scale.X * srcRect.Width;
                SizeY =  scale.Y * srcRect.Height;
                this.Origin = this.SourceCenter;
            }
        }

        public float Angle
        {
            get { return this.angle; }
            set { this.angle = value; }
        }

        public Vector2 Origin
        {
            get { return this.origin; }
            set
            {
                this.origin = value;
                destRect.X = GMath.Round(ScaleX * origin.X + position.X);
                destRect.Y = GMath.Round(ScaleY * origin.Y + position.Y);
            }
        }
        public float OriginX
        {
            get { return this.origin.X; }
            set
            {
                this.origin.X = value;
                destRect.X = GMath.Round(ScaleX * origin.X + position.X);
            }
        }
        public float OriginY
        {
            get { return this.origin.Y; }
            set
            {
                this.origin.Y = value;
                destRect.Y = GMath.Round(ScaleY * origin.Y + position.Y);
            }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        public byte ColorA
        {
            get { return this.color.A; }
            set { this.color.A = value; }
        }
        public byte ColorB
        {
            get { return this.color.B; }
            set { this.color.B = value; }
        }
        public byte ColorG
        {
            get { return this.color.G; }
            set { this.color.G = value; }
        }
        public byte ColorR
        {
            get { return this.color.R; }
            set { this.color.R = value; }
        }

        public SpriteEffects Effect
        {
            get { return this.spriteEffect; }
            set { this.spriteEffect = value; }
        }

        public float Depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }

        public BlendState BlendStated
        {
            get { return this.blendState; }
            set { this.blendState = value; }
        }

        #endregion

        #region Initialize

        #region Constructor with PathName
        /// <summary>
        /// Create new Sprite with initial position, size, angle, origin, color
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="pathName">Path to image file</param>
        /// <param name="position">Position on screen</param>
        /// <param name="size">Size of Sprite</param>
        /// <param name="angle">Angle to rotate</param>
        /// <param name="origin">Origin to rotate with angle</param>
        /// <param name="color">Color to overlay</param>
        public Sprite(Game game, string pathName, Vector2 position, Vector2? size, float angle, Vector2? origin, Color? color)
            : base(game)
        {
            this.LoadImageFromFile(pathName);
            this.position = position;

            if (size == null)
                size = Vector2.Zero;
            else
                this.size = size.Value;

            this.angle = angle;

            if (origin == null)
                origin = new Vector2(texture.Width / 2, texture.Height / 2);
            else
                this.origin = origin.Value;

            if (color == null)
                color = Color.White;
            else 
                color = color.Value;


            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial position, angle
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="pathName">Path to image file</param>
        /// <param name="position">Position on screen</param>
        /// <param name="angle">Angle to rotate</param>
        public Sprite(Game game, string pathName, Vector2 position, float angle)
            : base(game)
        {
            this.LoadImageFromFile(pathName);
            this.position = position;
            this.angle = angle;
            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial position, angle, origin
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="pathName">Path to image file</param>
        /// <param name="position">Position on screen</param>
        /// <param name="angle">Angle to rotate</param>
        /// <param name="origin">Origin to rotate with angle</param>
        public Sprite(Game game, string pathName, Vector2 position, float angle, Vector2 origin)
            : base(game)
        {
            this.LoadImageFromFile(pathName);
            this.position = position;
            this.angle = angle;
            this.origin = origin;
            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial position, size
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="pathName">Path to image file</param>
        /// <param name="position">Position on screen</param>
        /// <param name="size">Size of Sprite</param>
        public Sprite(Game game, string pathName, Vector2 position, Vector2 size)
            : base(game)
        {
            this.LoadImageFromFile(pathName);
            this.position = position;
            this.size = size;
            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial angle, origin
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="pathName">Path to image file</param>
        /// <param name="angle">Angle to rotate</param>
        /// <param name="origin">Origin to rotate with angle</param>
        public Sprite(Game game, string pathName, float angle, Vector2 origin)
            : base(game)
        {
            this.LoadImageFromFile(pathName);
            this.angle = angle;
            this.origin = origin;
            this.Initialize();
        }

        /// <summary>
        /// Constructor to create new Sprite from file path Name in computer
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        public Sprite(Game game, string pathName)
            :base(game)
        {
            this.LoadImageFromFile(pathName);
            this.Initialize();
        }

        #endregion

        #region Constructor with Texture
        /// <summary>
        /// Create new Sprite with initial position, size, angle, origin, color
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="texture">Texture of Sprite</param>
        /// <param name="position">Position on screen</param>
        /// <param name="size">Size of Sprite</param>
        /// <param name="angle">Angle to rotate</param>
        /// <param name="origin">Origin to rotate with angle</param>
        /// <param name="color">Color to overlay</param>
        public Sprite(Game game, Texture2D texture, Vector2 position, Vector2? size, float angle, Vector2? origin, Color? color)
            : base(game)
        {
            this.texture = texture;
            this.position = position;

            if (size == null)
                size = Vector2.Zero;
            else
                this.size = size.Value;

            this.angle = angle;

            if (origin == null)
                origin = new Vector2(texture.Width / 2, texture.Height / 2);
            else
                this.origin = origin.Value;

            if (color == null)
                color = Color.White;
            else
                color = color.Value;


            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial position, angle
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="texture">Texture of Sprite</param>
        /// <param name="position">Position on screen</param>
        /// <param name="angle">Angle to rotate</param>
        public Sprite(Game game, Texture2D texture, Vector2 position, float angle)
            : base(game)
        {
            this.texture = texture;
            this.position = position;
            this.angle = angle;
            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial position, angle, origin
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="texture">Texture of Sprite</param>
        /// <param name="position">Position on screen</param>
        /// <param name="angle">Angle to rotate</param>
        /// <param name="origin">Origin to rotate with angle</param>
        public Sprite(Game game, Texture2D texture, Vector2 position, float angle, Vector2 origin)
            : base(game)
        {
            this.texture = texture;
            this.position = position;
            this.angle = angle;
            this.origin = origin;
            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial position, size
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="texture">Texture of Sprite</param>
        /// <param name="position">Position on screen</param>
        /// <param name="size">Size of Sprite</param>
        public Sprite(Game game, Texture2D texture, Vector2 position, Vector2 size)
            : base(game)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            this.Initialize();
        }

        /// <summary>
        /// Create new Sprite with initial angle, origin
        /// </summary>
        /// <param name="game">Current Game</param>
        /// <param name="texture">Texture of Sprite</param>
        /// <param name="angle">Angle to rotate</param>
        /// <param name="origin">Origin to rotate with angle</param>
        public Sprite(Game game, Texture2D texture, float angle, Vector2 origin)
            : base(game)
        {
            this.texture = texture;
            this.angle = angle;
            this.origin = origin;
            this.Initialize();
        }

        /// <summary>
        /// Constructor to create new Sprite from file path Name in computer
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture">Texture of Sprite</param>
        public Sprite(Game game, Texture2D texture)
            : base(game)
        {
            this.texture = texture;
            this.Initialize();
        }

        #endregion

        #region Default Constructor
        /// <summary>
        /// Constructor copy from the sprite have before
        /// </summary>
        /// <param name="sprite">Sprite need to copy</param>
        public Sprite(Sprite sprite)
            : base(sprite.Game)
        {
            this.texture = sprite.texture;
            this.position = sprite.position;
            this.srcRect = sprite.srcRect;
            this.size = sprite.size;

            this.angle = sprite.angle;
            this.origin = sprite.origin;

            this.color = sprite.color;
            this.spriteEffect = sprite.spriteEffect;
            this.depth = sprite.depth;
            this.blendState = sprite.blendState;

            this.Initialize();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="game"></param>
        public Sprite(Game game)
            : base(game)
        {
            this.Initialize();
        }
        #endregion

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            if(texture != null)
            {
                if (srcRect == Rectangle.Empty)
                    this.srcRect = new Rectangle(0, 0, texture.Width, texture.Height);

                if (size == Vector2.Zero)
                {
                    size.X = texture.Width;
                    size.Y = texture.Height;
                }

                if (this.origin == Vector2.Zero)
                {
                    this.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
                }
            }

            this.destRect = new Rectangle(GMath.Round(position.X), GMath.Round(position.Y), GMath.Round(size.X), GMath.Round(size.Y));

			destRect.X += GMath.Round (ScaleX * origin.X);
			destRect.Y += GMath.Round (ScaleY * origin.Y);

            base.Initialize();
        }

        protected virtual void LoadImageFromFile(string pathName)
        {
            texture = this.Game.Content.Load <Texture2D>(pathName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw the sprite on screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if(this.Visible)
            {
                spriteBatch.Begin(SpriteSortMode.Texture, blendState);
                spriteBatch.Draw(texture, destRect, srcRect, color, angle, origin, spriteEffect, depth);
                spriteBatch.End();
            }
        }

        #endregion
    }
    
    /// <summary>
    /// Create an animation on screen
    /// </summary>
    public class Animation : Sprite
    {
        #region Attributes

        List<Rectangle> frames;
        int activeFrame = -1;
        public int CurrentFrame { get { return this.activeFrame; } set { this.activeFrame = value; this.SourceRectangle = this.frames[activeFrame]; } }

        TimeSpan delay = new TimeSpan(100000);
        TimeSpan lastTime = new TimeSpan(-100000000);
        TimeSpan currentTime;

        bool enable = true;
        int defaultFrame = 0;    //default when it not enable, draw the first frame

        #endregion

        #region Properties

        public List<Rectangle> Frames
        {
            get { return this.frames; }
            set
            {
                this.frames = value;
                this.SizeX = frames[defaultFrame].Width;
                this.SizeY = frames[defaultFrame].Height;
            }
        }
        public long Delay
        {
            get { return (long) this.delay.TotalMilliseconds; }
            set { this.delay = TimeSpan.FromMilliseconds(value);}
        }
        public bool Enable
        {
            get { return this.enable; }
            set { this.enable = value; }
        }
        public int DefaultFrame
        {
            get { return this.defaultFrame; }
            set { this.defaultFrame = value; }
        }

        #endregion

        #region Initialize

        #region From pathName and Sprite
        /// <summary>
        /// Constructor create new animation from path name in PC
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        /// <param name="frames">List of frame need to draw on screen of Animation</param>
        /// <param name="default">Default Frame when Animation not enable to animate</param>
        public Animation(Game game, string pathName, List<Rectangle> frames, int defaultFrame)
            : base(game, pathName)
        {
            this.frames = frames;
            this.defaultFrame = defaultFrame;
            this.SourceRectangle = frames[defaultFrame];
            this.SizeX = frames[defaultFrame].Width;
            this.SizeY = frames[defaultFrame].Height;
            this.Initialize();
        }

        /// <summary>
        /// Constructor create new animation from path name in PC
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        /// <param name="frames">List of frame need to draw on screen of Animation</param>
        public Animation(Game game, string pathName, List<Rectangle> frames)
            : base(game, pathName)
        {
            this.frames = frames;
            this.SourceRectangle = frames[defaultFrame];
            this.SizeX = frames[defaultFrame].Width;
            this.SizeY = frames[defaultFrame].Height;
            this.Initialize();
        }

        public Animation(Game game, string pathname)
            : base(game, pathname)
        {
            this.SourceRectangle = frames[defaultFrame];
        }

        /// <summary>
        /// Constructor create new animation from path name in PC
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        /// <param name="position">Position hope to apprear on screen</param>
        public Animation(Game game, string pathName, Vector2 position)
            : base(game, pathName)
        {
            this.SourceRectangle = frames[defaultFrame];
            this.Initialize();
        }

        /// <summary>
        /// Constructor create new Animation from sprite
        /// </summary>
        /// <param name="sprite">Sprite of Animation</param>
        /// <param name="frames">List of frame need to draw on screen of Animation</param>
        public Animation(Sprite sprite, List<Rectangle> frames)
            :base(sprite)
        {
            this.frames = frames;
            this.SourceRectangle = frames[defaultFrame];
            this.SizeX = frames[defaultFrame].Width;
            this.SizeY = frames[defaultFrame].Height;
            this.Initialize();
        }

        /// <summary>
        /// Constructor create new Animation from sprite
        /// </summary>
        /// <param name="sprite">Sprite of Animation</param>
        public Animation(Sprite sprite)
            :base(sprite)
        {
            this.SourceRectangle = frames[defaultFrame];
            this.Initialize();
        }

        public Animation(Animation anim)
            : base(anim)
        {
            this.frames = anim.frames;
            this.activeFrame = anim.activeFrame;

            this.delay = anim.delay;
            this.lastTime = anim.lastTime;
            this.currentTime = anim.currentTime;

            this.enable = anim.enable;
            this.defaultFrame = anim.defaultFrame;
        }
        #endregion

        #region From Texture2D

        /// <summary>
        /// Constructor create new animation from path name in PC
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        /// <param name="frames">List of frame need to draw on screen of Animation</param>
        /// <param name="default">Default Frame when Animation not enable to animate</param>
        public Animation(Game game, Texture2D texture, List<Rectangle> frames, int defaultFrame)
            : base(game, texture)
        {
            this.frames = frames;
            this.defaultFrame = defaultFrame;
            this.SourceRectangle = frames[defaultFrame];
            this.SizeX = frames[defaultFrame].Width;
            this.SizeY = frames[defaultFrame].Height;
            this.Initialize();
        }

        /// <summary>
        /// Constructor create new animation from path name in PC
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        /// <param name="frames">List of frame need to draw on screen of Animation</param>
        public Animation(Game game, Texture2D texture, List<Rectangle> frames)
            : base(game, texture)
        {
            this.frames = frames;
            this.SourceRectangle = frames[defaultFrame];
            this.SizeX = frames[defaultFrame].Width;
            this.SizeY = frames[defaultFrame].Height;
            this.Initialize();
        }

        /// <summary>
        /// Constructor create new animation from path name in PC
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pathName">Path Name location of Picture on PC</param>
        /// <param name="position">Position hope to apprear on screen</param>
        public Animation(Game game, Texture2D texture, Vector2 position)
            : base(game, texture)
        {
            frames = new List<Rectangle>();
            this.Initialize();
        }
        #endregion

        #region default constructor
        /// <summary>
        /// Default Constructor 
        /// </summary>
        /// <param name="game"></param>
        public Animation(Game game)
            : base(game)
        {
            frames = new List<Rectangle>();
            this.Initialize();
        }
        #endregion

        public new void Initialize()
        {

        }
        #endregion

        #region Methods : Update & Draw

        /// <summary>
        /// Update Source Rectangle of Sprites
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime;
            if (this.enable)
            {
                if (currentTime - lastTime >= delay)
                {
                    ++activeFrame;
                    if (activeFrame >= frames.Count)
                        activeFrame = 0;
                    this.SourceRectangle = frames[activeFrame];

                    lastTime = currentTime;
                }
            }
            else
                this.SourceRectangle = frames[defaultFrame];
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the animation, if it not enable, just draw the default frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public virtual void Reset()
        {
            this.activeFrame = 0;
            this.SourceRectangle = this.frames[activeFrame];
        }
        #endregion
    }
}
