using System;
using System.Collections.Generic;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie;
using Project.Scripts.Area.Zombie.Model;

namespace Project.Scripts.Area.ZombieSpawner.Model
{
    public class ZombieSpawnerModel : IZombieSpawnerModel
    {
        public Action ZombieExpired { get; set; }
        public Action<RoundConfig> ZombieSpawningStarted { get; set; }
        public Action<IZombieModel> ZombieRemoved { get; set; }
        public Action<IZombieModel> ZombieSpawned { get; set; }
        public Action<int> RemainedZombieChanged { get; set; }

        private RoundConfig _roundConfig;
        private int _currentZombieIndex;
        private int _zombiesKilled;
        private readonly List<IZombieModel> _activeZombies = new List<IZombieModel>();
        private readonly List<IZombieModel> _cachedZombies = new List<IZombieModel>();


        private IZombieModel GetNewZombie(ZombieConfig config)
        {
            if (_cachedZombies.Count != 0)
            {
                var zombie = _cachedZombies[0];
                zombie.Health = config.Health;
                _cachedZombies.Remove(zombie);
                InitializeZombie(zombie, config);
                return zombie;
            }
            else
            {
                var zombie = new ZombieModel();
                zombie.Health = config.Health;
                InitializeZombie(zombie, config);
                return zombie;
            }
        }

        private void InitializeZombie(IZombieModel zombie, ZombieConfig config)
        {
            _activeZombies.Add(zombie);
            zombie.Removed += OnZombieRemoved;
            zombie.Health = config.Health;
            zombie.DamageAmount = config.DamageAmount;
        }

        public void StartZombieSpawning(RoundConfig roundConfig)
        {
            _roundConfig = roundConfig;
            _zombiesKilled = 0;
            _currentZombieIndex = 0;
            RemainedZombieChanged?.Invoke(roundConfig.ZombieConfigs.Count);
            ZombieSpawningStarted?.Invoke(_roundConfig);
        }

        public bool TryZombieSpawn()
        {
            var isSpawnAllowed = _activeZombies.Count < _roundConfig.MaxZombiesInGame;
            if (isSpawnAllowed)
            {
                var isZombiesExpired = _currentZombieIndex >= _roundConfig.ZombieConfigs.Count;
                if (isZombiesExpired)
                {
                    if (_activeZombies.Count == 0)
                    {
                        ZombieExpired?.Invoke();
                    }

                    return false;
                }

                ZombieSpawned?.Invoke(GetNewZombie(_roundConfig.ZombieConfigs[_currentZombieIndex]));
                _currentZombieIndex++;
            }

            return isSpawnAllowed;
        }

        private void OnZombieRemoved(IZombieModel zombieModel)
        {
            ZombieRemoved?.Invoke(zombieModel);
            zombieModel.Removed -= OnZombieRemoved;
            _zombiesKilled++;

            RemainedZombieChanged?.Invoke(_roundConfig.ZombieConfigs.Count - _zombiesKilled);
            _activeZombies.Remove(zombieModel);
            _cachedZombies.Add(zombieModel);
        }
    }
}