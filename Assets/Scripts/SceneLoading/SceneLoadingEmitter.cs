using System;
using UnityEngine;

namespace SceneLoading
{
  public class SceneLoadingEmitter : MonoBehaviour
  {
    [SerializeField] private string nextScene;

    public static event Action<string> OnAsyncSceneLoaded;
    public static event Action<string> OnSyncSceneLoaded;

    public void LoadScene() => 
      OnSyncSceneLoaded?.Invoke(nextScene);

    public void LoadAsyncScene() => 
      OnAsyncSceneLoaded?.Invoke(nextScene);

    public void LoadAsyncScene(string scene) => 
      OnAsyncSceneLoaded?.Invoke(scene);

    public void LoadScene(string scene) => 
      OnSyncSceneLoaded?.Invoke(scene);
  }
}
