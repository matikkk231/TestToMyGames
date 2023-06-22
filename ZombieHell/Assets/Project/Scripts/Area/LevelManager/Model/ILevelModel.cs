using System;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public interface ILevelModel
    {
        Action<IZombieModel> ZombieRemoved { get; set; }
        Action<IZombieModel> ZombieSpawned { get; set; }
        Action<RoundConfig> RoundStarted { get; set; }
        Action LevelWon { get; set; }
        Action LevelLoosed { get; set; }
        IPlayerModel Player { get; }

        IZombieModel GetNewZombie();
        bool TryZombieSpawn();
        void StartLevel();
    }
}