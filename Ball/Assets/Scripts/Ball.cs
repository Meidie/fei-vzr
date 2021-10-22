using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private Paddle paddle;

    [SerializeField] 
    private float xVelocityPush = 2f;

    [SerializeField] 
    private float yVelocityPush = 15f;

    private Vector2 _paddleToBallDistance;
    private bool _ballLaunched = false;
    
    private void Start()
    {
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocityPush, yVelocityPush);
        }
    }

    private void PositionBallOnPaddle()
    {
        var paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePosition + _paddleToBallDistance;
    }
}
