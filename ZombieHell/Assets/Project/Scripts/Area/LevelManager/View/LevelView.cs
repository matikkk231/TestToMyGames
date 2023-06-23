using Project.Scripts.Area.Counter.View;
using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.ZombieSpawner.View;
using Project.Scripts.Base.AdoioService.View;
using UnityEngine;

namespace Project.Scripts.Area.LevelManager.View
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private ZombieSpawnerView _zombieSpawner;
        [SerializeField] private GameObject _floor;
        [SerializeField] private CounterView _counter;
        private IPlayerView _playerView;
        private IAudioServiceView _audioServiceView;

        public IZombieSpawnerView ZombieSpawner => _zombieSpawner;
        public ICounterView Counter => _counter;

        private void Awake()
        {
            for (int x = -30; x < 30; x += 10)
            {
                for (int z = -30; z < 30; z += 10)
                {
                    Instantiate(_floor).transform.position = new Vector3(x, 0, z);
                }
            }
        }

        public IPlayerView GetPlayer()
        {
            if (_playerView == null)
            {
                var playerView = Instantiate(_playerPrefab).GetComponent<IPlayerView>();
                _playerView = playerView;
            }

            _zombieSpawner.TargetToChase = _playerView.Transform;
            _playerView.AddAudioService(_audioServiceView);

            return _playerView;
        }

        public void AddAudioService(IAudioServiceView audioServiceView)
        {
            _audioServiceView = audioServiceView;
        }
    }
}