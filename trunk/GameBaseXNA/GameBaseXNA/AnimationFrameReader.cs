using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameBaseXNA
{
    struct AnimationFrameData
    {
        public int Width;
        public int Height;
        public int NumberOfFrames;
    }

    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    public class AnimationFrameReader : ContentTypeReader<AnimationFrameData>
    {
        protected override AnimationFrameData Read(ContentReader input, AnimationFrameData existingInstance)
        {
            // TODO: read a value from the input ContentReader.
            AnimationFrameData result;
            result.Width = input.ReadInt32();
            result.Height = input.ReadInt32();
            result.NumberOfFrames = input.ReadInt32();
        }
    }
}
