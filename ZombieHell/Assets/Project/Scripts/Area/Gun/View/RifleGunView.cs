using System;
using Project.Scripts.Area.Player.View;
using Project.Scripts.Area.Zombie.View;
using UnityEngine;

namespace Project.Scripts.Area.Gun.View
{
    public class RifleGunView : MonoBehaviour, IGunView
    {
        public Action AttackPressed { get; set; }

        private const float _shootingInterval = 0.2f;
        private float _timeTillGunReady;
        private Vector3 _attackDirection;

        public void Attack(int damage)
        {
            if (_timeTillGunReady > 0)
            {
                return;
            }

            Physics.Raycast(transform.position, _attackDirection, out var hit, 40);
            if (hit.collider != null)
            {
                var zombieView = hit.collider.GetComponent<IZombieView>();
                if (zombieView != null)
                {
                    zombieView.GetDamage(damage);
                }
            }

            _timeTillGunReady = _shootingInterval;
        }

        private bool CheckIsAttackPressed()
        {
            var leftMouseButton = 0;
            var isAttackPressing = Input.GetMouseButton(leftMouseButton);
            return isAttackPressing;
        }

        private void DeclareAttackDirection()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var playerView = hit.collider.GetComponent<PlayerView>();
                if (playerView == null)
                {
                    _attackDirection = hit.point - transform.position;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, _attackDirection);
        }

        private void Update()
        {
            if (_timeTillGunReady >= 0)
            {
                _timeTillGunReady -= Time.deltaTime;
                return;
            }

            if (CheckIsAttackPressed())
            {
                DeclareAttackDirection();
                AttackPressed?.Invoke();
            }
        }
    }
}