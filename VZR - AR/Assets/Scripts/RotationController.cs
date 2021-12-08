using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Slider rotationSlider;

    private GameObject _carPrefab;

    private void Awake()
    {
        rotationSlider.onValueChanged.AddListener(RotationChanged);
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

    private void RotationChanged(float newValue)
    {
        if (_carPrefab != null)
        {
            _carPrefab.transform.localRotation = Quaternion.AngleAxis(newValue, Vector3.up);
        }
    }
}