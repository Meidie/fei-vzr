using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [Range(0.5f, 5f)]
    [SerializeField] 
    private float gameSpeed = 1f;

    [SerializeField] 
    private int pointsPerBlockDestroyed = 10;

    [SerializeField] 
    private TextMeshProUGUI scoreText; 
    
    private int _currentScore;
    
    private void Awake()
    {
        int gameStateCount = FindObjectsOfType<GameState>().Length;
        if (gameStateCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddScore()
    {
        _currentScore += pointsPerBlockDestroyed;
        scoreText.text = "Score: " + _currentScore;
    }

    public void ResetScore()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
