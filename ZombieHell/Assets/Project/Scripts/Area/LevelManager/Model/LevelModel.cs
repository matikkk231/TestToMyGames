using System;
using System.Collections.Generic;
using Project.Scripts.Area.Counter.Model;
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
        public ICounterModel Counter { get; }
        public IZombieSpawnerModel ZombieSpawner => _zombieSpawner;

        public LevelModel(List<RoundConfig> roundConfigs)
        {
            _roundConfigs = roundConfigs;
            Player = new PlayerModel();
            _zombieSpawner = new ZombieSpawnerModel();
            _zombieSpawner.RemainedZombieChanged += OnRemainedZombiesChanged;
            _zombieSpawner.ZombieExpired += OnZombiesExpired;
            Counter = new CounterModel();

            Player.Died += OnPlayerDied;
        }

        private void StartNextRound()
        {
            _currentRound++;
            Counter.CurrentRound = _currentRound;
            _zombieSpawner.StartZombieSpawning(_roundConfigs[_currentRound]);
        }

        private void OnPlayerDied()
        {
            Player.Died -= OnPlayerDied;
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

            Counter.RoundChanged?.Invoke(_currentRound);
            StartNextRound();
        }

        private void OnRemainedZombiesChanged(int amount)
        {
            Counter.RemainedZombies = amount;
        }

        public void StartLevel()
        {
            Player = new PlayerModel();
            Counter.CurrentRound = _currentRound;
            _zombieSpawner.StartZombieSpawning(_roundConfigs[_currentRound]);
        }
    }
}