using UnityEngine;

public class ApplyColor : MonoBehaviour
{
  [SerializeField]
  private FlexibleColorPicker fcp;

  [SerializeField] private Material paint;
  private static readonly int CamoBlackTint = Shader.PropertyToID("_CamoBlackTint");

  private void Update()
  {
    paint.SetColor(CamoBlackTint, fcp.color);
  }
}
