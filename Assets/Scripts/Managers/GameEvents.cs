using System;
using UnityEngine;

public static class GameEvents
{
    // state events
    public static event Action<GlobalStateType> OnGlobalStateChanged; // (newState)

    // scene/level events
    public static event Action<RuntimeDialogueGraph, string> OnRequestDialogueStart;
    public static event Action OnRequestDialogueEnd;

    /// UI events
    public static event Action OnStartButtonPressed; // please note this is to be depricated soon until i refactor the code to use the state manager more
    public static event Action OnDialogueButtonPressed; // THIS EVENT IS HERE IF NEED BE, IT IS CURRENTLY NOT REFERENCED BY ANYTHING
    public static event Action OnRequestSettingsMenuOpen;
    public static event Action OnRequestSettingsMenuClose;
    public static event Action OnRequestShowDialogueUI;
    public static event Action OnRequestHideDialogueUI;
    public static event Action<float, CanvasGroup, Canvas> OnFadeOutUIElementRequested; // (duration, canvasGroup, canvas)
    public static event Action<float, CanvasGroup, Canvas> OnFadeInUIElementRequested; // (duration, canvasGroup, canvas)
    public static event Action<DialogueBoxPosition> OnDialogueBoxMove;
    public static event Action<string> OnPingObjectToHighlight;
    public static event Action<string> OnPingObjectToUnhighlight;

    // camera events
    public static event Action<Vector3, Quaternion, float, Vector3?, float?> OnCameraMoveRequest; // (position, rotation, duration, lookAtMarker, FOV)
    public static event Action<Vector3, float, float> OnCameraLookAtRequest; // (targetPosition, duration, FOV)
    public static event Action<GameObject, float, float> OnCameraLookAtGameObjectRequest; // (targetGameObject, duration, FOV)
    public static event Action<float, bool, float> OnCameraFOVChangeRequest; // (newFOV, slowZoom, duration)
    
    // Start Screen events
    public static event Action OnRequestNPCInteractionSequence; // (no parameters)
    public static event Action OnRequestNPCInteractionSequenceExit; // (no parameters)

    // Audio events
    public static event Action<string> OnRequestPlaySFX;
    public static event Action<string> OnRequestPlayMusic;
    public static event Action<string> OnRequestPlayAmbient;
    public static event Action<string> OnRequestStopSFX;
    public static event Action<string> OnRequestStopMusic;
    public static event Action<string> OnRequestStopAmbient;

    // dialogue
    public static event Action OnDialogueSequenceCompleted;

    #region Audio Calls
    public static void RequestPlaySFX(string clipName)
    {
        Debug.Log($"[GameEvents] Requesting SFX: {clipName ?? "<null>"}");
        OnRequestPlaySFX?.Invoke(clipName);
    }

    public static void RequestPlayMusic(string clipName)
    {
        Debug.Log($"[GameEvents] Requesting music: {clipName ?? "<null>"}");
        OnRequestPlayMusic?.Invoke(clipName);
    }

    public static void RequestPlayAmbient(string clipName)
    {
        Debug.Log($"[GameEvents] Requesting ambient: {clipName ?? "<null>"}");
        OnRequestPlayAmbient?.Invoke(clipName);
    }

    public static void RequestStopSFX(string clipName)
    {
        Debug.Log($"[GameEvents] Requesting stop SFX: {clipName ?? "<null>"}");
        OnRequestStopSFX?.Invoke(clipName);
    }

    public static void RequestStopMusic(string clipName)
    {
        Debug.Log($"[GameEvents] Requesting stop music: {clipName ?? "<null>"}");
        OnRequestStopMusic?.Invoke(clipName);
    }

    public static void RequestStopAmbient(string clipName)
    {
        Debug.Log($"[GameEvents] Requesting stop ambient: {clipName ?? "<null>"}");
        OnRequestStopAmbient?.Invoke(clipName);
    }
    #endregion

    #region State Calls
    /// <summary>
    /// Invoke the OnGlobalStateChanged event to notify the state manager that the global state has changed
    /// </summary>
    /// <param name="newState">The new global state.</param>
    public static void GlobalStateChanged(GlobalStateType newState)
    {
        OnGlobalStateChanged?.Invoke(newState);
    }
    #endregion

    #region Scene/Level Calls
    
    #endregion

    #region UI Calls
    /// <summary>
    /// Invoke the OnStartButtonPressed event to notify subscribers that the start button was pressed.
    /// </summary>
    public static void StartButtonPressed()
    {
        OnStartButtonPressed?.Invoke();
        Debug.Log("[GameEvents] OnStartButtonPressed invoked");
    }

