using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float screenWidthUnits = 16f;

    [SerializeField] private float minXPosition = 1f;

    public float MinXPosition
    {
        get => minXPosition;
        set => minXPosition = value;
    }

    [SerializeField] private float maxXPosition = 15f;

    public float MaxXPosition
    {
        get => maxXPosition;
        set => maxXPosition = value;
    }

    private void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(Input.mousePosition.x / Screen.width * screenWidthUnits, minXPosition, maxXPosition),
            transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Feature"))
        {
            var feature = other.GetComponent<IPowerUp>();
            feature?.ApplyPowerUp();
        }
    }
}