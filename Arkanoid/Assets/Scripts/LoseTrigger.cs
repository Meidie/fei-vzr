using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    private Level _level;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            var ballsLeft = _level.DestroyBall();
            if (ballsLeft <= 0)
            {
                SceneManager.LoadScene("Game Over");
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}