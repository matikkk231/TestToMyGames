using System;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Zombie.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public interface ILevelModel
    {
        Action<IZombieModel> ZombieRemoved { get; set; }
        IPlayerModel Player { get; }

        IZombieModel GetNewZombie();
        void StartLevel();
    }
}