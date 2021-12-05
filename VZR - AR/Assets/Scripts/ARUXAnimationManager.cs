using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ARUXAnimationManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Instructional test for visual UI")]
    private TMP_Text instructionText;

    public TMP_Text InstructionText
    {
        get => instructionText;
        set => instructionText = value;
    }

    [SerializeField] [Tooltip("Move device animation")]
    private VideoClip findAPlaneClip;

    public VideoClip FindAPlaneClip
    {
        get => findAPlaneClip;
        set => findAPlaneClip = value;
    }

    [SerializeField] [Tooltip("Tap to place animation")]
    private VideoClip tapToPlaceClip;

    public VideoClip TapToPlaceClip
    {
        get => tapToPlaceClip;
        set => tapToPlaceClip = value;
    }

    [SerializeField] [Tooltip("Video player reference")]
    private VideoPlayer videoPlayer;

    public VideoPlayer VideoPlayer
    {
        get => videoPlayer;
        set => videoPlayer = value;
    }

    [SerializeField] [Tooltip("Raw image used for video player reference")]
    private RawImage rawImage;

    public RawImage RawImage
    {
        get => rawImage;
        set => rawImage = value;
    }

    [SerializeField] [Tooltip("time the UI takes to fade on")]
    private float fadeOnDuration = 1.0f;

    [SerializeField] [Tooltip("time the UI takes to fade off")]
    private float fadeOffDuration = 0.5f;

    private readonly Color _alphaWhite = new Color(1, 1, 1, 0);
    private readonly Color _white = new Color(1, 1, 1, 1);

    private Color _targetColor;
    private Color _startColor;
    private Color _lerpingColor;
    private bool _fadeOn;
    private bool _fadeOff;
    private bool _tweening;
    private float _tweenTime;
    private float _tweenDuration;

    private const string MoveDeviceText = "Move Device Slowly";
    private const string TapToPlaceText = "Tap to Place AR";

    public static event Action ONFadeOffComplete;

    [SerializeField] private Texture transparent;

    public Texture Transparent
    {
        get => transparent;
        set => transparent = value;
    }

    RenderTexture m_RenderTexture;

    private void Start()
    {
        _startColor = _alphaWhite;
        _targetColor = _white;
    }

    private void Update()
    {
        if (!videoPlayer.isPrepared)
        {
            return;
        }

        if (_fadeOff || _fadeOn)
        {
            if (_fadeOn)
            {
                _startColor = _alphaWhite;
                _targetColor = _white;
                _tweenDuration = fadeOnDuration;
                _fadeOff = false;
            }

            if (_fadeOff)
            {
                _startColor = _white;
                _targetColor = _alphaWhite;
                _tweenDuration = fadeOffDuration;

                _fadeOn = false;
            }

            if (_tweenTime < 1)
            {
                _tweenTime += Time.deltaTime / _tweenDuration;
                _lerpingColor = Color.Lerp(_startColor, _targetColor, _tweenTime);
                rawImage.color = _lerpingColor;
                instructionText.color = _lerpingColor;
                _tweening = true;
            }
            else
            {
                _tweenTime = 0;
                _fadeOff = false;
                _fadeOn = false;
                _tweening = false;

                // was it a fade off?
                if (_targetColor == _alphaWhite)
                {
                    ONFadeOffComplete?.Invoke();
                    m_RenderTexture = videoPlayer.targetTexture;
                    m_RenderTexture.DiscardContents();
                    m_RenderTexture.Release();
                    Graphics.Blit(transparent, m_RenderTexture);
                }
            }
        }
    }

    public void ShowTapToPlace()
    {
        videoPlayer.clip = tapToPlaceClip;
        videoPlayer.Play();
        instructionText.text = TapToPlaceText;
        _fadeOn = true;
    }

    public void ShowCrossPlatformFindAPlane()
    {
        videoPlayer.clip = findAPlaneClip;
        videoPlayer.Play();
        instructionText.text = MoveDeviceText;
        _fadeOn = true;
    }

    public void FadeOffCurrentUI()
    {
        if (videoPlayer.clip != null)
        {
            if (_tweening || _fadeOn)
            {
                // stop tween immediately
                _tweenTime = 1.0f;
                rawImage.color = _alphaWhite;
                instructionText.color = _alphaWhite;
                ONFadeOffComplete?.Invoke();
            }

            _fadeOff = true;
        }
    }
}