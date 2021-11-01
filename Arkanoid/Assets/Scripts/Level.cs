using UnityEngine;

public class Level : MonoBehaviour
{
    private int _breakableBlocks;
    private int _balls = 1;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void AddBreakableBlock()
    {
        _breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        _breakableBlocks--;
        if (_breakableBlocks == 0)
        {
            StartCoroutine(_sceneLoader.LoadNextScene(0.1f));
        }
    }

    public void AddBall()
    {
        _balls++;
    }

    public int DestroyBall()
    {
        return --_balls;
    }
}