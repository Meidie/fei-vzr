using UnityEngine;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    [SerializeField]
    private Slider scaleSlider;

    private GameObject _carPrefab;

    private void Awake()
    {
        scaleSlider.onValueChanged.AddListener(ScaleChanged);
    }
    
    private void OnEnable()
    {
        PlaceObjectsOnPlane.ONPlacedObject += SetPrefab;
    }

    private void OnDisable()
    {
        PlaceObjectsOnPlane.ONPlacedObject -= SetPrefab;
    }

    private void SetPrefab(GameObject go)
    {
        _carPrefab = go;
    }

    private void ScaleChanged(float newValue)
    {
        if (_carPrefab != null)
        {
            _carPrefab.transform.localScale = Vector3.one * newValue;
        }
    }
}
