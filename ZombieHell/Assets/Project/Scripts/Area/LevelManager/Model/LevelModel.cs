using System;
using System.Collections.Generic;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.ZombieSpawner.Model;

namespace Project.Scripts.Area.LevelManager.Model
{
    public class LevelModel : ILevelModel
    {
        public Action LevelWon { get; set; }
        public Action LevelLoosed { get; set; }

        private readonly IZombieSpawnerModel _zombieSpawner;
        private readonly List<RoundConfig> _roundConfigs = new List<RoundConfig>();
        private int _currentRound;
        public IPlayerModel Player { get; private set; }
        public IZombieSpawnerModel ZombieSpawner => _zombieSpawner;

        public LevelModel(List<RoundConfig> roundConfigs)
        {
            _roundConfigs = roundConfigs;
            Player = new PlayerModel();
            _zombieSpawner = new ZombieSpawnerModel();
            Player.Died += OnPlayerDied;
            _zombieSpawner.ZombieExpired += OnZombiesExpired;
        }

        private void StartNextRound()
        {
            _currentRound++;
            _zombieSpawner.StartZombieSpawning(_roundConfigs[_currentRound]);
        }

        private void OnPlayerDied()
        {
            LevelLoosed?.Invoke();
        }

        private void OnZombiesExpired()
        {
            var isRoundsExpired = _currentRound + 1 >= _roundConfigs.Count;
            if (isRoundsExpired)
            {
                LevelWon?.Invoke();
                return;
            }

            StartNextRound();
        }

        public void StartLevel()
        {
            Player = new PlayerModel();
            _zombieSpawner.StartZombieSpawning(_roundConfigs[_currentRound]);
        }
    }
}