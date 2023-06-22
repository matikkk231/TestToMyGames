using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.Zombie.View;

namespace Project.Scripts.Area.LevelManager.View
{
    public interface ILevelView
    {
        public IPlayerView CreatePlayer();
        public IZombieView GetNewZombie();
    }
}