using UnityEngine;

namespace UI.Displaying
{
  public class LookAtCamera : MonoBehaviour
  {
    [SerializeField] private Canvas windowCanvas;
    
    private Camera mainCamera;

    private void Start()
    {
      mainCamera = Camera.main;
      windowCanvas.worldCamera = mainCamera;
    }

    private void Update()
    {
      Quaternion rotation = mainCamera.transform.rotation;
      transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
  }
}
