using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance.
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private AudioSource musicSource;

    [Range(0f, 1f)] [SerializeField] private float volume;

    // Initialize the singleton instance.
    private void Awake()
    {
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        // If there is not already an instance of AudioManager, set it to this.
        else
        {
            Instance = this;
            //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
            DontDestroyOnLoad(gameObject);
        }
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        var randomIndex = Random.Range(0, clips.Length);

        musicSource.volume = volume;
        musicSource.clip = clips[randomIndex];
        musicSource.Play();
    }
}