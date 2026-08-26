using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;

    private ItemSO item;
    private Image icon;


    private void Awake()
    {
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    public ItemSO GetItem()
    {
        return item;
    }

    public void setItem(ItemSO newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
    }

    public void UpdateSlot()
    {
        if(item != null)
        {
            icon.enabled = true;
            icon.sprite = item.itemIcon;
        }
        else
        {
            icon.enabled = false;
            icon.sprite = null;
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        icon.sprite = null;

        UpdateSlot();
    }

    public bool HasItem()
    {
        return item != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        Debug.Log("Hovering over slot with item: " + (item != null ? item.itemName : "None"));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        Debug.Log("Stopped hovering over slot with item: " + (item != null ? item.itemName : "None"));
    }
}  
