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
    public class Sound : GameComponent
    {
        protected SoundEffect SoundEngine { get; set; }
        public List<SoundEffectInstance> SoundInstance { get; set; }

        protected int MaxInstance { get; set; }
        
        /// <summary>
        /// Create new Sound engine
        /// </summary>
        /// <param name="game">Current game</param>
        /// <param name="filename">File name</param>
        public Sound(Game game, string filename) : base(game)
        {
            this.LoadSound(filename);
            this.SoundInstance = new List<SoundEffectInstance>();
            this.MaxInstance = int.MaxValue;
            this.Initialize();
        }

        public new virtual void Initialize()
        {
        }

        public virtual void LoadSound(string filename)
        {
            SoundEngine = this.Game.Content.Load<SoundEffect>(filename);
        }
        
        /// <summary>
        /// Create new sound instances to play
        /// <param name="isloop">Set instance is looped or not</param>
        /// <returns>True if number of instant is less than MAX (then this sound will be played)</returns>
        /// </summary>
        public bool Play(bool isloop)
        {
            if (this.SoundInstance.Count > this.MaxInstance)
                return false;
            SoundEffectInstance instance = SoundEngine.CreateInstance();
            SoundInstance.Add(instance);
            instance.IsLooped = isloop;
            instance.Play();

            return true;
        }

        /// <summary>
        /// Create new sound instances to play
        /// <returns>True if number of instant is less than MAX (then this sound will be played)</returns>
        /// </summary>
        public bool Play()
        {
            if (this.SoundInstance.Count > this.MaxInstance)
                return false;
            SoundEffectInstance instance = SoundEngine.CreateInstance();
            SoundInstance.Add(instance);
            instance.IsLooped = false;
            instance.Play();

            return true;
        }

        /// <summary>
        /// Pause all instances of sound
        /// </summary>
        public void PauseAll()
        {
            foreach (SoundEffectInstance inst in this.SoundInstance)
            {
                inst.Pause();
            }
        }
        
        /// <summary>
        /// Resume all instances of sound
        /// </summary>
        public void ResumeAll()
        {
            foreach (SoundEffectInstance inst in this.SoundInstance)
            {
                if (inst.State == SoundState.Paused)
                    inst.Resume();
            }
        }
        
        /// <summary>
        /// Stop all instances of sound
        /// </summary>
        public void StopAll()
        {
            foreach (SoundEffectInstance inst in this.SoundInstance)
            {
                if (inst.State != SoundState.Stopped)
                    inst.Stop();
            }

            this.SoundInstance.Clear();
        }

        /// <summary>
        /// Increase or decrease volume of all instances of sound
        /// </summary>
        /// <param name="vol">Number volume to increased</param>
        public void InscreaseVolume(float vol)
        {
            for (int i = 0; i < this.SoundInstance.Count; ++i)
            {
                if (this.SoundInstance[i].Volume + vol >= 0.0f && this.SoundInstance[i].Volume + vol <= 1.0f)
                    this.SoundInstance[i].Volume += vol;
                else if (this.SoundInstance[i].Volume + vol < 0.0f)
                    this.SoundInstance[i].Volume = 0.0f;
                else if (this.SoundInstance[i].Volume + vol > 1.0f)
                    this.SoundInstance[i].Volume = 1.0f;
                    
            }
                
        }
        
        /// <summary>
        /// Allow update itself
        /// </summary>
        /// <param name="gameTime">Current time</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < this.SoundInstance.Count; )
            {
                if (this.SoundInstance[i].State == SoundState.Stopped)
                    this.SoundInstance.RemoveAt(i);
                else
                    ++i;
            }
        }

        public int NumberOfInstances
        {
            get { return this.SoundInstance.Count; }
        }
    }
}