using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.ZombieSpawner.View;

namespace Project.Scripts.Area.LevelManager.View
{
    public interface ILevelView
    {
        public IZombieSpawnerView ZombieSpawner { get; }
        public IPlayerView GetPlayer();
    }
}