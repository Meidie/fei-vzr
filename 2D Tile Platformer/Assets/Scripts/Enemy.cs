using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody2D.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    private void FlipEnemy()
    {
        transform.localScale = new Vector2(-Mathf.Sign(_rigidbody2D.velocity.x), 1f);
    }
}