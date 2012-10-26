using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameBaseXNA
{
    public static class SpriteBank
    {
        private class SpriteItem : GameComponent
        {
            public SpriteItem(Game game)
                : base(game) { }

            protected Sprite sprite = null;
            public Sprite Sprite { get { return this.GetItem(); } }

            protected string assetName = string.Empty;
            public string AssetName { get { return this.assetName; } set { this.assetName = value; } }

            public virtual void Load()
            {
                this.sprite = new Sprite(this.Game, this.assetName);
            }

            protected virtual Sprite GetItem()
            {
                if (this.sprite == null)
                    this.Load();

                return new Sprite(this.sprite);
            }
        }

        private class AnimationItem : GameComponent
        {
            public AnimationItem(Game game, int width, int height, int frPerLine, int numOfFrame)
                : base(game)
            {
                this.frameWidth = width;
                this.frameHeight = height;
                this.framePerLine = frPerLine;
                this.numberOfFrames = numOfFrame;
                Frames = new List<Rectangle>();
                Resize();
            }

            public AnimationItem(Game game)
                : base(game)
            {
                this.frameWidth = 0;
                this.frameHeight = 0;
                this.framePerLine = 0;
                this.numberOfFrames = 0;
                Frames = new List<Rectangle>();
                Resize();
            }

            protected Animation sprite = null;
            public Animation Animation { get { return this.GetItem(); } }

            public List<Rectangle> Frames { get; set; }

            int frameWidth, frameHeight, numberOfFrames, framePerLine;
            public int FrameWidth { get { return frameWidth; } set { frameWidth = value; Resize(); } }
            public int FrameHeight { get { return frameHeight; } set { frameHeight = value; Resize(); } }
            public int NumberOfFrames { get { return numberOfFrames; } set { numberOfFrames = value; Resize(); } }
            public int FramePerLine { get { return framePerLine; } set { framePerLine = value; Resize(); } }

            protected string assetName = string.Empty;
            public string AssetName { get { return this.assetName; } set { this.assetName = value; } }

            protected void Resize()
            {
                if (Frames == null)
                    Frames = new List<Rectangle>();
                Frames.Clear();

                Rectangle frame;
                int x = 0, y = 0;
                for (int i = 0; i < numberOfFrames; ++i)
                {
                    frame = new Rectangle(x, y, frameWidth, frameHeight);
                    this.Frames.Add(frame);

                    if ((i + 1) % framePerLine == 0)
                    {
                        y += frameHeight;
                        x = 0;
                    }
                    else
                    {
                        x += frameWidth;
                    }
                }
            }

            public virtual void Load()
            {
                this.sprite = new Animation(this.Game, this.Game.Content.Load<Texture2D>(this.assetName), this.Frames);
            }

            protected virtual Animation GetItem()
            {
                if (this.sprite == null)
                    this.Load();

                return new Animation(this.sprite);
            }
        }

        public static Game Game;
        public static void SetGame(Game game)
        {
            Game = game;
        }

        static SpriteBank()
        {
            Sprites = new Dictionary<string, SpriteItem>();
            Animations = new Dictionary<string, AnimationItem>();
        }

        private static Dictionary<string, SpriteItem> Sprites { get; set; }
        private static Dictionary<string, AnimationItem> Animations { get; set; }

        public static Sprite GetSprite(string name)
        {
            SpriteItem item;
            if (!Sprites.ContainsKey(name))
            {
                item = new SpriteItem(SpriteBank.Game);
                item.AssetName = name;
                Sprites.Add(name, item);
            }
            else
                item = Sprites[name];

            return item.Sprite;
        }

        public static Animation GetAnimation(string name)
        {
            AnimationItem item;
            if (!Animations.ContainsKey(name))
            {
                item = new AnimationItem(SpriteBank.Game);
                item.AssetName = name;
                Animations.Add(name, item);
            }
            else
                item = Animations[name];

            return item.Animation;
        }

        public static Animation GetAnimation(string name, int width, int height, int framePerLine, int numberOfFrame)
        {
            AnimationItem item = Animations[name];
            if (item == null)
            {
                item = new AnimationItem(SpriteBank.Game);
                item.AssetName = name;
                Animations[name] = item;
            }

            item.FrameWidth = width;
            item.FrameHeight = height;
            item.FramePerLine = framePerLine;
            item.NumberOfFrames = numberOfFrame;
            return item.Animation;
        }

        public static void SetAnimationData(string name, int width, int height, int framePerLine, int numberOfFrame)
        {
            AnimationItem item;
            if (!Animations.ContainsKey(name))
            {
                item = new AnimationItem(SpriteBank.Game);
                item.AssetName = name;
                Animations.Add(name, item);
            }
            else
                item = Animations[name];

            item.FrameWidth = width;
            item.FrameHeight = height;
            item.FramePerLine = framePerLine;
            item.NumberOfFrames = numberOfFrame;
        }
    }
}