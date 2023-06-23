using System;

namespace Project.Scripts.Area.Counter.Model
{
    public interface ICounterModel
    {
        Action<int> RoundChanged { get; set; }
        Action<int> AmountZombiesChanged { get; set; }

        public int CurrentRound { get; set; }
        public int RemainedZombies { get; set; }
    }
}