using System;

namespace Project.Scripts.Area.Counter.Model
{
    public class CounterModel : ICounterModel
    {
        public Action<int> RoundChanged { get; set; }
        public Action<int> AmountZombiesChanged { get; set; }

        private int _currentRound;
        private int _remainedZombies;

        public int CurrentRound
        {
            get => _currentRound;
            set
            {
                _currentRound = value;
                RoundChanged?.Invoke(_currentRound);
            }
        }

        public int RemainedZombies
        {
            get => _remainedZombies;
            set
            {
                _remainedZombies = value;
                AmountZombiesChanged?.Invoke(_remainedZombies);
            }
        }
    }
}