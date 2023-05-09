using UnityEngine;

namespace SG
{
    public class ChestController : Interactable
    {
        Animator animator;

        public GameObject item;
        public GameObject openChestPanel;

        public bool isOpen = false;
        public bool isNear = false;

        public override void Interact(Player playerController)
        {
            animator.Play("Chest Open");
            isOpen = true;
            Instantiate(item, transform.position, Quaternion.identity);
            openChestPanel.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                openChestPanel.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                openChestPanel.SetActive(false);
            }
        }

    }
}
