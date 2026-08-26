using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;

    [SerializeField] private ItemDescriptionHolder itemDescriptionHolder;

    private ItemSO item;
    private Image icon;

    private void Awake()
    {
        if (itemDescriptionHolder == null)
        {
            itemDescriptionHolder = GameObject.Find("InventoryManager").GetComponent<ItemDescriptionHolder>();
        }
    }

    public Image GetIcon()
    {
        if (icon == null)
        {
            icon = transform.GetChild(0).GetComponent<Image>();
        }
        return icon;
    }

    public ItemSO GetItem()
    {
        if(item == null)
        {
            Debug.Log("Slot is empty.");
        }
        return item;
    }

    public void setItem(ItemSO newItem)
    {
        item = newItem;
        GetIcon().sprite = item.itemIcon;
    }

    public void UpdateSlot()
    {
        if(item != null)
        {
            GetIcon().enabled = true;
            GetIcon().sprite = item.itemIcon;
        }
        else
        {
            GetIcon().enabled = false;
            GetIcon().sprite = null;
        }
    }

    public void ClearSlot()
    {
        item = null;
        GetIcon().enabled = false;
        GetIcon().sprite = null;

        UpdateSlot();
    }

    public bool HasItem()
    {
        return item != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;

        if (itemDescriptionHolder != null)
        {
            itemDescriptionHolder.SetItem(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;

        if (itemDescriptionHolder != null)
        {
            itemDescriptionHolder.Clear();
        }
    }
}
