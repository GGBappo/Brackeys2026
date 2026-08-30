using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [Header("Item Data")]
    [SerializeField] private ItemSO itemData;
    [SerializeField] private Collider itemCollider;

    [Header("Story Integration")]
    [Tooltip("The exact MemoryBoard key, e.g., 'Item_PillBottle' or 'Item_Passport'")]
    public string memoryKey = ""; 
    [Tooltip("Check this if finding this item counts toward the 4 needed for the Ring")]
    public bool countsTowardsTotal = true; 

    [Header("Player Information")]
    [SerializeField] private Inventory playerInventory;

    [Header("UI Feedback")]
    [SerializeField] private GameObject pickupPrompt;

    private bool _isPlayerNear = false;
    public ItemSO item => itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = true;
            if (pickupPrompt != null) pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = false;
            if (pickupPrompt != null) pickupPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        // Assuming 'E' is your interact key. Change if you are using the new Input System.
        if (_isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    public void CollectItem()
    {
        if (playerInventory != null && itemData != null) 
        {
            playerInventory.AddItem(itemData);
        }

        if (SusMeter.Instance != null) 
        {
            SusMeter.Instance.RegisterItemPickup();
        }

        if (!string.IsNullOrEmpty(memoryKey))
        {
            MemoryBoard.SetVariable(memoryKey, "true");

            if (countsTowardsTotal)
            {
                string currentTotalStr = MemoryBoard.GetVariable("TotalItemsFound");
                if (int.TryParse(currentTotalStr, out int total))
                {
                    total++;
                    MemoryBoard.SetVariable("TotalItemsFound", total.ToString());
                    
                    // FIXED: Checks if the required 4 items are found to unlock the ring
                    if (total >= 4)
                    {
                        MemoryBoard.SetVariable("Item_Ring", "true");
                    }
                }
            
            }
        }

        if (pickupPrompt != null) pickupPrompt.SetActive(false);
        Destroy(gameObject); 
    }
}
