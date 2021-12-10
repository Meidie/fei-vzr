using UnityEngine;
using UnityEngine.UI;

public class WorldPosition : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private string partTag;
    
    [SerializeField]
    private float minShowAngle;

    [SerializeField]
    private float maxShowAngle = 180f;

    [SerializeField]
    private float minShowDistance = 0.5f;

    [SerializeField]
    private float maxShowDistance = 2f;

    private GameObject _car;
    private Transform _targetTransforms;
    private RectTransform _buttonRectTransform;
    private Image _buttonImage;

    private void Awake()
    {
        _buttonRectTransform = GetComponent<RectTransform>();
        _buttonImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        PlaceObjectsOnPlane.ONPlacedObject += SetInfoPositions;
    }

    private void OnDisable()
    {
        PlaceObjectsOnPlane.ONPlacedObject -= SetInfoPositions;
    }

    private void SetInfoPositions(GameObject obj)
    {
        _car = obj;
        _targetTransforms = GameObject.FindGameObjectWithTag(partTag).transform;
    }

    private void Update()
    {
        if (_targetTransforms != null)
        {
            var position = _targetTransforms.position;
            var screenPoint = arCamera.WorldToScreenPoint(position);
            _buttonRectTransform.position = screenPoint;

            var dist = Vector3.Distance(arCamera.transform.position, position);
            var angle = Vector3.Angle(arCamera.transform.forward, _targetTransforms.forward);
            if (dist > minShowDistance && dist < maxShowDistance && (angle > minShowAngle || angle < maxShowAngle))
            {
                _buttonImage.enabled = true;
            }
            else
            {
                _buttonImage.enabled = false;
            }
        }
    }
}