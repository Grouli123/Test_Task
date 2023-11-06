using System.Collections.Generic;
using Systems.Healths;
using CodeBase.CameraLogic;
using ConstantsValue;
using Enemies;
using Enemies.Spawn;
using GameStates.States.Interfaces;
using SceneLoading;
using Services.Factories.GameFactories;
using Services.Factories.Loot;
using Services.Loot;
using Services.Progress;
using Services.StaticData;
using Services.UI.Factory;
using Services.Waves;
using StaticData.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStates.States
{
  public class LoadGameLevelState : IPayloadedState<string>
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IGameStateMachine gameStateMachine;
    private readonly IGameFactory gameFactory;
    private readonly IUIFactory uiFactory;
    private readonly IStaticDataService staticData;
    private readonly IWaveServices waveServices;
    private readonly ILootService lootService;
    private readonly ILootSpawner lootSpawner;

    public LoadGameLevelState(ISceneLoader sceneLoader, 
      IGameStateMachine gameStateMachine, 
      IGameFactory gameFactory, 
      IUIFactory uiFactory, 
      IStaticDataService staticData,
      IWaveServices waveServices,
      ILootService lootService,
      ILootSpawner lootSpawner)
    {
      this.sceneLoader = sceneLoader;
      this.gameStateMachine = gameStateMachine;
      this.gameFactory = gameFactory;
      this.uiFactory = uiFactory;
      this.staticData = staticData;
      this.waveServices = waveServices;
      this.lootService = lootService;
      this.lootSpawner = lootSpawner;
    }

    public void Enter(string payload)
    {
      sceneLoader.Load(payload, OnLoaded);
    }

    public void Exit() { }

    private void OnLoaded()
    {
      InitGameWorld();
      gameStateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
      InitUIRoot();
      
      LevelStaticData levelData = GetLevelData();
      InitEnemySpawners(levelData.EnemySpawners, levelData.SpawnPointPrefab);
      InitBonusSpawner(levelData.BonusSpawners, levelData.SpawnPointPrefab);
      InitWaves(levelData.LevelWaves);
      InitLootService(levelData.LevelKey);
      
      GameObject hero = gameFactory.CreateHero();
      GameObject hud = CreateHud(hero);

      CleanupLootSpawner();

      Camera camera = Camera.main;
      CameraFollow(hero, camera);
      SetCameraToHud(hud, camera);
    }

    private LevelStaticData GetLevelData()
    {
      string sceneKey = SceneManager.GetActiveScene().name;
      return staticData.ForLevel(sceneKey);
    }

    private void InitEnemySpawners(List<SpawnPointStaticData> enemySpawners, SpawnPoint pointPrefab) => 
      gameFactory.CreateEnemySpawnPoints(enemySpawners, pointPrefab);

    private void InitBonusSpawner(List<SpawnPointStaticData> bonusSpawners, SpawnPoint pointPrefab) => 
      gameFactory.CreateBonusSpawnPoints(bonusSpawners, pointPrefab);

    private void CleanupLootSpawner() => 
      lootSpawner.Cleanup();

    private void InitWaves(LevelWaveStaticData waves) => 
      waveServices.SetLevelWaves(waves);

    private GameObject CreateHud(GameObject hero) => 
      gameFactory.CreateHud(hero);

    private void InitUIRoot() => 
      uiFactory.CreateUIRoot();

    private void InitLootService(string sceneName) => 
      lootService.SetSceneName(sceneName);

    private void CameraFollow(GameObject hero, Camera camera) => 
      camera.GetComponent<CameraFollow>().Follow(hero);

    private void SetCameraToHud(GameObject hud, Camera camera) => 
      hud.GetComponent<Canvas>().worldCamera = camera;
  }
}