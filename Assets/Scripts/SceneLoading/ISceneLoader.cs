using System;

namespace SceneLoading
{
  public interface ISceneLoader
  {
    void Load(string name, Action onLoaded = null, Action onCurtainHide = null);
  }
}