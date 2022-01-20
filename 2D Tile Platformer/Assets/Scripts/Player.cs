using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Animation parameters
    private static readonly int DieParam = Animator.StringToHash("Die");
    private static readonly int RunParam = Animator.StringToHash("Run");
    private static readonly int JumpParam = Animator.StringToHash("Jump");
    private static readonly int ClimbParam = Animator.StringToHash("Climb");
    private static readonly int ThrowParam = Animator.StringToHash("Throw");
    
    [SerializeField] private GameObject rock;
    [SerializeField] private Transform rockSpawn;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private bool throwingEnabled = true;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip bounceClip;
    [SerializeField] private AudioClip throwClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip ladderClip;

    private Vector2 _moveInput;
    private Animator _animator;
    private float _gravityScale;
    private bool _isAlive = true;
    private bool _isPlayingLadderClip;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private CapsuleCollider2D _playerCollider2D;
    private BoxCollider2D _playerFeetCollider2D;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _playerCollider2D = GetComponent<CapsuleCollider2D>();
        _playerFeetCollider2D = GetComponent<BoxCollider2D>();
        _gravityScale = _rigidbody2D.gravityScale;
    }

    private void Update()
    {
        if (_isAlive)
        {
            Run();
            FlipPlayer();
            ClimbLadder();
            Die();
            Bounce();
        }
    }
    
    /// <summary>
    /// Metóda, ktorá sa zavolá pri stlačení tlačidla namapovaného cez inputSystem na pohyb
    /// </summary>
    private void OnMove(InputValue value)
    {
        if (_isAlive)
        {
            _moveInput = value.Get<Vector2>();
        }
    }

    /// <summary>
    /// Metóda, ktorá sa zavolá pri stlačení tlačidla namapovaného cez inputSystem na skok
    /// </summary>
    private void OnJump(InputValue value)
    {
        if (!_isAlive || !_playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            !value.isPressed) return;
        _animator.SetTrigger(JumpParam);
        _audioSource.PlayOneShot(jumpClip);
        _rigidbody2D.velocity += new Vector2(0f, jumpSpeed);
    }

    /// <summary>
    /// Metóda, ktorá sa zavolá pri stlačení tlačidla namapovaného cez inputSystem na fire
    /// </summary>
    private void OnFire(InputValue value)
    {
        if (_isAlive && throwingEnabled)
        {
            _audioSource.PlayOneShot(throwClip, 0.2f);
            _animator.SetTrigger(ThrowParam);
            Invoke(nameof(ThrowRock), 0.3f);
        }
    }

    private void ThrowRock()
    {
        Instantiate(rock, rockSpawn.position, transform.rotation);
    }

    private void Run()
    {
        _rigidbody2D.velocity = new Vector2(_moveInput.x * runSpeed, _rigidbody2D.velocity.y);
        _animator.SetBool(RunParam, IsPlayerMovingHorizontally());
    }

    private void FlipPlayer()
    {
        if (IsPlayerMovingHorizontally())
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {
        if (!_playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            _rigidbody2D.gravityScale = _gravityScale;
            _animator.SetBool(ClimbParam, false);

            if (_isPlayingLadderClip)
            {
                ResetAudioSource();
                _isPlayingLadderClip = false;
            }
            
            return;
        } 

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _moveInput.y * climbSpeed);
        _rigidbody2D.gravityScale = 0f;

        var isMoving = IsPlayerMovingVertically();

        if (isMoving)
        {
            if (!_isPlayingLadderClip)
            {
                _audioSource.clip = ladderClip;
                _audioSource.loop = true;
                _audioSource.Play();
                _isPlayingLadderClip = true;
            }
        }
        else
        {
            ResetAudioSource();
            _isPlayingLadderClip = false;
        }

        _animator.SetBool(ClimbParam, isMoving);
    }
    
    private void Bounce()
    {
        if ( _playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Bounce")))
        {
            _audioSource.PlayOneShot(bounceClip, 0.2f);
        }
    }

    private void Die()
    {
        if (_playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")) ||
            _playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask( "Water")))
        {
            _audioSource.PlayOneShot(deathClip);
            _isAlive = false;
            _animator.SetTrigger(DieParam);
            StartCoroutine(Reset());
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        GameSession.Instance.PlayerDied();
    }

    private void ResetAudioSource()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
        _audioSource.loop = false;
    }
    private bool IsPlayerMovingHorizontally() => Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
    
    private bool IsPlayerMovingVertically() => Mathf.Abs(_rigidbody2D.velocity.y) > Mathf.Epsilon;
}