using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARUXReasonsManager : MonoBehaviour
{
    [SerializeField] private bool showNotTrackingReasons = true;

    public bool ShowNotTrackingReasons
    {
        get => showNotTrackingReasons;
        set => showNotTrackingReasons = value;
    }

    [SerializeField] private TMP_Text reasonDisplayText;

    public TMP_Text ReasonDisplayText
    {
        get => reasonDisplayText;
        set => reasonDisplayText = value;
    }

    [SerializeField] private GameObject reasonParent;

    public GameObject ReasonParent
    {
        get => reasonParent;
        set => reasonParent = value;
    }

    [SerializeField] private Image reasonIcon;

    public Image ReasonIcon
    {
        get => reasonIcon;
        set => reasonIcon = value;
    }

    [SerializeField] private Sprite initARSprite;

    public Sprite InitARSprite
    {
        get => initARSprite;
        set => initARSprite = value;
    }

    [SerializeField] private Sprite motionSprite;

    public Sprite MotionSprite
    {
        get => motionSprite;
        set => motionSprite = value;
    }

    [SerializeField] private Sprite lightSprite;

    public Sprite LightSprite
    {
        get => lightSprite;
        set => lightSprite = value;
    }

    [SerializeField] private Sprite featuresSprite;

    public Sprite FeaturesSprite
    {
        get => featuresSprite;
        set => featuresSprite = value;
    }

    [SerializeField] private Sprite unsupportedSprite;

    public Sprite UnsupportedSprite
    {
        get => unsupportedSprite;
        set => unsupportedSprite = value;
    }

    [SerializeField] private Sprite noneSprite;

    public Sprite NoneSprite
    {
        get => noneSprite;
        set => noneSprite = value;
    }

    NotTrackingReason _currentReason;
    bool _sessionTracking;

    private const string InitARText = "Initializing augmented reality.";
    private const string MotionText = "Try moving at a slower pace.";
    private const string LightText = "It’s too dark. Try going to a more well lit area.";
    private const string FeaturesText = "Look for more textures or details in the area.";
    private const string UnsupportedText = "AR content is not supported.";
    private const string NoneText = "Wait for tracking to begin.";

    private void OnEnable()
    {
        ARSession.stateChanged += ARSessionOnStateChanged;
        if (!showNotTrackingReasons)
        {
            reasonParent.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ARSession.stateChanged -= ARSessionOnStateChanged;
    }

    private void Update()
    {
        if (showNotTrackingReasons)
        {
            if (!_sessionTracking)
            {
                _currentReason = ARSession.notTrackingReason;
                ShowReason();
            }
            else
            {
                if (reasonDisplayText.gameObject.activeSelf)
                {
                    reasonParent.SetActive(false);
                }
            }
        }
    }

    private void ARSessionOnStateChanged(ARSessionStateChangedEventArgs obj)
    {
        _sessionTracking = obj.state == ARSessionState.SessionTracking ? true : false;
    }

    private void ShowReason()
    {
        reasonParent.SetActive(true);
        SetReason();
    }

    private void SetReason()
    {
        switch (_currentReason)
        {
            case NotTrackingReason.Initializing:
            case NotTrackingReason.Relocalizing:
                reasonDisplayText.text = InitARText;
                reasonIcon.sprite = initARSprite;
                break;
            case NotTrackingReason.ExcessiveMotion:
                reasonDisplayText.text = MotionText;
                reasonIcon.sprite = motionSprite;
                break;
            case NotTrackingReason.InsufficientLight:
                reasonDisplayText.text = LightText;
                reasonIcon.sprite = lightSprite;
                break;
            case NotTrackingReason.InsufficientFeatures:
                reasonDisplayText.text = FeaturesText;
                reasonIcon.sprite = featuresSprite;
                break;
            case NotTrackingReason.Unsupported:
                reasonDisplayText.text = UnsupportedText;
                reasonIcon.sprite = unsupportedSprite;
                break;
            case NotTrackingReason.None:
                reasonDisplayText.text = NoneText;
                reasonIcon.sprite = noneSprite;
                break;
        }
    }
}