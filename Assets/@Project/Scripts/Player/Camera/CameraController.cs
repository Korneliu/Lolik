using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    [Serializable]
    public class CameraController
    {
        private const string StatePlayer = "Player", StateEnemy = "Enemy";
        
        private const float _threshold = 0.01f;

        [SerializeField] private Animator _animatorState;
        [SerializeField] private CinemachineVirtualCamera _camEnemy;
        [SerializeField] private Transform _enemyBody;

        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        public float Sesitivity = 1.0f;

        private UnityEngine.InputSystem.PlayerInput _playerInput;
        private PlayerInput _input;
        private Input _Input;

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private bool isEnemyCamera;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }

        public void Initialize(UnityEngine.InputSystem.PlayerInput playerInput, PlayerInput input)
        {
            _playerInput = playerInput;
            _input = input;
            _Input = new Input();
            _Input.Enable();
            _Input.Player.LockOn.performed += SwapCamera;
        }

        public void Update()
        {
            _enemyBody.LookAt(_camEnemy.LookAt);
        }

        private void SwapCamera(InputAction.CallbackContext obj)
        {
            isEnemyCamera = !isEnemyCamera;

            if (isEnemyCamera)
            {
                SetStateEnemy(_camEnemy.LookAt);
            }
            else
            {
                SetStatePlayer();
            }
        }


        public void CameraRotation()
        {
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Sesitivity * Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch -= _input.look.y * deltaTimeMultiplier;
            }

            _cinemachineTargetYaw = Math.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = Math.ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        public void Connect(CinemachineVirtualCamera stateDrivenCamera)
        {
            var virtualCamera = stateDrivenCamera;
            virtualCamera.Follow = virtualCamera.LookAt = CinemachineCameraTarget.transform;
            _input.cursorInputForLook = _input.cursorLocked = true;
        }

        public void SetStatePlayer() => _animatorState.Play(StatePlayer);

        public void SetStateEnemy(Transform target)
        {
            _camEnemy.LookAt = target;
            _animatorState.Play(StateEnemy);
        }

    }
}