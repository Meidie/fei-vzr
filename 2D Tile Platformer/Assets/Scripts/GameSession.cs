using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    [SerializeField] private int numberOfLives;
    [SerializeField] private TextMeshProUGUI livesText;
    
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = numberOfLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void PlayerDied()
    {
        if (numberOfLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetSession();
        }
    }

    private void TakeLife()
    {
        numberOfLives--;
        livesText.text = numberOfLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void ResetSession()
    {
        ScenePersist.Instance.ResetPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}