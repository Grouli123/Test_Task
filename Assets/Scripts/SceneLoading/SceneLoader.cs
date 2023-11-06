using System;
using System.Collections;
using Bootstrapp;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
  public class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly LoadingCurtain loadingCurtain;

    public SceneLoader(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
      _coroutineRunner = coroutineRunner;
      this.loadingCurtain = loadingCurtain;
    }

    public void Load(string name, Action onLoaded = null, Action onCurtainHide = null)
    {
      _coroutineRunner.StopAllCoroutines();
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, onCurtainHide));
    }

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null, Action onCurtainHide= null)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }
      loadingCurtain.Show();
      while (loadingCurtain.IsShown == false)
        yield return null;
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (waitNextScene.isDone == false)
        yield return null;
      
      loadingCurtain.Hide();
      
      onLoaded?.Invoke();
      while (loadingCurtain.IsShown)
        yield return null;
     
      onCurtainHide?.Invoke();
    }
  }
}
