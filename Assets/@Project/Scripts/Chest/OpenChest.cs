using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace SG
{
    public class OpenChest : Interactable
    {
        Animator animator;
        OpenChest openChest;

        public Transform playerStadingPosition;
        public GameObject itemSpawner;
        public WeaponItem itemInChest;

        private void Awake()
        {
            animator = GetComponent<Animator>();  
            openChest = GetComponent<OpenChest>();
        }

        public override void Interact(Player playerController)
        {
            Vector3 rotationDirection = transform.position - playerController.transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(playerController.transform.rotation, tr, 300 * Time.deltaTime);
            playerController.transform.rotation = targetRotation;

            playerController.OpenChestInteraction(playerStadingPosition);
            animator.Play("Chest Open");
            StartCoroutine(SpawnItemInChest());

            WeaponPickUp weaponPickUp = itemSpawner.GetComponent<WeaponPickUp>();

            if (weaponPickUp != null)
            {
                weaponPickUp.weapon = itemInChest;
            }
        }

        private IEnumerator SpawnItemInChest()
        {
            yield return new WaitForSeconds(1f);
            Instantiate(itemSpawner, transform);
            Destroy(openChest);
        }

    }
}
