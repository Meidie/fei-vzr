using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Lights : MonoBehaviour
{
    private bool _areLightsOn;
    private Sequence _sequence;
    private List<GameObject> _frontLights = new List<GameObject>();
    private List<MeshRenderer> _lightMeshes = new List<MeshRenderer>();
    
    public void TurnLightsOn()
    {
        if (!_frontLights.Any())
        {
            _frontLights = GameObject.FindGameObjectsWithTag("front_light").ToList();
        }

        if (!_lightMeshes.Any())
        {
            _lightMeshes = GameObject.FindGameObjectsWithTag("light_mesh").Select(go => go.GetComponent<MeshRenderer>())
                .ToList();
        }
        

        if (!_areLightsOn)
        {
            foreach (var lightMesh in _lightMeshes)
            {
                lightMesh.enabled = true;
            }
            
            var sequence = DOTween.Sequence();
            foreach (var frontLight in _frontLights)
            {
                sequence.Join(frontLight.transform.DOLocalRotate(new Vector3(-45, 0, 0), 0.5f));
            }

            sequence.Play();
            _areLightsOn = true;
        }
        else
        {
            foreach (var lightMesh in _lightMeshes)
            {
                lightMesh.enabled = false;
            }
            
            var sequence = DOTween.Sequence();
            foreach (var frontLight in _frontLights)
            {
                sequence.Join(frontLight.transform.DOLocalRotate(new Vector3(14, 0, 0), 0.5f));
            }

            sequence.Play();
            _areLightsOn = false;
        }
    }
}