using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject target;
    int index = 0;
    public int Index { get => index; 
    set
        {
            if (value > slots.Length - 1)
            {
                value = 0;
            }

            if (value < 0)
            {
                value = slots.Length - 1;
            }

            index = value;
        }
    }
    public Transform[] slots;

    public void Naviguation()
    {
        target.transform.position = slots[Index].transform.position;

        if (Input.GetButtonDown("NextItem"))
        {
            Index++;
        }

        if (Input.GetButtonDown("PreviousItem"))
        {
            Index--;
        }
    }

    void UseItem()
    {
        SlotUI slotUI = slots[Index].GetComponent<SlotUI>();

        if (Input.GetButtonDown("UseItem") && slotUI.containedItem != null)
        {
            slotUI.containedItem.Effect();
        }
    }

    public void AddItem(GameObject newItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            SlotUI slotUI = slots[i].GetComponent<SlotUI>();

            if (!slotUI.isOccupied)
            {
                GameObject newObject = Instantiate(newItem, slots[i].position, Quaternion.identity, slots[i]);
                slotUI.containedItem = newObject.GetComponent<Item>();
                break;
            }
        }
    }

    private void Update()
    {
        Naviguation();
        UseItem();
    }    
}
