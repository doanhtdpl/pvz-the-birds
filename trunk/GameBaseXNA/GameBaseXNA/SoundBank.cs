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
    public static class GSound
    {
        static Dictionary<string, Sound> soundBank = new Dictionary<string, Sound>();
        static Game Game;

        public static void SetGame(Game game)
        {
            Game = game;
        }

        public static Sound GetSound(string name)
        {
            if (!soundBank.ContainsKey(name))
            {
                soundBank[name] = new Sound(Game, name);
            }

            return soundBank[name];
        }

        public static void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, Sound> sounds in soundBank)
            {
                sounds.Value.Update(gameTime);
            }
        }

        public static void StopAll()
        {
            foreach (KeyValuePair<string, Sound> sounds in soundBank)
            {
                sounds.Value.StopAll();
            }
        }
    }
}
