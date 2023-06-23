using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Area.HealthBar
{
    public class HealthBarView : MonoBehaviour, IHealthBarView
    {
        [SerializeField] private Slider _bar;

        public void ShowBar(int currentHealth, int startHealth)
        {
            var healthPercent = (float) currentHealth / startHealth;
            _bar.value = healthPercent;
        }

        private void Update()
        {
            var oldCameraPosition = Camera.main.transform.position;
            var targetToLook = new Vector3(transform.position.x, oldCameraPosition.y, oldCameraPosition.z);
            transform.LookAt(targetToLook);
        }
    }
}