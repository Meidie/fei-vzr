using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject go;

    public void Click()
    {
       PlaceObjectsOnPlane.Test(go);
    }
}