using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.Area.MainMenu.View
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [SerializeField] private TextMeshProUGUI _notificationText;
        private const string _winNotification = "YOU WON BRO";
        private const string _looseNotification = "YOU LOOSED, TRY AGAIN, LOOSER!";

        public void ShowLooseNotify()
        {
            this.gameObject.SetActive(true);
            _notificationText.text = _looseNotification;
        }

        public void ShowWinNotify()
        {
            this.gameObject.SetActive(true);
            _notificationText.text = _winNotification;
        }

        public void RestartGame()
        {
            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.path);
        }
    }
}