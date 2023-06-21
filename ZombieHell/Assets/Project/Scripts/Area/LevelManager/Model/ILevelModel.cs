using Project.Scripts.Area.Player.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public interface ILevelModel
    {
        IPlayerModel Player { get; }

        void StartLevel();
    }
}