using System;
using System.Collections.Generic;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Area.ZombieSpawner.View
{
    public class ZombieSpawnerView : MonoBehaviour, IZombieSpawnerView
    {
        public Action PreparedSpawnZombie { get; set; }

        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private float _minDistanceFromCenter;
        [SerializeField] private float _maxDistanceFromCenter;
        [SerializeField] private float _maxOffsetFromCenter;
        private readonly List<IZombieView> _cachedZombies = new List<IZombieView>();
        private readonly List<IZombieView> _activeZombies = new List<IZombieView>();

        private float _timeTillNextSpawn;
        private float _timeBetweenZombieSpawn;
        private bool _isZombieSpawningStarted;

        public Transform TargetToChase { get; set; }

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


        public IZombieView GetNewZombie()
        {
            if (_cachedZombies.Count != 0)
            {
                var zombie = _cachedZombies[0];
                _cachedZombies.Remove(zombie);
                _activeZombies.Add(zombie);
                InitializeZombie(zombie);
                _timeTillNextSpawn = _timeBetweenZombieSpawn;
                return zombie;
            }

            var zombieObject = Instantiate(_zombiePrefab);
            var zombieView = zombieObject.GetComponent<IZombieView>();
            _activeZombies.Add(zombieView);
            InitializeZombie(zombieView);
            _timeTillNextSpawn = _timeBetweenZombieSpawn;
            return zombieView;
        }

        private void InitializeZombie(IZombieView zombie)
        {
            var sideToSpawn = Random.Range(1, 5);
            var distanceFromCenter = Random.Range(_minDistanceFromCenter, _maxDistanceFromCenter);
            var centerOffset = Random.Range(-_maxOffsetFromCenter, _maxOffsetFromCenter);

            switch (sideToSpawn)
            {
                case 1:
                    zombie.Position = new Vector3(centerOffset, 0, distanceFromCenter);
                    break;
                case 2:
                    zombie.Position = new Vector3(-distanceFromCenter, 0, centerOffset);
                    break;
                case 3:
                    zombie.Position = new Vector3(centerOffset, 0, -distanceFromCenter);
                    break;
                case 4:
                    zombie.Position = new Vector3(distanceFromCenter, 0, centerOffset);
                    break;
                default: throw new Exception("there is no side less 1 or bigger 4");
            }

            zombie.TargetToChase = TargetToChase;
            zombie.Removed += OnZombieRemoved;
            zombie.SetActive(true);
        }

        private void OnZombieRemoved(IZombieView zombie)
        {
            zombie.SetActive(false);
            zombie.Removed -= OnZombieRemoved;
            _activeZombies.Remove(zombie);
            _cachedZombies.Add(zombie);
        }

        public void StartZombieSpawning(RoundConfig roundConfig)
        {
            _timeBetweenZombieSpawn = roundConfig.TimeBetweenZombieSpawn;
            _timeTillNextSpawn = _timeBetweenZombieSpawn;
            _isZombieSpawningStarted = true;
        }
    }
}