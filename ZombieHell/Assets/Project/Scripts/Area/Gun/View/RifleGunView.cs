using System;
using Project.Scripts.Area.Zombie.View;
using Project.Scripts.Base.AdoioService.View;
using UnityEngine;

namespace Project.Scripts.Area.Gun.View
{
    public class RifleGunView : MonoBehaviour, IGunView
    {
        public Action AttackPressed { get; set; }

        [SerializeField] private float _shootingInterval = 0.2f;
        [SerializeField] private AudioClip _shootSound;

        private float _timeTillGunReady;
        private Vector3 _attackDirection;
        private IAudioServiceView _audioServiceView;

        public void Attack(int damage)
        {
            if (_timeTillGunReady > 0)
            {
                return;
            }

            Physics.Raycast(transform.position, _attackDirection, out var hit, 40);
            if (hit.collider != null)
            {
                var isZombie = hit.collider.CompareTag("Zombie");
                if (isZombie)
                {
                    var zombieView = hit.collider.GetComponent<IZombieView>();
                    zombieView.GetDamage(damage);
                }
            }

            _audioServiceView.Play(_shootSound);
            _timeTillGunReady = _shootingInterval;
        }

        public void AddAudioService(IAudioServiceView audioServiceView)
        {
            _audioServiceView = audioServiceView;
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
                var isPlayer = hit.collider.CompareTag("Player");
                if (!isPlayer)
                {
                    _attackDirection = hit.point - transform.position;
                }
            }
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