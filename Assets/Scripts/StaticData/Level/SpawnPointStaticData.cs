using System;
using Enemies;
using UnityEngine;

namespace StaticData.Level
{
  [Serializable]
  public struct SpawnPointStaticData
  {
    public string Id;
    public Vector3 Position;

    public SpawnPointStaticData(string id, Vector3 position)
    {
      Id = id;
      Position = position;
    }
  }
}