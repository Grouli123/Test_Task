using System;
using UnityEngine;

namespace Interfaces
{
  public interface IPickedupObject<out T> 
  {
    event Action<T> PickedUp;
    void Show();
    void Hide();
    void SetPosition(Vector3 position);
  }
}