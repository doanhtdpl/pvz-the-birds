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
    public abstract class EffectCharacter : DrawableGameComponent
    {
        #region Attribute
        public SpriteFont Font { get; set; }
        public char Character { get; set; }
        public Texture2D texture { get; set; }
        protected Vector2 position;
        public Vector2 Position { get { return this.position; } set { this.position = value; } }
        public float PositionX { get { return this.position.X; } set { this.position.X = value; } }
        public float PositionY { get { return this.position.Y; } set { this.position.Y = value; } }

        public float AppearTime { get; set; }
        public float NormalTime { get; set; }
        public float ExitTime { get; set; }
        public float TimeCounter { get; set; }
        public float TimeLife { get; set; }

        public SpriteBatch SpriteBatch { get; set; }
        #endregion

        #region Initilize
        public EffectCharacter(Game game, SpriteFont font, char character, Vector2 position, float appearTime, float normalTime, float exitTime) 
            : base(game)
        {
            Font = font;
            Character = character;
            AppearTime = appearTime;
            NormalTime = normalTime;
            ExitTime = exitTime;
            this.position = position;
            this.Initialize();
        }
        public EffectCharacter(Game game, SpriteFont font, Texture2D texTure, Vector2 position, float appearTime, float normalTime, float exitTime)
            : base(game)
        {
            Font = font;
            texture = texTure;
            AppearTime = appearTime;
            NormalTime = normalTime;
            ExitTime = exitTime;
            this.position = position;
            this.Initialize();
        }

        public EffectCharacter(Game game, SpriteFont font, char character)
            : base(game)
        {
            Font = font;
            Character = character;
            AppearTime = 300f;
            NormalTime = 400f;
            ExitTime = 200f;
            this.Initialize();
        }
        public EffectCharacter(Game game, SpriteFont font, char character,float timeLife)
            : base(game)
        {
            Font = font;
            Character = character;
            AppearTime = timeLife/3;
            NormalTime = timeLife/3;
            ExitTime = timeLife/3;
            TimeLife = timeLife;
            this.Initialize();
        }
        public EffectCharacter(EffectCharacter effectCharater)
            : base(effectCharater.Game)
        {
            Font = effectCharater.Font;
            Character = effectCharater.Character;
            AppearTime = effectCharater.AppearTime;
            NormalTime = effectCharater.NormalTime;
            ExitTime = effectCharater.ExitTime;
        }
        public EffectCharacter(Game game)
            : base(game)
        {
            Font = null;
            Character = char.MinValue;
            AppearTime = 300f;
            NormalTime = 400f;
            ExitTime = 200f;
            this.Initialize();
        }
        #endregion 

        #region Method
        public new virtual void Initialize()
        {
            TimeCounter = 0f;
            TimeLife = AppearTime + NormalTime + ExitTime;
            this.Enabled = false;
            this.SpriteBatch = (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
            base.Initialize();
        } 

        public  override void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                TimeCounter += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                if (0 <= TimeCounter && TimeCounter <= AppearTime)
                {
                    this.Appearing();
                }
                else if (AppearTime < TimeCounter && TimeCounter <= AppearTime + NormalTime)
                {
                    this.Normaling();
                }
                else if (AppearTime + NormalTime < TimeCounter && TimeCounter <= TimeLife)
                {
                    this.Exiting();
                }
                else
                {
                    this.Enabled = false;
                    this.TimeCounter = 0f;
                }
            }
            base.Update(gameTime);
        }

        public void Begin()
        {
            this.Enabled = true;
        }

        public abstract void Appearing();
        public virtual void Normaling()  { }
        public abstract void Exiting();

        #endregion 

    }

    public abstract class TextEffect : DrawableGameComponent
    {
        #region Attributes
        public List<EffectCharacter> CharList { get; set; }
        public SpriteFont Font { get; set; }
        string original;

        protected Vector2 position;
        public Vector2 Position { get { return this.position; } set { this.position = value; this.UpdatePosition(); } }
        public float PositionX { get { return this.position.X; } set { this.position.X = value; this.UpdatePosition(); } }
        public float PositionY { get { return this.position.Y; } set { this.position.Y = value; this.UpdatePosition(); } }

        public bool IsDie
        {
            get { return ((this.currentIndex >= this.original.Length) && !this.CharList[this.original.Length - 1].Enabled);}
        }

        public float TimePerCharacter { get; set; }
        public float TimeCounter;
        int currentIndex = 0;
        #endregion

        #region Initialize
        public TextEffect(Game game, string text, SpriteFont font, Vector2 position, float tpc)
            :base(game)
        {
            this.original = text;
            this.TimePerCharacter = tpc;
            this.position = position;
            this.Font = font;
            this.Initialize();
        }
        #endregion

        #region Method
        public new virtual void Initialize()
        {
            TimeCounter = 0f;
            Vector2 pos = this.position;

            this.CharList = new List<EffectCharacter>();
            for (int i = 0; i < this.original.Length; ++i)
            {
                this.AddCharacter(this.original[i], pos);
                pos.X += this.Font.MeasureString(this.original[i].ToString()).X;
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.TimeCounter += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if ((this.currentIndex < this.CharList.Count) && (this.TimeCounter >= this.TimePerCharacter))
            {
                this.CharList[currentIndex++].Begin();
                this.TimeCounter = 0;
            }

            foreach (EffectCharacter ec in this.CharList)
            {
                ec.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (EffectCharacter ec in this.CharList)
            {
                ec.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        public virtual void UpdatePosition()
        {
            Vector2 pos = this.position;
            for (int i = 0; i < this.CharList.Count; ++i)
            {
                this.CharList[i].Position = pos;
                pos.X += this.Font.MeasureString(this.original[i].ToString()).X;
            }
        }

        public abstract void AddCharacter(char c, Vector2 position);
        #endregion
    }
}
