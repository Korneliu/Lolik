using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        [SerializeField] PlayerInventory playerInventory;
        [SerializeField] UIManager uiManager;
        public Vector2 look;
        public bool jump;
        public bool roll;
        public bool attack;
        public bool sprint;
        public bool IsPickUp;
        public bool inventoryInput;

        public bool inventoryFlag;

        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        private Input _input;

        private void Awake()
        {
            _input = new Input();
            _input.Enable();
            _input.PlayerQuickSlots.DPadRight.performed += DPadRightInput;
            _input.PlayerQuickSlots.DPadLeft.performed += DPadLeftInput;
            _input.Player.Inventory.performed += OpenWindow;
        }


        private void DPadLeftInput(InputAction.CallbackContext obj)
        {
            if (obj.ReadValue<float>() == 1F)
                DPadLeftInput();
        }

        private void OpenWindow(InputAction.CallbackContext obj)
        {
            if (obj.ReadValue<float>() == 1F)
                OpenWindow();
        }

        private void DPadRightInput(InputAction.CallbackContext obj)
        {
            if (obj.ReadValue<float>() == 1F)
                DPadRightInput();
        }

        private void Update()
        {
            RollInput(_input.Player.Roll.IsPressed());
            AttackInput(_input.Player.Attack.IsPressed());
            IsPickUp = _input.PickUpItem.PickUp.IsPressed();
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
            //Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void DPadRightInput()
        {
            playerInventory.ChangeRightWeapon();
        }

        public void DPadLeftInput()
        {
            playerInventory.ChangeLeftWeapon();
        }

        public void OpenWindow()
        {
            inventoryInput = _input.Player.Inventory.IsPressed();

            if (inventoryInput)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                }
                else
                {
                    uiManager.CloseSelectWindow();
                }
            }

        }

        //public void CloseWindow()
        //{
        //}
    }
}