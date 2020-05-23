using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    InventoryUI inventoryUI;
    public GameObject item;

    private void Awake()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryUI.AddItem(item);
            Destroy(gameObject);
        }
    }
}
