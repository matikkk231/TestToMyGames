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
        [SerializeField] private Vector3 _maxPlayerPosition;
        [SerializeField] private Vector3 _minPlayerPosition;
        private IPlayerView _playerView;
        private IAudioServiceView _audioServiceView;

        public IZombieSpawnerView ZombieSpawner => _zombieSpawner;
        public ICounterView Counter => _counter;

        private void Awake()
        {
            BuildFloors();
        }

        private void BuildFloors()
        {
            Vector3Int minMapPosition = new Vector3Int(-50, 0, -50);
            Vector3Int maxMapPosition = new Vector3Int(50, 0, 50);
            int distanceBetweenFloors = 10;
            for (int x = minMapPosition.x; x < maxMapPosition.x; x += distanceBetweenFloors)
            {
                for (int z = minMapPosition.z; z < maxMapPosition.z; z += distanceBetweenFloors)
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
            _playerView.SetMovingArea(_minPlayerPosition, _maxPlayerPosition);

            return _playerView;
        }

        public void AddAudioService(IAudioServiceView audioServiceView)
        {
            _audioServiceView = audioServiceView;
        }
    }
}