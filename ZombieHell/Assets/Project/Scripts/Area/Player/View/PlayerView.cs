using System;
using Project.Scripts.Area.GameCamera.View;
using Project.Scripts.Area.Gun.View;
using Project.Scripts.Base.AdoioService.View;
using UnityEngine;

namespace Project.Scripts.Area.Player.View
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        public Action<int> Damaged { get; set; }

        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private RifleGunView _gunView;
        private Vector3 _minMovingPosition;
        private Vector3 _maxMovingPosition;
        private const float _speed = 4;
        private Vector3 _moveDirection;
        private IAudioServiceView _audioServiceView;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public Transform Transform
        {
            get => transform;
        }

        public IGunView Gun => _gunView;

        public void GetDamage(int damage)
        {
            Damaged?.Invoke(damage);
        }

        public void AddAudioService(IAudioServiceView audioServiceView)
        {
            _audioServiceView = audioServiceView;
            _gunView.AddAudioService(_audioServiceView);
        }

        public void SetMovingArea(Vector3 minPosition, Vector3 maxPosition)
        {
            _maxMovingPosition = maxPosition;
            _minMovingPosition = minPosition;
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

        private void Awake()
        {
            CreateCamera();
        }

        private void CreateCamera()
        {
            var camera = Instantiate(new GameObject());
            camera.name = "Camera";
            camera.tag = "MainCamera";
            camera.AddComponent<AudioListener>();
            camera.AddComponent<Camera>();
            var cameraView = camera.AddComponent<CameraView>();
            cameraView.TargetToChase = Transform;
        }

        private void DeclareMoveDirection()
        {
            _moveDirection = Vector3.zero;
            var isOutBorderLeft = transform.position.x < _minMovingPosition.x;
            if (Input.GetKey(KeyCode.A) && !isOutBorderLeft)
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(-1, oldDirection.y, oldDirection.z);
            }

            var isOutBorderRight = transform.position.x > _maxMovingPosition.x;
            if (Input.GetKey(KeyCode.D) && !isOutBorderRight)
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(1, oldDirection.y, oldDirection.z);
            }

            var isOutBorderUpper = transform.position.y > _maxMovingPosition.y;
            if (Input.GetKey(KeyCode.W) && !isOutBorderUpper)
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(oldDirection.x, oldDirection.y, 1);
            }

            var isOutBorderUnder = transform.position.y < _minMovingPosition.y;
            if (Input.GetKey(KeyCode.S) && !isOutBorderUnder)
            {
                var oldDirection = _moveDirection;
                _moveDirection = new Vector3(oldDirection.x, oldDirection.y, -1);
            }
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.velocity = direction * _speed;
            if (direction != Vector3.zero)
            {
                _playerAnimator.SetBool(IsMoving, true);
                return;
            }

            _playerAnimator.SetBool(IsMoving, false);
        }
    }
}