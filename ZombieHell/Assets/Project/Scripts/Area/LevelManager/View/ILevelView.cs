using System;
using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.View;

namespace Project.Scripts.Area.LevelManager.View
{
    public interface ILevelView
    {
        public Action PreparedSpawnZombie { get; set; }
        public IPlayerView CreatePlayer();
        public IZombieView GetNewZombie();
        public void StartZombieSpawning(RoundConfig roundConfig);
    }
}