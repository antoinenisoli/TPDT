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
    public SlotUI[] slots;
    public List<Item> items;

    private void Awake()
    {
        slots = GetComponentsInChildren<SlotUI>();       
    }

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
        if (Input.GetButtonDown("UseItem") && slots[Index].containedItem != null && slots[Index].containedItem.CanUse())
        {
            items.Remove(items[Index]);
            slots[Index].containedItem.Effect();
            slots[Index].isOccupied = false;
        }
    }

    public void AddItem(GameObject newItem, GameObject picked)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isOccupied)
            {
                GameObject newObject = Instantiate(newItem, slots[i].transform.position, Quaternion.identity, slots[i].transform);
                slots[i].containedItem = newObject.GetComponent<Item>();
                slots[i].isOccupied = true;
                items.Add(slots[i].containedItem);
                Destroy(picked);
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
