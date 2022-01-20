using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetPersist()
    {
        Destroy(gameObject);
    }
}