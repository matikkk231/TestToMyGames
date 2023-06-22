using System;
using Project.Scripts.Area.Gun.View;
using Project.Scripts.Area.Zombie.View;
using UnityEngine;

namespace Project.Scripts.Area.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public Action<int> Damaged { get; set; }

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RifleGunView _gunView;
        private const float _speed = 4;
        private Vector3 _moveDirection;

        public Transform Transform
        {
            get => transform;
        }

        public IGunView Gun => _gunView;

        public void GetDamage(int damage)
        {
            Damaged?.Invoke(damage);
        }

        private void Update()
        {
            DeclareMoveDirection();
            Move(_moveDirection);
        }

        private void FixedUpdate()
        {
            LookAtMouse();
        }

        private void LookAtMouse()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Transform transform1;
                (transform1 = transform).LookAt(hit.point);
                var oldRotation = transform1.rotation;
                transform1.rotation = new Quaternion(0, oldRotation.y, 0, oldRotation.w);
            }
        }

        private void DeclareMoveDirection()
        {
            _moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(-1, oldDirection.y, oldDirection.z);
            }

            if (Input.GetKey(KeyCode.D))
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(1, oldDirection.y, oldDirection.z);
            }

            if (Input.GetKey(KeyCode.W))
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(oldDirection.x, oldDirection.y, 1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(oldDirection.x, oldDirection.y, -1);
            }
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }
    }
}