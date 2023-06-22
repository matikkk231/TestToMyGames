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
            var random = Random.Range(10, 30);
            if (_cachedZombies.Count != 0)
            {
                var zombie = _cachedZombies[0];
                _cachedZombies.Remove(zombie);
                _activeZombies.Add(zombie);
                zombie.Position = new Vector3(0, 0, random);
                zombie.TargetToChase = TargetToChase;
                zombie.Removed += OnZombieRemoved;
                zombie.SetActive(true);
                _timeTillNextSpawn = _timeBetweenZombieSpawn;
                return zombie;
            }

            var zombieObject = Instantiate(_zombiePrefab);
            zombieObject.transform.position = new Vector3(0, 0, random);
            var zombieView = zombieObject.GetComponent<IZombieView>();
            zombieView.TargetToChase = TargetToChase;
            _activeZombies.Add(zombieView);
            zombieView.Removed += OnZombieRemoved;
            zombieView.SetActive(true);
            _timeTillNextSpawn = _timeBetweenZombieSpawn;
            return zombieView;
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