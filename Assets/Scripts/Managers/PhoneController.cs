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

    [Header("Prefabs")]
    public GameObject friendBubblePrefab;
    public GameObject playerBubblePrefab;

    [Header("Tween Settings")]
    public float hiddenYPosition = -1200f;
    public float visibleYPosition = 0f;
    public float tweenDuration = 0.4f;
    public KeyCode toggleKey = KeyCode.Tab;

    [HideInInspector] public bool isPhoneOpen = false;
    [HideInInspector] public List<PhoneMessage> messageHistory = new List<PhoneMessage>();

    private void Start()
    {
        // Snap phone off-screen on start
        phonePanel.anchoredPosition = new Vector2(phonePanel.anchoredPosition.x, hiddenYPosition);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            TogglePhone();
        }
    }

    public void TogglePhone()
    {
        isPhoneOpen = !isPhoneOpen;

        if (isPhoneOpen)
        {
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
        // incase we get dupes
        if (messageHistory.Count > 0 && messageHistory[messageHistory.Count - 1].text == text) return;

        messageHistory.Add(new PhoneMessage { text = text, isPlayer = isPlayer });
        
        if (isPhoneOpen) RefreshMessageHistory();
    }

    private void RefreshMessageHistory()
    {
        foreach (Transform child in scrollContentContainer)
        {
            Destroy(child.gameObject);
        }

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