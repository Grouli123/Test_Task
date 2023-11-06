using System;
using Services.UI.Factory;
using UI.Base;

namespace StaticData.UI
{
  [Serializable]
  public struct WindowInstantiateData
  {
    public WindowId ID;
    public BaseWindow Window;
  }
}