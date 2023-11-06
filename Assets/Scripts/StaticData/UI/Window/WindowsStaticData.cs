using System.Collections.Generic;
using UnityEngine;

namespace StaticData.UI
{
  [CreateAssetMenu(fileName = "Windows Instantiate Data", menuName = "Static Data/UI/Create Windows Instantiate Data", order = 55)]
  public class WindowsStaticData : ScriptableObject
  {
    public List<WindowInstantiateData> InstantiateData;
  }
}