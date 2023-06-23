using System;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.Model;

namespace Project.Scripts.Area.ZombieSpawner.Model
{
    public interface IZombieSpawnerModel
    {
        Action ZombieExpired { get; set; }
        Action<RoundConfig> ZombieSpawningStarted { get; set; }
        Action<IZombieModel> ZombieRemoved { get; set; }
        Action<IZombieModel> ZombieSpawned { get; set; }
        Action<int> RemainedZombieChanged { get; set; }

        void StartZombieSpawning(RoundConfig roundConfig);
        bool TryZombieSpawn();
    }
}