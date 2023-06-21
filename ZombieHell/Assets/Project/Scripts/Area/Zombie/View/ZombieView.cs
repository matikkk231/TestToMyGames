using System;
using Project.Scripts.Area.Player.View;
using UnityEngine;

namespace Project.Scripts.Area.Zombie.View
{
    public class ZombieView : MonoBehaviour, IZombieView
    {
        public Action<int> Damaged { get; set; }

        [SerializeField] private Rigidbody _rigidbody;
        private const float _speed = 2;
        private const float _attackDisctance = 2;
        private const float _attackCooldownTime = 2;
        private float _timeTillReadyAttack;

        public Transform TargetToChase { get; set; }

        private void FixedUpdate()
        {
            ChaseTarget();
            LookAtTarget();
            if (_timeTillReadyAttack < 0)
            {
                Attack();
            }
        }

        private void Update()
        {
            _timeTillReadyAttack -= Time.deltaTime;
        }

        public void ChaseTarget()
        {
            _rigidbody.velocity =
                (TargetToChase.position - transform.position).normalized * _speed;
        }

        public void GetDamage(int damage)
        {
            Damaged?.Invoke(damage);
            Debug.Log("damaged");
        }

        private void LookAtTarget()
        {
            transform.LookAt(TargetToChase.position);
        }

        private void Attack()
        {
            var position = transform.position;
            var rayStart = new Vector3(position.x, position.y + 2, position.z);
            var rayDirection = (TargetToChase.position - position).normalized;
            if (Physics.Raycast(rayStart, rayDirection, out var hit, _attackDisctance))
            {
                var playerView = hit.collider.GetComponent<IPlayerView>();
                if (playerView != null)
                {
                    playerView.GetDamage(4);
                    _timeTillReadyAttack = _attackCooldownTime;
                }
            }
        }
    }
}