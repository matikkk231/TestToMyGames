using System;
using System.Collections.Generic;
using Project.Scripts.Area.LevelManager.Model;
using Project.Scripts.Area.LevelManager.View;
using Project.Scripts.Area.Player.Presenter;
using Project.Scripts.Area.Zombie.Model;
using Project.Scripts.Area.Zombie.Presenter;

namespace Project.Scripts.Area.LevelManager.Presenter
{
    public class LevelPresenter : IDisposable
    {
        private readonly ILevelView _view;
        private readonly ILevelModel _model;

        private readonly PlayerPresenter _playerPresenter;

        private readonly List<ZombiePresenter> _zombiePresenters = new List<ZombiePresenter>();

        public LevelPresenter(ILevelView levelView, ILevelModel levelModel)
        {
            _view = levelView;
            _model = levelModel;
            _playerPresenter = new PlayerPresenter(_view.CreatePlayer(), _model.Player);
            _zombiePresenters.Add(new ZombiePresenter(_view.GetNewZombie(), _model.GetNewZombie()));
            AddListeners();
        }

        private void AddListeners()
        {
            _model.ZombieRemoved += OnZombieModelRemoved;
        }

        private void RemoveListeners()
        {
            _model.ZombieRemoved -= OnZombieModelRemoved;
        }

        private void OnZombieModelRemoved(IZombieModel model)
        {
            foreach (var zombie in _zombiePresenters)
            {
                if (zombie.Model == model)
                {
                    zombie.Dispose();
                    _zombiePresenters.Remove(zombie);
                    return;
                }
            }
        }

        public void Dispose()
        {
            foreach (var disposable in _zombiePresenters)
            {
                disposable.Dispose();
            }

            RemoveListeners();
        }
    }
}