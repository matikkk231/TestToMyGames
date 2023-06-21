using System;
using UnityEngine;

namespace Project.Scripts.Area.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Rigidbody _rigidbody;
        private const float _speed = 2;
        private Vector3 _moveDirection;

        private void FixedUpdate()
        {
            DeclareMoveDirection();
            Move(_moveDirection);
            LookAtMouse();
        }

        // better to think about more productive decision
        private void LookAtMouse()
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                transform.LookAt(hit.point);
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