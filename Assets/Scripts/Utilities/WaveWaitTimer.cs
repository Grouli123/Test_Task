using System;
using System.Collections;
using UnityEngine;

namespace Utilities
{
  public class WaveWaitTimer : MonoBehaviour
  {
    public event Action TimeOut;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void StartWait(float time)
    {
      StartCoroutine(Wait(time));
    }

    private IEnumerator Wait(float time)
    {
      yield return new WaitForSeconds(time);
      TimeOut?.Invoke();
    }
  }
}