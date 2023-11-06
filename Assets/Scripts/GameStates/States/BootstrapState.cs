using System;
using Bootstrapp;
using Enemies.Spawn;
using GameStates.States.Interfaces;
using Input;
using Loots;
using SceneLoading;
using Services;
using Services.Assets;
using Services.Audio;
using Services.Bonuses;
using Services.Factories.Enemy;
using Services.Factories.GameFactories;
using Services.Factories.Loot;
using Services.Hero;
using Services.Input;
using Services.Loot;
using Services.Progress;
using Services.Random;
using Services.Score;
using Services.StaticData;
using Services.UI.Factory;
using Services.UI.Windows;
using Services.Waves;

namespace GameStates.States
{
  public class BootstrapState : IState
  {
    private readonly ISceneLoader sceneLoader;
    private readonly IGameStateMachine gameStateMachine;
    private readonly AllServices services;

    public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ref AllServices services, ICoroutineRunner coroutineRunner, LootContainer lootContainer)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.services = services;
      RegisterServices(coroutineRunner, lootContainer);
    }

    public void Enter() 
      => gameStateMachine.Enter<LoadProgressState>();

    public void Exit(){}

    private void RegisterServices(ICoroutineRunner coroutineRunner, LootContainer lootContainer)
    {
      RegisterStateMachine();
      RegisterInputService();
      RegisterRandom();
      RegisterStaticDataService();
      RegisterProgress();
      RegisterAssets();
      RegisterEnemiesFactory();
      RegisterEnemiesSpawner();
      RegisterScoreService();
      RegisterUIFactory();
      RegisterWindowsService();
      RegisterHeroDeathService();
      RegisterBonusFactory();
      RegisterBonusSpawner();
      RegisterGameFactory();
      RegisterWaveService(coroutineRunner);
      RegisterLootSpawner();
      RegisterLootService(lootContainer);
      RegisterAudioService();
    }

    private void RegisterAudioService() =>
      services.RegisterSingle<IAudioService>(new AudioService(services.Single<IAssetProvider>()));

    private void RegisterWaveService(ICoroutineRunner coroutineRunner) => 
      services.RegisterSingle<IWaveServices>(new WaveServices(services.Single<IEnemySpawner>(), coroutineRunner, services.Single<IBonusSpawner>()));

    private void RegisterEnemiesSpawner() => 
      services.RegisterSingle<IEnemySpawner>(new EnemySpawner(services.Single<IEnemiesFactory>(), services.Single<IRandomService>()));

    private void RegisterEnemiesFactory() => 
      services.RegisterSingle<IEnemiesFactory>(new EnemiesFactory(services.Single<IAssetProvider>(), services.Single<IStaticDataService>()));

    private void RegisterLootSpawner() => 
      services.RegisterSingle<ILootSpawner>(new LootSpawner(services.Single<IAssetProvider>()));

    private void RegisterLootService(LootContainer lootContainer) => 
      services.RegisterSingle<ILootService>(new LootService(services.Single<ILootSpawner>(), services.Single<IRandomService>(),services.Single<IStaticDataService>(), services.Single<IEnemySpawner>(), lootContainer));

    private void RegisterGameFactory()
    {
      services.RegisterSingle<IGameFactory>(
        new GameFactory(
        services.Single<IAssetProvider>(), 
        services.Single<IStaticDataService>(),
        services.Single<IInputService>(),
        services.Single<IEnemySpawner>(),
        services.Single<IWindowsService>(), 
        services.Single<IPersistentProgressService>(),
        services.Single<IBonusSpawner>(),
        services.Single<IHeroDeathService>()));
    }

    private void RegisterStateMachine() => 
      services.RegisterSingle(gameStateMachine);

    private void RegisterInputService()
    {
      IInputService inputService = new InputService(new HeroControls());
      services.RegisterSingle<IInputService>(inputService);
    }

    private void RegisterAssets()
    {
      IAssetProvider provider = new AssetProvider();
      services.RegisterSingle(provider);
    }

    private void RegisterStaticDataService()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.Load();
      services.RegisterSingle(staticData);
    }

    private void RegisterProgress() => 
      services.RegisterSingle(new PersistentProgressService(services.Single<IStaticDataService>().ForHeroCharacteristics()));

    private void RegisterRandom() => 
      services.RegisterSingle(new RandomService());

    private void RegisterUIFactory() =>
      services.RegisterSingle(new UIFactory(
        services.Single<IGameStateMachine>(),
        services.Single<IAssetProvider>(),
        services.Single<IStaticDataService>(), 
        services.Single<IPersistentProgressService>(), 
        services.Single<IScoreService>()));

    private void RegisterWindowsService() => 
      services.RegisterSingle(new WindowsService(services.Single<IUIFactory>()));

    private void RegisterHeroDeathService() => 
      services.RegisterSingle(new HeroDeathService(services.Single<IWindowsService>()));

    private void RegisterBonusSpawner() => 
      services.RegisterSingle(new BonusSpawner(services.Single<IBonusFactory>(), services.Single<IRandomService>()));

    private void RegisterBonusFactory() => 
      services.RegisterSingle(new BonusFactory(services.Single<IAssetProvider>(), services.Single<IStaticDataService>()));

    private void RegisterScoreService()
    {
      services.RegisterSingle(new ScoreService(services.Single<IEnemySpawner>(), 
        services.Single<IStaticDataService>().ForScore(), 
        services.Single<IPersistentProgressService>().Player.Score));
    }
  }
}