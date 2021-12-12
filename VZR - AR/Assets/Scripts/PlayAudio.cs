using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private bool _isMusicPlaying;
    
    public void PlayMusic()
    {
        if (!_isMusicPlaying)
        {
            GetComponent<AudioSource>().Play();
            _isMusicPlaying = true;
        }
        else
        {
            GetComponent<AudioSource>().Stop();
            _isMusicPlaying = false;
        }
    }
}
