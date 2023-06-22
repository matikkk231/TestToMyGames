using UnityEngine;

namespace Project.Scripts.Area.GameCamera.View
{
    public class CameraView : MonoBehaviour
    {
        public Transform TargetToChase { get; set; }

        private void Update()
        {
            var position = TargetToChase.transform.position;
            var targetToLook = new Vector3(position.x, position.y, position.z);
            var cameraPosition = new Vector3(targetToLook.x, targetToLook.y + 9.66f, targetToLook.z - 5.93f);

            transform.position = cameraPosition;
            transform.LookAt(targetToLook);
        }
    }
}