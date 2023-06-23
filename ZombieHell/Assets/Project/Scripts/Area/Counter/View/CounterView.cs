using TMPro;
using UnityEngine;

namespace Project.Scripts.Area.Counter.View
{
    public class CounterView : MonoBehaviour, ICounterView
    {
        [SerializeField] private TextMeshProUGUI _remainedZombiesText;
        [SerializeField] private TextMeshProUGUI _currentRoundText;

        public void ShowRound(int currentRound)
        {
            _currentRoundText.text = "Round: " + currentRound;
        }

        public void ShowRemainedZombies(int remainedZombies)
        {
            _remainedZombiesText.text = "Zombies: " + remainedZombies;
        }
    }
}