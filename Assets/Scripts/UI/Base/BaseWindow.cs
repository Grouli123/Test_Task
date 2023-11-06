using System;
using GameStates;
using Services.UI.Factory;
using UnityEngine;

namespace UI.Base
{
  public class BaseWindow : MonoBehaviour
  {
    public event Action<BaseWindow> Destroyed;

    private void Awake() => 
      OnAwake();


    private void OnDestroy()
    {
      Cleanup();
      Destroyed?.Invoke(this);
    }

    protected virtual void Initialize() { }

    protected virtual void Subscribe() { }

    protected virtual void Cleanup() { }


    private void OnAwake()
    {
      Initialize();
      Subscribe();
    }

    private void CloseWindow() => 
      gameObject.SetActive(false);

    public virtual void Open() => 
      gameObject.SetActive(true);

    public virtual void Close() => 
      CloseWindow();
  }
}