using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class PointerOverUI
{
    public static bool IsPointerOverUIObject(this Vector2 touchPosition)
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position =  new Vector2(touchPosition.x, touchPosition.y)
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
    }
}
