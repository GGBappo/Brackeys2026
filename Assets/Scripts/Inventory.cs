using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemSO item;
    public GameObject inventorySlotsParent;

    private List<Slot> InventorySlots = new List<Slot>();

    public void Awake()
    {
        InventorySlots.AddRange(inventorySlotsParent.GetComponentsInChildren<Slot>());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(item);
            Debug.Log("Item added to inventory");
        }
    }

    public void AddItem(ItemSO itemToAdd)
    {
        foreach (Slot slot in InventorySlots)
        {
            if (slot.HasItem() && slot.GetItem() == itemToAdd)
            {
                slot.setItem(itemToAdd);
            }
        }

        foreach (Slot slot in InventorySlots)
        {
            if (!slot.HasItem())
            {
                slot.setItem(itemToAdd);
                break;
            }
        }
    }

    private void FillItemData()
    {

    }
}