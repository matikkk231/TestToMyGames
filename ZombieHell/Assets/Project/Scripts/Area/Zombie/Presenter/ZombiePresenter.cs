using System;
using Project.Scripts.Area.Zombie.Model;
using Project.Scripts.Area.Zombie.View;

namespace Project.Scripts.Area.Zombie.Presenter
{
    public class ZombiePresenter : IDisposable
    {
        private readonly IZombieView _view;
        public readonly IZombieModel Model;

        public ZombiePresenter(IZombieView view, IZombieModel model)
        {
            _view = view;
            Model = model;
            AddListeners();
        }

        private void AddListeners()
        {
            Model.Died += OnModelDied;
            _view.Damaged += OnDamaged;
        }

        private void RemoveListeners()
        {
            Model.Died -= OnModelDied;
            _view.Damaged -= OnDamaged;
        }

        private void OnModelDied()
        {
            _view.Removed?.Invoke(_view);
        }

        private void OnDamaged(int damage)
        {
            Model.GetDamage(damage);
        }

        public void Dispose()
        {
            RemoveListeners();
        }
    }
}