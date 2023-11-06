using UnityEngine;

public class UIRoot : MonoBehaviour
{
  [SerializeField] private Canvas canvas;
  
  public void SetCamera(Camera camera) => 
    canvas.worldCamera = camera;
}
