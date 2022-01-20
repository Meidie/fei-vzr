using System;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float rockSpeed = 10f;
    
    private Rigidbody2D _rigidbody2D;
    private Player _player;
    private float _xSpeed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
        // let spravnym smerom pri otacani postavy
        _xSpeed = _player.transform.localScale.x * rockSpeed;
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}