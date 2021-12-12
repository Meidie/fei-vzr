using UnityEngine;
using UnityEngine.UI;

public class InfoPointer : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private string partTag;
    [SerializeField] private GameObject detailPanel;
    
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
    private Button _button;
    private bool _isDetailVisible;

    private void Awake()
    {
        _buttonRectTransform = GetComponent<RectTransform>();
        _buttonImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }
    
    public void SetInfoPositions()
    {
        _targetTransforms = GameObject.FindGameObjectWithTag(partTag).transform;
    }

    public void ShowDetail()
    {
        if (!_isDetailVisible)
        {
            detailPanel.SetActive(true);
            _isDetailVisible = true;
        }
        else
        {
            detailPanel.SetActive(false);
            _isDetailVisible = false;
        }
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
            //Debug.Log(angle);
            if (dist > minShowDistance && dist < maxShowDistance && angle >= minShowAngle && angle <= maxShowAngle)
            {
                _buttonImage.enabled = true;
                _button.enabled = true;
                
                if (detailPanel != null && _isDetailVisible)
                {
                    detailPanel.SetActive(true);
                }
            }
            else
            {
                _buttonImage.enabled = false;
                _button.enabled = false;

                if (detailPanel != null)
                {
                    detailPanel.SetActive(false);
                }
            }
        }
    }
}