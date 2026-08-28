using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Item Data")]
public class ItemSO : ScriptableObject
{

    [TextArea(3, 10)]
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    public GameObject itemPrefab;
    public int maxStackSize = 1;
}
