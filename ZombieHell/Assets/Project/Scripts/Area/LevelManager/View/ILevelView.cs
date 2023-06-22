using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.ZombieSpawner.View;
using Project.Scripts.Base.AdoioService.View;

namespace Project.Scripts.Area.LevelManager.View
{
    public interface ILevelView
    {
        public IZombieSpawnerView ZombieSpawner { get; }
        public IPlayerView GetPlayer();
        public void AddAudioService(IAudioServiceView audioServiceView);
    }
}