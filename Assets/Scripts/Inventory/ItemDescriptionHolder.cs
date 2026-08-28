using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionHolder : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private Image itemIconImage;

    public void SetItem(ItemSO item)
    {
        if (item == null)
        {
            Clear();
            return;
        }

        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDescription;
        itemIconImage.sprite = item.itemIcon;
        itemIconImage.enabled = item.itemIcon != null;
    }

    public void Clear()
    {
        itemNameText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
        itemIconImage.sprite = null;
        itemIconImage.enabled = false;
    }
}