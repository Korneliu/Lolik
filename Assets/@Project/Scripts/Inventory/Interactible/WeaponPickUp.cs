namespace SG
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(Player playerController)
        {
            PickUpItem(playerController);
        }

        private void PickUpItem(Player playerController)
        {
            playerController.PickUpItem(weapon);
            Destroy(gameObject);
        }
    }
}