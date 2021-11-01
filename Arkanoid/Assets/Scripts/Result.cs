using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text =
            $"Final Score: {GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().text.Split(' ')[1]}";
    }
}