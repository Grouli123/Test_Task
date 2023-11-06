using System.Collections.Generic;
using Systems.Healths;
using ConstantsValue;
using Enemies.Spawn;
using Hero;
using Services.Assets;
using Services.Bonuses;
using Services.Hero;
using Services.Input;
using Services.Progress;
using Services.StaticData;
using Services.UI.Buttons;
using Services.UI.Windows;
using StaticData.Hero;
using StaticData.Level;
using UI.Displaying;
using UI.Windows.Inventories;
using UnityEngine;

namespace Services.Factories.GameFactories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly IInputService inputService;
    private readonly IEnemySpawner enemySpawner;
    private readonly IWindowsService windowsService;
    private readonly IPersistentProgressService progressService;
    private readonly IBonusSpawner bonusSpawner;
    private readonly IHeroDeathService deathService;
    private GameObject heroGameObject;

    public GameFactory(IAssetProvider assets,
      IStaticDataService staticData,
      IInputService inputService,
      IEnemySpawner enemySpawner,
      IWindowsService windowsService,
      IPersistentProgressService progressService,
      IBonusSpawner bonusSpawner,
      IHeroDeathService deathService)
    {
      this.assets = assets;
      this.staticData = staticData;
      this.inputService = inputService;
      this.windowsService = windowsService;
      this.progressService = progressService;
      this.bonusSpawner = bonusSpawner;
      this.enemySpawner = enemySpawner;
      this.deathService = deathService;
    }
    
    public GameObject CreateHero()
    {
      HeroSpawnStaticData spawnData = staticData.ForHero();
      heroGameObject = InstantiateObject(spawnData.HeroPrefab, spawnData.SpawnPoint);
      
      progressService.SetPlayerToDefault();
      IHealth health = heroGameObject.GetComponentInChildren<IHealth>();
      health.SetHp(progressService.Player.Characteristics.Health(), progressService.Player.Characteristics.Health());
      
      heroGameObject.GetComponent<HeroInput>().Construct(inputService);
      heroGameObject.GetComponent<HeroStateMachine>().Construct(
        progressService.Player.AttackData, 
        progressService.Player.ImpactsData,
        progressService.Player.Characteristics);
      
      heroGameObject.GetComponentInChildren<HeroStamina>().Construct(progressService.Player.StaminaStaticData, progressService.Player.Characteristics);
      
      heroGameObject.GetComponent<HeroInventory>().Construct(progressService.Player.Inventory);
      
      heroGameObject.GetComponent<HeroDeath>().Construct(deathService, health);
      return heroGameObject;
    }

    public GameObject CreateHud(GameObject hero)
    {
      GameObject hud = assets.Instantiate<GameObject>(AssetsPath.Hud);
      hud.GetComponentInChildren<HPDisplayer>().Construct(hero.GetComponentInChildren<IHealth>());
      hud.GetComponentInChildren<StaminaDisplayer>().Construct(hero.GetComponentInChildren<IStamina>());
      hud.GetComponentInChildren<HeroScoreDisplayer>().Construct(progressService.Player.Score);
      InitButtons(hud);
      return hud;
    }

    private void InitButtons(GameObject hud)
    {
      OpenWindowButton[] buttons = hud.GetComponentsInChildren<OpenWindowButton>(true);
      for (int i = 0; i < buttons.Length; i++)
      {
        buttons[i].Construct(windowsService);
      }
    }

    public void CreateEnemySpawnPoints(List<SpawnPointStaticData> spawnPoints, SpawnPoint pointPrefab)
    {
      enemySpawner.Cleanup();
      for (int i = 0; i < spawnPoints.Count; i++)
      {
        enemySpawner.AddPoint(CreateEnemySpawnPoint(spawnPoints[i], pointPrefab));
      }
    }

    public void CreateBonusSpawnPoints(List<SpawnPointStaticData> spawnPoints, SpawnPoint pointPrefab)
    {
      bonusSpawner.Cleanup();
      for (int i = 0; i < spawnPoints.Count; i++)
      {
        bonusSpawner.AddPoint(CreateEnemySpawnPoint(spawnPoints[i], pointPrefab));
      }
    }

    private SpawnPoint CreateEnemySpawnPoint(SpawnPointStaticData data, SpawnPoint prefab)
    {
      SpawnPoint spawner = assets.Instantiate(prefab, data.Position).GetComponent<SpawnPoint>();
      spawner.Construct(data.Id);
      return spawner;
    }

    private GameObject InstantiateObject(GameObject prefab, Vector3 at) => 
      assets.Instantiate(prefab, at);
  }
}