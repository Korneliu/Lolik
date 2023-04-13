using System;
using UnityEngine;

namespace SG
{
    [Serializable]
    public class PlayerController
    {
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")] public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Tooltip("What layers the character uses as ground")]

        private Animator _animator;
        private Transform _transform;
        private PlayerMovement _movement;
        private CameraController _camera;

        public int _animIDSpeed { get; private set; }
        public int _animIDGrounded { get; private set; }
        public int _animIDAttack { get; private set; }
        public int _animIDJump { get; private set; }
        public int animIDRoll { get; private set; }
        public int _animIDFreeFall { get; private set; }
        public int _animIDMotionSpeed { get; private set; }
        public UnityEngine.InputSystem.PlayerInput _playerInput { get; private set; }
        public PlayerInput Input { get; private set; }

        public void Initialize(Transform transform,
            Animator animator,
            UnityEngine.InputSystem.PlayerInput playerInput,
            PlayerInput input,
            CharacterController controller,
            CameraController cameraController,
            PlayerMovement movement)
        {
            _transform = transform;
            _animator = animator;
            _playerInput = playerInput;
            Input = input;

            _movement = movement;
            _camera = cameraController;
            _movement.Initialize(transform, this, _animator, input, controller);
            _camera.Initialize(playerInput, input);

            AssignAnimationIDs();
        }

        public void Update()
        {
            _movement.JumpAndGravity();
            GroundedCheck();
            _movement.Move();
            _animator.SetBool(_animIDAttack, Input.attack);
        }

        public void LateUpdate()
        {
            _camera.CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDAttack = Animator.StringToHash("Attack");
            _animIDJump = Animator.StringToHash("Jump");
            animIDRoll = Animator.StringToHash("Roll");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(_transform.position.x, _transform.position.y - GroundedOffset,
                _transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            _animator.SetBool(_animIDGrounded, Grounded);
        }

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            if (_transform == null) return;
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Gizmos.DrawSphere(
                new Vector3(_transform.position.x, _transform.position.y - GroundedOffset, _transform.position.z),
                GroundedRadius);
        }
#endif
    }
}