namespace Project.Scripts.Area.Counter.View
{
    public interface ICounterView
    {
        public void ShowRound(int currentRound);
        public void ShowRemainedZombies(int remainedZombies);
    }
}