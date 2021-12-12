using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> controllers;
   [SerializeField] private GameObject menu;

   private void Awake()
   {
      foreach (var controller in controllers)
      {
         controller.SetActive(false);
      }
   }

   private void OnEnable()
   {
      PlaceObjectsOnPlane.ONPlacedObject += ShowMenu;
   }
   
   private void OnDisable()
   {
      PlaceObjectsOnPlane.ONPlacedObject -= ShowMenu;
   }

   private void ShowMenu(GameObject obj)
   {
      menu.GetComponent<TweenSequencer>().Show(EnableControllers);
   }

   private void EnableControllers()
   {
      foreach (var controller in controllers)
      {
         controller.SetActive(true);
      }
   }
}
