using System;
using System.Collections.Generic;
using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Area.LevelManager.View
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        public Action PreparedSpawnZombie { get; set; }

        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private GameObject _floor;
        private readonly List<IZombieView> _cachedZombies = new List<IZombieView>();
        private readonly List<IZombieView> _activeZombies = new List<IZombieView>();
        private IPlayerView _playerView;

        private float _timeTillNextSpawn;
        private float _timeBetweenZombieSpawn;
        private bool _isZombieSpawningStarted;

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

        private void Update()
        {
            if (!_isZombieSpawningStarted)
            {
                return;
            }

            _timeTillNextSpawn -= Time.deltaTime;
            if (_timeTillNextSpawn < 0)
            {
                PreparedSpawnZombie?.Invoke();
            }
        }

        public IPlayerView CreatePlayer()
        {
            var playerView = Instantiate(_playerPrefab).GetComponent<IPlayerView>();
            _playerView = playerView;
            return playerView;
        }


        public IZombieView GetNewZombie()
        {
            var random = Random.Range(10, 30);
            if (_cachedZombies.Count != 0)
            {
                var zombie = _cachedZombies[0];
                _cachedZombies.Remove(zombie);
                _activeZombies.Add(zombie);
                zombie.Position = new Vector3(0, 0, random);
                zombie.TargetToChase = _playerView.Transform;
                zombie.Removed += OnZombieRemoved;
                zombie.SetActive(true);
                _timeTillNextSpawn = _timeBetweenZombieSpawn;
                return zombie;
            }

            var zombieObject = Instantiate(_zombiePrefab);
            zombieObject.transform.position = new Vector3(0, 0, random);
            var zombieView = zombieObject.GetComponent<IZombieView>();
            zombieView.TargetToChase = _playerView.Transform;
            _activeZombies.Add(zombieView);
            zombieView.Removed += OnZombieRemoved;
            zombieView.SetActive(true);
            _timeTillNextSpawn = _timeBetweenZombieSpawn;
            return zombieView;
        }

        public void StartZombieSpawning(RoundConfig roundConfig)
        {
            _timeBetweenZombieSpawn = roundConfig.TimeBetweenZombieSpawn;
            _timeTillNextSpawn = _timeBetweenZombieSpawn;
            _isZombieSpawningStarted = true;
        }

        private void OnZombieRemoved(IZombieView zombie)
        {
            zombie.SetActive(false);
            zombie.Removed -= OnZombieRemoved;
            _activeZombies.Remove(zombie);
            _cachedZombies.Add(zombie);
        }
    }
}