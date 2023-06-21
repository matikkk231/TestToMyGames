using System;
using System.Collections.Generic;
using Project.Scripts.Area.Gun.Presenter;
using Project.Scripts.Area.Player.Model;
using Project.Scripts.Area.Player.View;

namespace Project.Scripts.Area.Player.Presenter
{
    public class PlayerPresenter : IDisposable
    {
        private readonly IPlayerView _view;
        private readonly IPlayerModel _model;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public PlayerPresenter(IPlayerView view, IPlayerModel model)
        {
            _model = model;
            _view = view;
            _disposables.Add(new GunPresenter(view.Gun, model.Gun));
            AddListeners();
        }

        private void AddListeners()
        {
            _view.Damaged += OnDamaged;
        }

        private void RemoveListeners()
        {
            _view.Damaged -= OnDamaged;
        }

        private void OnDamaged(int damage)
        {
            _model.GetDamage(damage);
        }

        public void Dispose()
        {
            RemoveListeners();
        }
    }
}