    /// <summary>
    /// Invoke the OnDialogueButtonPressed event to notify subscribers that the dialogue button was pressed.
    /// </summary>
    public static void DialougeButtonPressed() // temporarily placed here if need be.
    {
        OnDialogueButtonPressed?.Invoke();
        Debug.Log("[GameEvents] OnDialougeButtonPressed invoked");
    }

    /// <summary>
    /// Invoke the OnRequestDialogueStart event to request that dialogue begin at the specified node.
    /// </summary>
    /// <param name="dialogueGraph">The dialogue graph to start from.</param>
    /// <param name="nodeID">The node ID to start from, or null to use the entry node.</param>
    public static void RequestDialogueStart(RuntimeDialogueGraph dialogueGraph, string nodeID = null)
    {
        Debug.Log($"[GameEvents] Requesting dialogue start: {(dialogueGraph != null ? dialogueGraph.name : "<no graph>")} / {nodeID ?? "<entry>"}");
        OnRequestDialogueStart?.Invoke(dialogueGraph, nodeID);
    }

    /// <summary>
    /// Invoke the OnRequestDialogueEnd event to request that the current dialogue end.
    /// </summary>
    public static void RequestDialogueEnd()
    {
        Debug.Log("[GameEvents] Requesting dialogue end");
        OnRequestDialogueEnd?.Invoke();
    }

    /// <summary>
    /// Invoke the OnFadeOutUIElementRequested event to request that a UI element fade out.
    /// </summary>
    /// <param name="duration">The fade duration in seconds.</param>
    /// <param name="canvasGroup">The CanvasGroup to fade out, if any.</param>
    /// <param name="canvas">The Canvas to fade out, if any.</param>
    public static void RequestFadeOutUIElement(float duration, CanvasGroup canvasGroup = null, Canvas canvas = null)
    {
        Debug.Log($"[GameEvents] Requesting fade out of UI element: {canvasGroup?.name ?? canvas?.name} over duration: {duration}");
        OnFadeOutUIElementRequested?.Invoke(duration, canvasGroup, canvas);
    }

    /// <summary>
    /// Invoke the OnFadeInUIElementRequested event to request that a UI element fade in.
    /// </summary>
    /// <param name="duration">The fade duration in seconds.</param>
    /// <param name="canvasGroup">The CanvasGroup to fade in, if any.</param>
    /// <param name="canvas">The Canvas to fade in, if any.</param>
    public static void RequestFadeInUIElement(float duration, CanvasGroup canvasGroup = null, Canvas canvas = null)
    {
        Debug.Log($"[GameEvents] Requesting fade in of UI element: {canvasGroup?.name ?? canvas?.name} over duration: {duration}");
        OnFadeInUIElementRequested?.Invoke(duration, canvasGroup, canvas);
    }

    /// <summary>
    /// Invoke the OnRequestSettingsMenuOpen event to request that the settings menu open.
    /// </summary>
    public static void RequestSettingsMenuOpen()
    {
        Debug.Log("[GameEvents] Requesting to open Settings Menu");
        OnRequestSettingsMenuOpen?.Invoke();
    }

    /// <summary>
    /// Invoke the OnRequestSettingsMenuClose event to request that the settings menu close.
    /// </summary>
    public static void RequestSettingsMenuClose()
    {
        Debug.Log("[GameEvents] Requesting to close Settings Menu");
        OnRequestSettingsMenuClose?.Invoke();
    }

    /// <summary>
    /// Invoke the OnRequestShowDialogueUI event to request that the dialogue UI be shown.
    /// </summary>
    public static void RequestShowDialogueUI()
    {
        Debug.Log("[GameEvents] Requesting to show Dialogue UI");
        OnRequestShowDialogueUI?.Invoke();
    }

    /// <summary>
    /// Invoke the OnRequestHideDialogueUI event to request that the dialogue UI be hidden.
    /// </summary>
    public static void RequestHideDialogueUI()
    {
        Debug.Log("[GameEvents] Requesting to hide Dialogue UI");
        OnRequestHideDialogueUI?.Invoke();
    }
    
    /// <summary>
    /// Invoke the OnDialogueBoxMove event to request that the dialogue box move to a new position.
    /// </summary>
    /// <param name="dialogueBoxPosition">The target position for the dialogue box.</param>
    public static void RequestDialogueBoxMove(DialogueBoxPosition dialogueBoxPosition)
    {
        Debug.Log($"[GameEvents] Requesting to move Dialogue box to {dialogueBoxPosition} position");
        OnDialogueBoxMove?.Invoke(dialogueBoxPosition);
    }

