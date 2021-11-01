using UnityEngine;

public class BallAutoLaunch : MonoBehaviour
{
    [SerializeField] private Platform platform;

    [SerializeField] private float xVelocityPush = 2f;
    [SerializeField] private float yVelocityPush = 12f;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float randomDirectionFactor = 0.2f;

    private bool _ballLaunched;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;

    public Platform Platform
    {
        set
        {
            if (platform == null)
            {
                platform = value;
            }
        }
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        CountBalls();
        PositionBallOnPaddle();
        LaunchBall();
    }

    private void LaunchBall()
    {
        _ballLaunched = true;
        _rigidbody2D.velocity = new Vector2(xVelocityPush, yVelocityPush);
    }

    private void PositionBallOnPaddle()
    {
        transform.position = new Vector2(platform.transform.position.x, platform.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_ballLaunched)
        {
            var velocityTweak = new Vector2(
                Random.Range(0f, randomDirectionFactor),
                Random.Range(0f, randomDirectionFactor));

            _audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            _rigidbody2D.velocity += velocityTweak;
        }
    }

    private void CountBalls()
    {
        FindObjectOfType<Level>()?.AddBall();
    }
}