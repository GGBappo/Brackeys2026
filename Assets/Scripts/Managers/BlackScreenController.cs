using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BlackScreenController : MonoBehaviour
{
    public static BlackScreenController Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("A full-screen black image covering everything")]
    public CanvasGroup blackScreenGroup;
    public TextMeshProUGUI cutsceneText;

    [Header("Tween Settings")]
    public float fadeDuration = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        blackScreenGroup.alpha = 0f;
        blackScreenGroup.blocksRaycasts = false;
        cutsceneText.text = "";
    }

    public void PlaySequence(string text, string sfxName, float holdTime)
    {
        StartCoroutine(SequenceRoutine(text, sfxName, holdTime));
    }

    private IEnumerator SequenceRoutine(string text, string sfxName, float holdTime)
    {
        GameEvents.GlobalStateChanged(GlobalStateType.Dialogue); 
        blackScreenGroup.blocksRaycasts = true;
        cutsceneText.text = "";

        yield return blackScreenGroup.DOFade(1f, fadeDuration).WaitForCompletion();

        cutsceneText.text = text;
        if (!string.IsNullOrEmpty(sfxName)) GameEvents.RequestPlaySFX(sfxName);

        if (holdTime <= 0f) yield break; 

        yield return new WaitForSeconds(holdTime);
        ClearBlackScreen();
    }

    public void ClearBlackScreen()
    {
        cutsceneText.text = "";
        blackScreenGroup.DOFade(0f, fadeDuration).OnComplete(() => 
        {
            blackScreenGroup.blocksRaycasts = false;
            GameEvents.GlobalStateChanged(GlobalStateType.Active);
        });
    }
}