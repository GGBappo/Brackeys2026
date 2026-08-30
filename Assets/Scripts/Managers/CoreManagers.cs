using UnityEngine;

public class CoreManagers : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        MemoryBoard.InitializeDefaults();
    }
}
