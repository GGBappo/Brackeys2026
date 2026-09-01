using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

public class PhoneController : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform phonePanel;
    public Transform scrollContentContainer; 
    public Transform choiceContainer;
    public ScrollRect phoneScrollRect;
    
    [Header("Notification UI")]
    public GameObject notificationPopup;
    public TextMeshProUGUI notificationCountText;

    [Header("Prefabs")]
    public GameObject friendBubblePrefab;
    public GameObject playerBubblePrefab;

    [Header("Tween Settings")]
    public float hiddenYPosition = -1200f;
    public float visibleYPosition = 0f;
    public float tweenDuration = 0.4f;
    public KeyCode toggleKey = KeyCode.M;

    [HideInInspector] public bool isPhoneOpen = false;
    [HideInInspector] public List<PhoneMessage> messageHistory = new List<PhoneMessage>();
    
    private int _unreadCount = 0;

    private void Start()
    {
        phonePanel.anchoredPosition = new Vector2(phonePanel.anchoredPosition.x, hiddenYPosition);
        if (notificationPopup != null) notificationPopup.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey)) TogglePhone();
    }

    public void TogglePhone()
    {
        if (DialogueManager.IsDialogueActive && choiceContainer.childCount == 0) return;

        isPhoneOpen = !isPhoneOpen;

        if (isPhoneOpen)
        {
            _unreadCount = 0;
            if (notificationPopup != null) notificationPopup.SetActive(false);
            
            RefreshMessageHistory();
            phonePanel.DOAnchorPosY(visibleYPosition, tweenDuration).SetEase(Ease.OutBack);
            GameEvents.RequestPlaySFX("PhoneUnlock"); 
        }
        else
        {
            phonePanel.DOAnchorPosY(hiddenYPosition, tweenDuration).SetEase(Ease.InBack);
        }
    }

    public void ReceiveNewText(string text, bool isPlayer)
    {
        if (messageHistory.Count > 0 && messageHistory[messageHistory.Count - 1].text == text) return;

        messageHistory.Add(new PhoneMessage { text = text, isPlayer = isPlayer });
        
        if (isPhoneOpen) 
        {
            RefreshMessageHistory();
        }
        else if (!isPlayer)
        {
            _unreadCount++;
            if (notificationPopup != null) 
            {
                notificationPopup.SetActive(true);
                notificationPopup.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f, 10, 1f);
            }
            if (notificationCountText != null) notificationCountText.text = _unreadCount.ToString();
        }
    }

    public void ForceClosePhone()
    {
        if (isPhoneOpen)
        {
            isPhoneOpen = false;
            phonePanel.DOAnchorPosY(hiddenYPosition, tweenDuration).SetEase(Ease.InBack);
        }
        if (notificationPopup != null) notificationPopup.SetActive(false);
    }

    private void RefreshMessageHistory()
    {
        foreach (Transform child in scrollContentContainer) Destroy(child.gameObject);

        foreach (PhoneMessage msg in messageHistory)
        {
            GameObject prefabToUse = msg.isPlayer ? playerBubblePrefab : friendBubblePrefab;
            GameObject bubble = Instantiate(prefabToUse, scrollContentContainer);
            
            TextMeshProUGUI bubbleText = bubble.GetComponentInChildren<TextMeshProUGUI>();
            if (bubbleText != null) bubbleText.text = msg.text;
        }
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollContentContainer.GetComponent<RectTransform>());
        
        if (phoneScrollRect != null) phoneScrollRect.verticalNormalizedPosition = 0f;
    }
}