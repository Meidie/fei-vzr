using UnityEngine;

public class Audio : MonoBehaviour
{
    private void Awake()
    {
        var audioSources = GameObject.FindGameObjectsWithTag("GameMusic");
        if (audioSources.Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
