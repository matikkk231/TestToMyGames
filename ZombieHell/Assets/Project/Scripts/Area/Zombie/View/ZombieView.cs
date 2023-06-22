using System;
using Project.Scripts.Area.Player.View;
using UnityEngine;

namespace Project.Scripts.Area.Zombie.View
{
    public class ZombieView : MonoBehaviour, IZombieView
    {
        public Action TargetFound { get; set; }
        public Action<int> Damaged { get; set; }
        public Action<IZombieView> Removed { get; set; }

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        [SerializeField] private Rigidbody _rigidbody;
        private IPlayerView _foundPlayer;
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
                var player = SearchPlayer();
                if (player != null)
                {
                    _foundPlayer = player;
                    TargetFound?.Invoke();
                }
            }
        }

        private void Update()
        {
            _timeTillReadyAttack -= Time.deltaTime;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void ChaseTarget()
        {
            _rigidbody.velocity =
                (TargetToChase.position - transform.position).normalized * _speed;
        }

        public void GetDamage(int damage)
        {
            Damaged?.Invoke(damage);
        }

        public void Attack(int damage)
        {
            _foundPlayer.GetDamage(damage);
            _foundPlayer = null;
        }

        private void LookAtTarget()
        {
            transform.LookAt(TargetToChase.position);
        }

        private IPlayerView SearchPlayer()
        {
            var position = transform.position;
            var rayStart = new Vector3(position.x, position.y + 2, position.z);
            var rayDirection = (TargetToChase.position - position).normalized;
            if (Physics.Raycast(rayStart, rayDirection, out var hit, _attackDisctance))
            {
                if (hit.collider.tag == "Player")
                {
                    var playerView = hit.collider.GetComponent<IPlayerView>();
                    _timeTillReadyAttack = _attackCooldownTime;
                    return playerView;
                }
            }

            return null;
        }
    }
}