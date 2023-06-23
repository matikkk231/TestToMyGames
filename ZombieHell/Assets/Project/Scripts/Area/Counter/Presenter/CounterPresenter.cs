using System;
using Project.Scripts.Area.Counter.Model;
using Project.Scripts.Area.Counter.View;

namespace Project.Scripts.Area.Counter.Presenter
{
    public class CounterPresenter : IDisposable
    {
        private readonly ICounterView _view;
        private readonly ICounterModel _model;


        public CounterPresenter(ICounterView view, ICounterModel model)
        {
            _view = view;
            _model = model;
            AddListeners();
            OnRoundChanged(_model.CurrentRound);
            OnRemainedZombiesChanged(model.RemainedZombies);
        }

        private void AddListeners()
        {
            _model.RoundChanged += OnRoundChanged;
            _model.AmountZombiesChanged += OnRemainedZombiesChanged;
        }

        private void RemoveListeners()
        {
            _model.RoundChanged -= OnRoundChanged;
            _model.AmountZombiesChanged -= OnRemainedZombiesChanged;
        }

        private void OnRemainedZombiesChanged(int remainedZombies)
        {
            _view.ShowRemainedZombies(remainedZombies);
        }

        private void OnRoundChanged(int currentRound)
        {
            _view.ShowRound(currentRound);
        }

        public void Dispose()
        {
            RemoveListeners();
        }
    }
}