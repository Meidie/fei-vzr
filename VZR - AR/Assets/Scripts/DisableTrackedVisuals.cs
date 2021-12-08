using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisableTrackedVisuals : MonoBehaviour
{
    [SerializeField] [Tooltip("Disables spawned feature points and the ARPointCloudManager")]
    private bool disableFeaturePoints;

    public bool DisableFeaturePoints
    {
        get => disableFeaturePoints;
        set => disableFeaturePoints = value;
    }

    [SerializeField] [Tooltip("Disables spawned planes and ARPlaneManager")]
    private bool disablePlaneRendering;

    public bool DisablePlaneRendering
    {
        get => disablePlaneRendering;
        set => disablePlaneRendering = value;
    }

    [SerializeField] private ARPointCloudManager pointCloudManager;

    public ARPointCloudManager PointCloudManager
    {
        get => pointCloudManager;
        set => pointCloudManager = value;
    }

    [SerializeField] private ARPlaneManager planeManager;

    public ARPlaneManager PlaneManager
    {
        get => planeManager;
        set => planeManager = value;
    }

    private void OnEnable()
    {
        PlaceObjectsOnPlane.ONPlacedObject += ONPlacedObject;
    }

    private void OnDisable()
    {
        PlaceObjectsOnPlane.ONPlacedObject -= ONPlacedObject;
    }

    private void ONPlacedObject(GameObject go)
    {
        if (disableFeaturePoints)
        {
            pointCloudManager.SetTrackablesActive(false);
            pointCloudManager.enabled = false;
        }

        if (disablePlaneRendering)
        {
            planeManager.SetTrackablesActive(false);
            planeManager.enabled = false;
        }
    }
}