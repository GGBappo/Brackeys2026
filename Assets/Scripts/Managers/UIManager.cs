using UnityEngine;
using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup _loadingScreen;

    [Header("Scene Transition Speed")]
    [SerializeField] private float _globalTransitionSpeed = 1f;

    private void OnEnable() {
        GameEvents.OnFadeOutUIElementRequested += FadeOutUIElement;
        GameEvents.OnFadeInUIElementRequested += FadeInUIElement;
    }

    private void OnDisable() {
        GameEvents.OnFadeOutUIElementRequested -= FadeOutUIElement;
        GameEvents.OnFadeInUIElementRequested -= FadeInUIElement;
    }

    private void FadeInUIElement(float duration, CanvasGroup canvasGroup = null, Canvas canvas = null)
    {
        if (canvasGroup == null && canvas == null)
        {
            Debug.LogWarning("[UIManager] No CanvasGroup or Canvas provided for fade in.");
            return;
        }

        CanvasGroup cg = canvasGroup;
        if (cg == null)
        {
            cg = canvas.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = canvas.gameObject.AddComponent<CanvasGroup>();
            }
        }

        if (canvas != null) 
        {
            canvas.enabled = true; 
        }
        cg.gameObject.SetActive(true);
        cg.DOFade(1f, duration).OnComplete(() => 
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        });
    }

    private void FadeOutUIElement(float duration, CanvasGroup canvasGroup = null, Canvas canvas = null)
    {
        if (canvasGroup == null && canvas == null)
        {
            Debug.LogWarning("[UIManager] No CanvasGroup or Canvas provided for fade out.");
            return;
        }

        CanvasGroup cg = canvasGroup;
        if (cg == null)
        {
            cg = canvas.GetComponent<CanvasGroup>();
            if (cg == null)
            {
                cg = canvas.gameObject.AddComponent<CanvasGroup>();
            }
        }

        // disable interaction and raycasting before starting the fade
        cg.interactable = false;
        cg.blocksRaycasts = false;

        cg.DOFade(0f, duration).OnComplete(() => 
        {
            cg.gameObject.SetActive(false);
        });
    }
}