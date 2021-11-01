using UnityEngine;

public class Gravity : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -3);
    }
}
