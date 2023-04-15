using UnityEngine;

namespace SG
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerController playerController)
        {
            base.Interact(playerController);
        }

        private void PickUpItem(PlayerController playerController)
        {
            PlayerInventory playerInventory;
            PlayerMovement playerMovement;
            Animator animator;

            playerInventory = playerController.GetComponent<PlayerInventory>();
            playerMovement = playerController.GetComponent<PlayerMovement>();
            animator = playerController.GetComponentInChildren<Animator>();

            playerMovement.rigidbody.velocity = Vector3.zero;
            animator.Play("PickUpItem");
            playerInventory.weaponsInventory.Add(weapon);
            Destroy(gameObject);
        }
    }
}