using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private float xVelocityPush = 2f;
    [SerializeField] private float yVelocityPush = 15f;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float randomDirectionFactor = 0.2f;

    private Vector2 _paddleToBallDistance;
    private bool _ballLaunched;
    private Rigidbody2D _rigidbody2D;

    private AudioSource _audioSource;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _paddleToBallDistance = transform.position - paddle.transform.position;
    }

    private void Update()
    {
        if (!_ballLaunched)
        {
            PositionBallOnPaddle();
            LaunchBallOnClick();
        }
    }

    private void LaunchBallOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ballLaunched = true;
            _rigidbody2D.velocity = new Vector2(xVelocityPush, yVelocityPush);
        }
    }

    private void PositionBallOnPaddle()
    {
        var paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + _paddleToBallDistance;
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
}