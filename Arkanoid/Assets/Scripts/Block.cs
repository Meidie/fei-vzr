using PowerUps;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private GameObject contactParticles;
    [SerializeField] private Sprite[] hitSprites;

    private Level _level;
    private GameState _gameState;
    private SpriteRenderer _spriteRenderer;
    private PowerUpsManager _powerUpsManager;

    private int _maxNumberOfHits;
    private int _numberOfHitsReceived;

    private void Start()
    {
        _maxNumberOfHits = hitSprites.Length + 1;
        _gameState = FindObjectOfType<GameState>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _powerUpsManager = FindObjectOfType<PowerUpsManager>();
        CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CompareTag("Breakable"))
        {
            ProcessHit();
        }
    }

    private void ProcessHit()
    {
        _numberOfHitsReceived++;
        if (_numberOfHitsReceived >= _maxNumberOfHits)
        {
            SpawnPowerUp();
            DestroyBlock();
        }
        else
        {
            ChangeHitSprite();
        }
    }

    private void ChangeHitSprite()
    {
        _spriteRenderer.sprite = hitSprites[_numberOfHitsReceived - 1];
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySound();
        SpawnParticleEffect();
        _gameState.AddScore();
        Destroy(gameObject);
        _level.BlockDestroyed();
    }

    private void CountBreakableBlocks()
    {
        _level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            _level.AddBreakableBlock();
        }
    }

    private void PlayBlockDestroySound()
    {
        if (Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
        }
    }

    private void SpawnParticleEffect()
    {
        Destroy(Instantiate(contactParticles, transform.position, transform.rotation), 2f);
    }

    private void SpawnPowerUp()
    {
        _powerUpsManager.SpawnPowerUp(gameObject.transform.position);
    }
}