using Project.Scripts.Area.Player.View;

namespace Project.Scripts.Area.LevelManager.View
{
    public interface ILevelView
    {
        public IPlayerView CreatePlayer();
    }
}