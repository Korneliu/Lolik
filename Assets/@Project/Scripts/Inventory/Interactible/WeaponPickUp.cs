using UnityEngine;

namespace SG
{
    public class WeaponPickUp : Interactable
    {
        public GameObject pickUpPanel;
        public WeaponItem weapon;

        public override void Interact(Player playerController)
        {
            PickUpItem(playerController);
            pickUpPanel.SetActive(false);
        }

        private void PickUpItem(Player playerController)
        {
            playerController.PickUpItem(weapon);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                pickUpPanel.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                pickUpPanel.SetActive(false);
            }
        }

    }
}