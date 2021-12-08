using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    public static event Action<GameObject> ONPlacedObject;

    [SerializeField] private GameObject placementPrefab;

    public GameObject PlacementPrefab
    {
        get => placementPrefab;
        set => placementPrefab = value;
    }

    public GameObject spawnedObject { get; private set; }

    [SerializeField] private Camera arCamera;

    private ARRaycastManager _raycastManager;
    private Vector2 _touchPosition;
    private bool _onTouchHold;

    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            _touchPosition = touch.position;
 
            switch (touch.phase)
            {
                case TouchPhase.Began:
                {
                    var ray = arCamera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out var hitObject))
                    {
                        if (hitObject.transform.gameObject.CompareTag("car"))
                        {
                            _onTouchHold = true;
                        }
                    }

                    break;
                }
                case TouchPhase.Ended:
                    _onTouchHold = false;
                    break;
            }
            
            if (_raycastManager.Raycast(_touchPosition, Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(placementPrefab, hitPose.position, hitPose.rotation);
                    ONPlacedObject?.Invoke(spawnedObject);
                }
                else
                {
                    if (_onTouchHold)
                    {
                        spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                    }
                }
            }
        }
    }
}