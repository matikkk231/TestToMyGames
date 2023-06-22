using System;
using System.Collections.Generic;
using Project.Scripts.Area.Round;
using Project.Scripts.Area.Zombie.Model;
using Project.Scripts.Area.Zombie.Presenter;
using Project.Scripts.Area.ZombieSpawner.Model;
using Project.Scripts.Area.ZombieSpawner.View;

namespace Project.Scripts.Area.ZombieSpawner.Presenter
{
    public class ZombieSpawnerPresenter : IDisposable
    {
        private readonly IZombieSpawnerModel _model;
        private readonly IZombieSpawnerView _view;

        private readonly List<ZombiePresenter> _zombiePresenters;

        public ZombieSpawnerPresenter(IZombieSpawnerView view, IZombieSpawnerModel model)
        {
            _zombiePresenters = new List<ZombiePresenter>();
            _view = view;
            _model = model;
            AddListeners();
        }

        private void AddListeners()
        {
            _view.PreparedSpawnZombie += OnPreparedZombieSpawn;
            _model.ZombieSpawned += OnZombieSpawned;
            _model.ZombieSpawningStarted += OnZombieSpawningStarted;
            _model.ZombieRemoved += OnZombieRemoved;
        }

        private void RemoveListeners()
        {
            _view.PreparedSpawnZombie -= OnPreparedZombieSpawn;
            _model.ZombieSpawned -= OnZombieSpawned;
            _model.ZombieSpawningStarted -= OnZombieSpawningStarted;
            _model.ZombieRemoved -= OnZombieRemoved;
        }

        private void OnPreparedZombieSpawn()
        {
            _model.TryZombieSpawn();
        }

        private void OnZombieRemoved(IZombieModel model)
        {
            foreach (var zombiePresenter in _zombiePresenters)
            {
                if (zombiePresenter.Model == model)
                {
                    zombiePresenter.Dispose();
                    _zombiePresenters.Remove(zombiePresenter);
                    return;
                }
            }
        }

        private void OnZombieSpawned(IZombieModel model)
        {
            _zombiePresenters.Add(new ZombiePresenter(_view.GetNewZombie(), model));
        }

        private void OnZombieSpawningStarted(RoundConfig roundConfig)
        {
            _view.StartZombieSpawning(roundConfig);
        }

        public void Dispose()
        {
            RemoveListeners();
        }
    }
}