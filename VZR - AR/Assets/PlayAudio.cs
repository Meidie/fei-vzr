using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