    /// <summary>
    /// Invoke the OnPingObjectToHighlight event to request that an object be highlighted.
    /// </summary>
    /// <param name="objectID">The object ID to highlight.</param>
    public static void PingObjectToHightlight(string objectID)
    {
        Debug.Log($"[GameEvents] Pinging object with object ID '{objectID}' to highlight");
        OnPingObjectToHighlight?.Invoke(objectID);
    }

    /// <summary>
    /// Invoke the OnPingObjectToUnhighlight event to request that an object be unhighlighted.
    /// </summary>
    /// <param name="objectID">The object ID to unhighlight.</param>
    public static void PingObjectToUnhighlight(string objectID)
    {
        Debug.Log($"[GameEvents] Pinging object with object ID '{objectID}' to unhighlight");
        OnPingObjectToUnhighlight?.Invoke(objectID);
    }

    #endregion

    #region Camera Calls

    /// <summary>
    /// Invoke the OnCameraMoveRequest event to request that the camera move to a new position and rotation.
    /// </summary>
    /// <param name="position">The target position.</param>
    /// <param name="rotation">The target rotation.</param>
    /// <param name="duration">The movement duration in seconds.</param>
    /// <param name="lookAtMarker">Optional marker for the camera to look at.</param>
    /// <param name="FOV">Optional field of view to apply during the move.</param>
    public static void RequestCameraMove(Vector3 position, Quaternion rotation, float duration, Vector3? lookAtMarker = null, float? FOV = null)
    {
        Debug.Log($"[GameEvents] Requesting camera move to position: {position}, rotation: {rotation}, duration: {duration}");
        OnCameraMoveRequest?.Invoke(position, rotation, duration, lookAtMarker, FOV);
    }

    /// <summary>
    /// Invoke the OnCameraLookAtRequest event to request that the camera look at a world position.
    /// </summary>
    /// <param name="targetPosition">The position for the camera to look at.</param>
    /// <param name="duration">The look-at duration in seconds.</param>
    /// <param name="FOV">The field of view to use while looking at the target.</param>
    public static void RequestCameraLookAt(Vector3 targetPosition, float duration, float FOV = 50f)
    {
        Debug.Log($"[GameEvents] Requesting camera to look at position: {targetPosition}, duration: {duration}, FOV: {FOV}");
        OnCameraLookAtRequest?.Invoke(targetPosition, duration, FOV);
    }

    /// <summary>
    /// Invoke the OnCameraLookAtGameObjectRequest event to request that the camera look at a GameObject.
    /// </summary>
    /// <param name="target">The target GameObject.</param>
    /// <param name="duration">The look-at duration in seconds.</param>
    /// <param name="FOV">The field of view to use while looking at the target.</param>
    public static void RequestCameraLookAt(GameObject target, float duration, float FOV = 50f)
    {
        Debug.Log($"[GameEvents] Requesting camera to look at GameObject: {target.name}, duration: {duration}, FOV: {FOV}");
        OnCameraLookAtGameObjectRequest?.Invoke(target, duration, FOV);
    }

    /// <summary>
    /// Invoke the OnCameraFOVChangeRequest event to request a change to the camera field of view.
    /// </summary>
    /// <param name="FOV">The target field of view.</param>
    /// <param name="slowZoom">Whether the change should be treated as a slow zoom.</param>
    /// <param name="duration">The change duration in seconds.</param>
    public static void RequestCameraFOVChange(float FOV, bool slowZoom = false, float duration = 1f)
    {
        Debug.Log($"[GameEvents] Requesting camera FOV change to: {FOV}, slowZoom: {slowZoom}, duration: {duration}");
        OnCameraFOVChangeRequest?.Invoke(FOV, slowZoom, duration);
    }
    #endregion

    #region Start Screen Calls
    /// <summary>
    /// Invoke the OnRequestNPCInteractionSequence event to request the NPC interaction sequence.
    /// </summary>
    public static void RequestNPCInteractionSequence()
    {
        Debug.Log("[GameEvents] Requesting NPC interaction sequence");
        OnRequestNPCInteractionSequence?.Invoke();
    }

    /// <summary>
    /// Invoke the OnRequestNPCInteractionSequenceExit event to request the exit of the NPC interaction sequence.
    /// </summary>
    public static void RequestNPCInteractionSequenceExit()
    {
        Debug.Log("[GameEvents] Requesting exit of NPC interaction sequence");
        OnRequestNPCInteractionSequenceExit?.Invoke();
    }
    #endregion

    #region Dialogue Calls
    public static void DialogueSequenceCompleted()
    {
        Debug.Log("[GameEvents] Dialogue sequence completed");
        OnDialogueSequenceCompleted?.Invoke();
    }
    #endregion
}