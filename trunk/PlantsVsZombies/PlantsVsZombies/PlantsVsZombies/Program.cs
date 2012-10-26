using System;

namespace PlantsVsZombies
{
    using PvZGame = LuongPvZ;
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PvZGame game = new PvZGame())
            {
                game.Run();
            }
        }
    }
#endif
}

