using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float screenWidthUnits = 16f;

    [SerializeField] 
    private float minXPosition = 1f;

    [SerializeField] 
    private float maxXPosition = 15f;

    private void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(Input.mousePosition.x / Screen.width * screenWidthUnits, minXPosition, maxXPosition),
            transform.position.y);
    }
}
