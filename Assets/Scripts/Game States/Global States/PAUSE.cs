using UnityEngine;

public class PAUSE : IGameState
{
    public void EnterState()
    {
        Debug.Log("[GAME STATE] entered PAUSE state.");
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
        Debug.Log("[GAME STATE] exiting PAUSE state.");
    }
}