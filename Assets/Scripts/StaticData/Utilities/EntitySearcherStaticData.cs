using UnityEngine;

namespace StaticData.Utilities
{
  [CreateAssetMenu(fileName = "EntitySearcherStaticData", menuName = "Static Data/Utilities/Create Entity Searcher Data", order = 55)]
  public class EntitySearcherStaticData : ScriptableObject
  {
    public LayerMask Mask;
    public int EntitiesCount;
    public float CheckRadius;
  }
}