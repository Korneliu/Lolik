using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        List<HandEquipmentSlotUI> handEquipmentSlotUI = new List<HandEquipmentSlotUI>();

        private void Start()
        {
            handEquipmentSlotUI = GetComponentsInChildren<HandEquipmentSlotUI>().ToList();
        }

        //private void OnEnable()
        //{
        //    LoadWeaponsOnEquipmentScreen(FindObjectOfType<PlayerInventory>());
        //}

        public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for (int i = 0; i < handEquipmentSlotUI.Count; i++)
            {
                if (handEquipmentSlotUI[i].rightHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[0]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[1]);
                }
                else if (handEquipmentSlotUI[i].leftHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponInLeftHandSlots[0]);
                }
                else
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponInLeftHandSlots[1]);
                }
            }
        }

        public void SelectRightHandSlot01()
        {
            rightHandSlot01Selected = true;
        }

        public void SelectRightHandSlot02()
        {
            rightHandSlot02Selected = true;
        }

        public void SelectLeftHandSlot01()
        {
            leftHandSlot01Selected = true;
        }

        public void SelectLeftHandSlot02()
        {
            leftHandSlot02Selected = true;
        }
    }
}
