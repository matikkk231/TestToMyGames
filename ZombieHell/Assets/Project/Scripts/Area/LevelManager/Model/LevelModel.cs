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
        public Action LevelWon { get; set; }
        public Action LevelLoosed { get; set; }

        private readonly List<IZombieModel> _activeZombies = new List<IZombieModel>();
        private readonly List<IZombieModel> _cachedZombies = new List<IZombieModel>();

        private readonly List<RoundConfig> _roundConfigs = new List<RoundConfig>();
        private int _currentRound;
        private int _currentZombie;
        public IPlayerModel Player { get; private set; }

        public LevelModel(List<RoundConfig> roundConfigs)
        {
            _roundConfigs = roundConfigs;
            Player = new PlayerModel();
            Player.Died += OnPlayerDied;
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
                if (_currentZombie == _roundConfigs[_currentRound].ZombieConfigs.Count)
                {
                    StartNextRound();
                    return false;
                }

                _currentZombie++;
                ZombieSpawned?.Invoke(GetNewZombie());
            }

            return isSpawnAllowed;
        }

        private void StartNextRound()
        {
            if (_currentRound + 1 >= _roundConfigs.Count)
            {
                LevelWon?.Invoke();
                return;
            }

            _currentRound++;
            RoundStarted?.Invoke(_roundConfigs[_currentRound]);
        }

        private void OnPlayerDied()
        {
            LevelLoosed?.Invoke();
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