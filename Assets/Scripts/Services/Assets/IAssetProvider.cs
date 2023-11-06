using UnityEngine;

namespace Services.Assets
{
  public interface IAssetProvider : IService
  {
    T Load<T>(string path) where T : Object;
    T Instantiate<T>(string prefabPath) where T: Object;
    T Instantiate<T>(T prefab) where T : Object;
    T Instantiate<T>(T prefab, Vector3 at) where T : Object;
    T Instantiate<T>(T prefab, Transform parent) where T : Object;
    T Instantiate<T>(T prefab, Vector3 at, Quaternion rotation, Transform parent) where T : Object;
  }
}