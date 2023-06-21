using System;
using System.Collections.Generic;
using Project.Scripts.Area.LevelManager.Model;
using Project.Scripts.Area.LevelManager.View;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Player.Presenter;
using Project.Scripts.Area.Zombie.Model;
using Project.Scripts.Area.Zombie.Presenter;

namespace Project.Scripts.Area.LevelManager.Presenter
{
    public class LevelPresenter : IDisposable
    {
        private readonly ILevelView _view;
        private readonly ILevelModel _model;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public LevelPresenter(ILevelView levelView, ILevelModel levelModel)
        {
            _view = levelView;
            _model = levelModel;
            _disposables.Add(new PlayerPresenter(_view.CreatePlayer(), new PlayerModel()));
            _disposables.Add(new ZombiePresenter(_view.CreateZombie(), new ZombieModel()));
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}