using Project.Scripts.Area.Player.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public class LevelModel : ILevelModel
    {
        public IPlayerModel Player { get; private set; }

        public void StartLevel()
        {
            Player = new PlayerModel();
        }
    }
}