using System.Linq;
using Bonuses;
using Enemies.Spawn;
using StaticData.Level;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor.Level
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.EnemySpawners = FindObjectsOfType<EnemySpawnMarker>()
          .Select(x => new SpawnPointStaticData(x.GetComponent<UniqueId>().Id, x.transform.position))
          .ToList();
        
        levelData.BonusSpawners = FindObjectsOfType<BonusSpawnMarker>()
          .Select(x => new SpawnPointStaticData(x.GetComponent<UniqueId>().Id, x.transform.position))
          .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}