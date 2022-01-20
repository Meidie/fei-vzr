using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private float moveRate = 0.2f;
    [SerializeField] private  float maxHeight = float.MaxValue;

    private void Update()
    {
        if (transform.position.y < maxHeight)
        {
            transform.Translate(new Vector2(0f, moveRate * Time.deltaTime));
        }
    }
}