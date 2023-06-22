using System;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.View;
using UnityEngine;

namespace Project.Scripts.Area.ZombieSpawner.View
{
    public interface IZombieSpawnerView
    {
        public Action PreparedSpawnZombie { get; set; }
        public Transform TargetToChase { get; set; }

        public IZombieView GetNewZombie();
        public void StartZombieSpawning(RoundConfig roundConfig);
    }
}