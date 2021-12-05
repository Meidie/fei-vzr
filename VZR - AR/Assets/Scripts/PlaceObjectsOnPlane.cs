using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField] [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    private GameObject placedPrefab;

    public GameObject PlacedPrefab
    {
        get => placedPrefab;
        set => placedPrefab = value;
    }

    public GameObject spawnedObject { get; private set; }

    public static event Action ONPlacedObject;

    private ARRaycastManager _raycastManager;

    private static readonly List<ARRaycastHit> Hits = new List<ARRaycastHit>();

    [SerializeField] private int maxNumberOfObjectsToPlace = 1;

    private int _numberOfPlacedObjects;

    [SerializeField] private bool canReposition = true;

    public bool CanReposition
    {
        get => canReposition;
        set => canReposition = value;
    }

    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (_raycastManager.Raycast(touch.position, Hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = Hits[0].pose;

                    if (_numberOfPlacedObjects < maxNumberOfObjectsToPlace)
                    {
                        spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

                        _numberOfPlacedObjects++;
                    }
                    else
                    {
                        if (canReposition)
                        {
                            spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        }
                    }

                    ONPlacedObject?.Invoke();
                }
            }
        }
    }
}