using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class PlayerInput : MonoBehaviour
    {
        
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool roll;
        public bool attack;
        public bool sprint;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        PlayerInventory playerInventory;

        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        private Input _input;

        private void Awake()
        {
            _input = new Input();
            _input.Enable();
        }

        private void Update()
        {
            RollInput(_input.Player.Roll.IsPressed());
            AttackInput(_input.Player.Attack.IsPressed());
        }

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void AttackInput(bool newJumpState)
        {
            attack = newJumpState;
        }

        public void RollInput(bool newRollState)
        {
            roll = newRollState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }



        public void TickInput(float delta)
        {
            HandleQuickSlotsInput();
        }

        private void HandleQuickSlotsInput()
        {
            _input.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
            _input.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;

            if (d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }else if (d_Pad_Left)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }
    }
}