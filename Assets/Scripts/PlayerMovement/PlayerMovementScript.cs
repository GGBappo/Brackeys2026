using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject pickUpPrompt;

    // Suspicion meter
    [SerializeField] private SusMeter susMeter;

    private CharacterController characterController;
    private float cameraPitch;
    private Transform cameraTransform;
    private GlobalStateType currentState = GlobalStateType.Active;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }

        cameraTransform = playerCamera;
    }

    private void OnEnable()
    {
        GameEvents.OnGlobalStateChanged += UpdateState;
    }

    private void OnDisable()
    {
        GameEvents.OnGlobalStateChanged -= UpdateState;
    }

    private void OnValidate()
    {
        if (inventoryCanvas == null)
        {
            GameObject canvasObject = GameObject.Find("Canvas");
            if (canvasObject != null)
            {
                inventoryCanvas = canvasObject;
            }
        }

        if (playerInventory == null)
        {
            GameObject inventoryObject = GameObject.Find("Inventory");
            if (inventoryObject != null)
            {
                playerInventory = inventoryObject.GetComponent<Inventory>();
            }
        }
    }
    
    private void UpdateState(GlobalStateType newState)
    {
        currentState = newState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        UpdateCursorState();

        if (!IsInventoryOpen() && !DialogueManager.IsDialogueActive)
        {
            HandleInteraction();
            MovePlayer();
            LookAround();
        }
    }

    private void HandleInteraction()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            InteractableItem interactable = hit.collider != null
                ? hit.collider.GetComponent<InteractableItem>()
                : null;

            if (pickUpPrompt != null)
            {
                pickUpPrompt.SetActive(interactable != null);
            }

            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                if (interactable.item == null)
                {
                    Debug.LogWarning($"'{interactable.gameObject.name}' has no ItemSO assigned.");
                    return;
                }

                Debug.Log($"Picked up {interactable.item.itemName}");
                
                interactable.CollectItem();
            }
        }
        else
        {
            if (pickUpPrompt != null)
            {
                pickUpPrompt.SetActive(false);
            }
        }
    }

    private void ToggleInventory()
    {
        if (inventoryCanvas == null)
        {
            return;
        }

        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    private void UpdateCursorState()
    {
        bool uiActive = IsInventoryOpen() || DialogueManager.IsDialogueActive;

        Cursor.lockState = uiActive
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = uiActive;
    }

    private bool IsInventoryOpen()
    {
        return inventoryCanvas != null && inventoryCanvas.activeInHierarchy;
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement =
            transform.right * horizontal +
            transform.forward * vertical;

        characterController.Move(
            movement * moveSpeed * Time.deltaTime
        );
    }

    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        if (playerCamera != null)
        {
            playerCamera.localRotation =
                Quaternion.Euler(cameraPitch, 0f, 0f);
        }
    }
}