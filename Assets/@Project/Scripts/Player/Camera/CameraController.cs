using System;
using Cinemachine;
using UnityEngine;

namespace SG
{
    [Serializable]
    public class CameraController
    {
        private const float _threshold = 0.01f;

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

        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;


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
    }
}