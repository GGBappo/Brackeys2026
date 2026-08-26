using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.EventSystems;

public class InteractableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Item Data")]
    [SerializeField] private ItemSO itemData;
    [SerializeField] private Collider itemCollider;

    [Header("Player Information")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Collider playerCollider;

    [Header("UI Feedback")]
    [SerializeField] private GameObject pickupPrompt;

    private bool isInRange = false;

    private void OnValidate()
    {
        if (playerCollider == null)
        {
            playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        }
        if (playerInventory == null)
        {
            playerInventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        }
        if (itemCollider == null)
        {
            itemCollider = GetComponent<Collider>();
        }
    }

    public ItemSO item { get => itemData; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }
    }
}
