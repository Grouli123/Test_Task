using System;
using ConstantsValue;
using GameStates;
using Services.Assets;
using Services.PlayerData;
using Services.Progress;
using Services.Score;
using Services.StaticData;
using StaticData.UI;
using UI.Base;
using UI.Windows;
using UI.Windows.Inventories;
using UI.Windows.Menus;
using UnityEngine;

namespace Services.UI.Factory
{
  public class UIFactory : IUIFactory
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly IPersistentProgressService progressService;
    private readonly IScoreService scoreService;

    private Transform uiRoot;

    private Camera mainCamera;

    public event Action<WindowId,BaseWindow> Spawned;

    public UIFactory(IGameStateMachine gameStateMachine,
      IAssetProvider assets,
      IStaticDataService staticData, 
      IPersistentProgressService progressService,
      IScoreService scoreService)
    {
      this.gameStateMachine = gameStateMachine;
      this.assets = assets;
      this.staticData = staticData;
      this.progressService = progressService;
      this.scoreService = scoreService;
    }

    public void CreateUIRoot()
    {
      uiRoot = assets.Instantiate<GameObject>(AssetsPath.UIRootPath).transform;
      uiRoot.GetComponent<UIRoot>().SetCamera(GetCamera());
    }

    public void CreateWindow(WindowId id)
    {
      WindowInstantiateData config = LoadWindowInstantiateData(id);
      switch (id)
      {
        case WindowId.Inventory:
          CreateInventoryWindow(config, id);
          break;
        case WindowId.PauseMenu:
          CreatePauseMenuWindow(config, id);
          break;
        case WindowId.MainMenu:
          CreateMainMenuWindow(config, id);
          break;
         case WindowId.DeathMenu:
           CreateDeathMenuWindow(config, id);
           break;
        default:
          CreateWindow(config, id);
          break;
      }
    }

    private void CreateInventoryWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      ((InventoryWindow)window).Construct(progressService.Player);
      NotifyAboutCreateWindow(id, window);
    }

    private void CreatePauseMenuWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      ((PauseMenuWindow)window).Construct(gameStateMachine);
      NotifyAboutCreateWindow(id, window);
    }

    private void CreateMainMenuWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      ((MainMenuWindow)window).Construct(gameStateMachine);
      NotifyAboutCreateWindow(id, window);
    }

    private void CreateDeathMenuWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      ((DeathMenuWindow)window).Construct(gameStateMachine, scoreService);
      NotifyAboutCreateWindow(id, window);
    }

    private void CreateWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      NotifyAboutCreateWindow(id, window);
    }

    private void NotifyAboutCreateWindow(WindowId id, BaseWindow window) => 
      Spawned?.Invoke(id, window);

    private BaseWindow InstantiateWindow(WindowInstantiateData config) => 
      assets.Instantiate(config.Window, uiRoot);

    private WindowInstantiateData LoadWindowInstantiateData(WindowId id) => 
      staticData.ForWindow(id);

    private Camera GetCamera()
    {
      if (mainCamera == null)
        mainCamera = Camera.main;
      return mainCamera;
    }
  }
}