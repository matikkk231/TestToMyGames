using System;
using System.Collections.Generic;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public class LevelModel : ILevelModel
    {
        public Action<IZombieModel> ZombieRemoved { get; set; }
        public Action<IZombieModel> ZombieSpawned { get; set; }
        public Action<RoundConfig> RoundStarted { get; set; }

        private readonly List<IZombieModel> _activeZombies = new List<IZombieModel>();
        private readonly List<IZombieModel> _cachedZombies = new List<IZombieModel>();

        private readonly List<RoundConfig> _roundConfigs = new List<RoundConfig>();
        private int _currentRound;
        public IPlayerModel Player { get; private set; } = new PlayerModel();

        public LevelModel(List<RoundConfig> roundConfigs)
        {
            _roundConfigs = roundConfigs;
        }

        public IZombieModel GetNewZombie()
        {
            if (_cachedZombies.Count != 0)
            {
                var zombie = _cachedZombies[0];
                _cachedZombies.Remove(zombie);
                _activeZombies.Add(zombie);
                zombie.Removed += OnZombieRemoved;
                return zombie;
            }
            else
            {
                var zombie = new ZombieModel();
                _activeZombies.Add(zombie);
                zombie.Removed += OnZombieRemoved;
                return zombie;
            }
        }

        public bool TryZombieSpawn()
        {
            var isSpawnAllowed = _activeZombies.Count < _roundConfigs[_currentRound].MaxZombiesInGame;
            if (isSpawnAllowed)
            {
                var zombieConfig = _roundConfigs[_currentRound].ZombieConfigs[0];
                _roundConfigs[_currentRound].ZombieConfigs.Remove(zombieConfig);
                ZombieSpawned?.Invoke(GetNewZombie());
            }

            return isSpawnAllowed;
        }

        private void OnZombieRemoved(IZombieModel zombieModel)
        {
            ZombieRemoved?.Invoke(zombieModel);
            _activeZombies.Remove(zombieModel);
            _cachedZombies.Add(zombieModel);
        }

        public void StartLevel()
        {
            Player = new PlayerModel();
            RoundStarted?.Invoke(_roundConfigs[_currentRound]);
        }
    }
}