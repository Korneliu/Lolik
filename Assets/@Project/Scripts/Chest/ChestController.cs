using UnityEngine;

namespace SG
{
    public class ChestController : Interactable
    {
        [SerializeField] Animator animator;

        public GameObject item;
        public GameObject openChestPanel;

        public bool isOpen = false;
        public bool isNear = false;
        private GameObject loot;

        public override void Interact(Player playerController)
        {
            animator.Play("Chest Open");
            isOpen = true;
            loot = Instantiate(item, transform.position, Quaternion.identity);
            loot.GetComponent<Collider>().enabled = false;
            openChestPanel.SetActive(false);
            GetComponent<Collider>().enabled = false;
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

        public void DropItems() // use animation event
        {
            loot.GetComponent<Collider>().enabled = true;
        }
    }
}