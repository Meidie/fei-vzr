using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class DisableTrackedVisuals : MonoBehaviour
{
    [SerializeField] private ARPointCloudManager pointCloudManager;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private Sprite activeIcon;
    [SerializeField] private Sprite disabledIcon;
    private bool _areVisualsActive = true;
    public void DisableVisuals()
    {
        if (_areVisualsActive)
        {
            pointCloudManager.SetTrackablesActive(false);
            pointCloudManager.enabled = false;
            
            planeManager.SetTrackablesActive(false);
            planeManager.enabled = false;

            _areVisualsActive = false;
            gameObject.GetComponent<Image>().sprite = activeIcon;
        }
        else
        {
            pointCloudManager.SetTrackablesActive(true);
            pointCloudManager.enabled = true;
            
            planeManager.SetTrackablesActive(true);
            planeManager.enabled = true;
            
            _areVisualsActive = true;
            gameObject.GetComponent<Image>().sprite = disabledIcon;
        }
    }
}