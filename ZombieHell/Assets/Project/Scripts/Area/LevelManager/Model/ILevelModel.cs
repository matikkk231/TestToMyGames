using System;
using Project.Scripts.Area.Counter.Model;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.ZombieSpawner.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public interface ILevelModel
    {
        Action LevelWon { get; set; }
        Action LevelLoosed { get; set; }
        IPlayerModel Player { get; }
        ICounterModel Counter { get; }
        IZombieSpawnerModel ZombieSpawner { get; }
    }
}