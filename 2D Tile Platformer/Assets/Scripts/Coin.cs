using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUp;
    [SerializeField] private int pointForCoin = 10;

    private bool _collected ;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || _collected) return;
        _collected = true;
        AudioSource.PlayClipAtPoint(coinPickUp, Camera.main.transform.position, 0.2f);
        Destroy(gameObject);
        GameSession.Instance.AddScore(pointForCoin);
    }
}