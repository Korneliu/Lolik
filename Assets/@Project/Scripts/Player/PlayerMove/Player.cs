using Cinemachine;
using UnityEngine;

namespace SG
{
    [RequireComponent(typeof(CharacterController),
        typeof(UnityEngine.InputSystem.PlayerInput),
        typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private CameraController _cameraController;

        [SerializeField] private PlayerMovement _playerMovement;

        [SerializeField] private CinemachineVirtualCamera _camPlayer;

        private Animator _animator;

        private Animator Animator
        {
            get
            {
                if (_animator == null) _animator = transform.GetComponentInChildren<Animator>();
                return _animator;
            }
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            _playerController.Initialize(transform,
                Animator,
                GetComponent<UnityEngine.InputSystem.PlayerInput>(),
                GetComponent<PlayerInput>(),
                GetComponent<CharacterController>(),
                _cameraController,
                _playerMovement);
        }

        private void OnEnable()
        {
            _playerController._playerInput.enabled = true;
            SetCamera(_camPlayer);
        }

        private void Update()
        {
            if (Animator == null) return;
            _playerController.Update();
            _cameraController.Update();
        }

        private void LateUpdate()
        {
            _playerController.LateUpdate();
        }

        private void SetCamera(CinemachineVirtualCamera stateDrivenCamera)
        {
            if (stateDrivenCamera == null) return;
            _cameraController.Connect(stateDrivenCamera);
        }

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            if (transform == null) return;
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (_playerController.Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - _playerController.GroundedOffset,
                    transform.position.z), _playerController.GroundedRadius);
        }
#endif
        public void PickUpItem(WeaponItem weaponItem)
        {
            Animator.Play("PickUp");
            GetComponent<PlayerInventory>().weaponsInventory.Add(weaponItem);
        }
    }
}