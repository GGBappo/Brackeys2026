using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.EventSystems;

public class InteractableItem : MonoBehaviour
{
    [Header("Item Data")]
    [SerializeField] private ItemSO itemData;
    [SerializeField] private Collider itemCollider;

    [Header("Player Information")]
    [SerializeField] private Inventory playerInventory;

    [Header("UI Feedback")]
    [SerializeField] private GameObject pickupPrompt;


    public ItemSO item { get => itemData; }
}
