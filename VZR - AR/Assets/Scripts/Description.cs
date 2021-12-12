using UnityEngine;

public class Description : MonoBehaviour
{
    [SerializeField] private GameObject descriptionPanel;

    private bool _isDescriptionPanelActive;

    public void ShowDescription()
    {
        if (!_isDescriptionPanelActive)
        {
            descriptionPanel.SetActive(true);
            _isDescriptionPanelActive = true;
        }
        else
        {
            descriptionPanel.SetActive(false);
            _isDescriptionPanelActive = false;
        }
    }
}