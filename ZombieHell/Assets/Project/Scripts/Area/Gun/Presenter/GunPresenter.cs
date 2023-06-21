using System;
using Project.Scripts.Area.Gun.Model;
using Project.Scripts.Area.Gun.View;

namespace Project.Scripts.Area.Gun.Presenter
{
    public class GunPresenter : IDisposable
    {
        private readonly IGunView _view;
        private readonly IGunModel _model;

        public GunPresenter(IGunView view, IGunModel model)
        {
            _view = view;
            _model = model;
            AddListeners();
        }

        private void AddListeners()
        {
            _view.AttackPressed += OnAttackPressed;
            _model.Attacked += OnAttacked;
        }

        private void RemoveListeners()
        {
            _view.AttackPressed -= OnAttackPressed;
            _model.Attacked -= OnAttacked;
        }

        private void OnAttacked(int damage)
        {
            _view.Attack(damage);
        }

        private void OnAttackPressed()
        {
            _model.Attack();
        }

        public void Dispose()
        {
            RemoveListeners();
        }
    }
}