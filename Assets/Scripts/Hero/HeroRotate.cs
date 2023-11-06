using UnityEngine;

namespace Hero
{
  public class HeroRotate : MonoBehaviour
  {
    [SerializeField] private float rotateSpeed;

    public void Rotate(float angle)
    {
      transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + Vector3.up * angle,
        rotateSpeed * Time.deltaTime);
    }
  }
}