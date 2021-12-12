using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> infosPointers;
    
    [SerializeField]
    private GameObject description;
    
    private bool _areInfoPointersActive;
    private bool _isDescriptionActive;
    private void Awake()
    {
        foreach (var pointer in infosPointers)
        {
            pointer.SetActive(false);
        }
    }

    public void EnableInfoPointers()
    {
        if (!_areInfoPointersActive)
        {
            foreach (var pointer in infosPointers)
            {
                pointer.SetActive(true);
                pointer.GetComponent<InfoPointer>().SetInfoPositions();
            }

            _areInfoPointersActive = true;
        } else
        {
            foreach (var pointer in infosPointers)
            {
                pointer.SetActive(false);
            }
            
            _areInfoPointersActive = false;
        }
    }

    public void EnableDescription()
    {
        if (!_isDescriptionActive)
        {
            description.SetActive(true);
            _isDescriptionActive = true;
        }
        else
        {
            description.SetActive(false);
            _isDescriptionActive = false;
        }
    }
}
