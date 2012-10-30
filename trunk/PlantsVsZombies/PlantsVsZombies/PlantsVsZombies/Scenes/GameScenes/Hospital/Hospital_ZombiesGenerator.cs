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
using GameBaseXNA;
using PlantsVsZombies.Zombies.Managers.Waves;
using PlantsVsZombies.Zombies.Managers;
using PlantsVsZombies.Zombies;

namespace PlantsVsZombies.Scenes.GameScenes.Hospital
{
    public class Hospital_ZombiesGenerator : ZombieGenerator
    {
        #region Constructors
        public Hospital_ZombiesGenerator(ZombiesManager manager)
            : base(manager)
        {
            this.Waves.Add(new IdleWave(manager, 15000000));
            this.Waves.Add(new Wave1(manager));
            this.Waves.Add(new Wave2(manager));
            this.Waves.Add(new Wave3(manager));
            this.Waves.Add(new Wave4(manager));
            this.Waves.Add(new Wave5(manager));
        }
        #endregion
    }

    public class Wave1 : LinearWave
    {
        #region Contructors
        public Wave1(ZombiesManager manager)
            : base(manager, 1, 0, 12000)
        {

        }
        #endregion

        #region Methods
        public override Zombie Generate()
        {
            Zombie zombie = new Zombies.Spiders.GiantSpider(this.Game);
            this.ZombiesManager.Add(zombie, GRandom.RandomInt(0, this.ZombiesManager.Grid.NumberOfRows));

            return zombie;
        }
        #endregion
    }

    public class Wave2 : LinearWave
    {
        #region Contructors
        public Wave2(ZombiesManager manager)
            : base(manager, 2, 3000, 6000)
        {

        }
        #endregion

        #region Methods
        public override Zombie Generate()
        {
            Zombie zombie = new Zombies.Spiders.GiantSpider(this.Game);
            this.ZombiesManager.Add(zombie, GRandom.RandomInt(0, this.ZombiesManager.Grid.NumberOfRows));

            return zombie;
        }
        #endregion
    }

    public class Wave3 : LinearWave
    {
        #region Contructors
        public Wave3(ZombiesManager manager)
            : base(manager, 6, 1000, 6000)
        {

        }
        #endregion

        #region Methods
        public override Zombie Generate()
        {
            Zombie zombie = null;
            switch (GRandom.RandomInt(2))
            {
                case 0:
                    zombie = new Zombies.Spiders.GiantSpider(this.Game);
                    break;
                case 1:
                    zombie = new Zombies.Skeletons.BarrowWight(this.Game);
                    break;
            };

            this.ZombiesManager.Add(zombie, GRandom.RandomInt(0, this.ZombiesManager.Grid.NumberOfRows));

            return zombie;
        }
        #endregion
    }

    public class Wave4 : LinearWave
    {
        #region Contructors
        public Wave4(ZombiesManager manager)
            : base(manager, 15, 500, 10000)
        {

        }
        #endregion

        #region Methods
        public override Zombie Generate()
        {
            Zombie zombie = null;
            switch (GRandom.RandomInt(2))
            {
                case 0:
                    zombie = new Zombies.Spiders.GiantSpider(this.Game);
                    break;
                case 1:
                    zombie = new Zombies.Skeletons.BarrowWight(this.Game);
                    break;
            };

            this.ZombiesManager.Add(zombie, GRandom.RandomInt(0, this.ZombiesManager.Grid.NumberOfRows));

            return zombie;
        }
        #endregion
    }

    public class Wave5 : SpeedUpWave
    {
        #region Contructors
        public Wave5(ZombiesManager manager)
            : base(manager, 20, 800, 30, 16000)
        {

        }
        #endregion

        #region Methods
        public override Zombie Generate()
        {
            Zombie zombie = null;
            switch (GRandom.RandomInt(2))
            {
                case 0:
                    zombie = new Zombies.Spiders.GiantSpider(this.Game);
                    break;
                case 1:
                    zombie = new Zombies.Skeletons.BarrowWight(this.Game);
                    break;
                case 2:
                    zombie = new Zombies.Zombies.Nupperibo(this.Game);
                    break;
            };

            this.ZombiesManager.Add(zombie, GRandom.RandomInt(0, this.ZombiesManager.Grid.NumberOfRows));

            return zombie;
        }
        #endregion
    }
}
