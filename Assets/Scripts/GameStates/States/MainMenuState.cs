using ConstantsValue;
using GameStates.States.Interfaces;
using SceneLoading;
using Services.UI.Factory;
using Services.UI.Windows;

namespace GameStates.States
{
  public class MainMenuState : IState
  {
    private readonly IUIFactory uiFactory;
    private readonly IWindowsService windowsService;
    private readonly ISceneLoader sceneLoader;

    public MainMenuState(IUIFactory uiFactory, IWindowsService windowsService, ISceneLoader sceneLoader)
    {
      this.uiFactory = uiFactory;
      this.windowsService = windowsService;
      this.sceneLoader = sceneLoader;
    }

    public void Enter()
    {
      sceneLoader.Load(Constants.MainMenuScene, OnLoaded);
    }

    private void OnLoaded()
    {
      uiFactory.CreateUIRoot();
      windowsService.Open(WindowId.MainMenu);
    }

    public void Exit()
    {
      
    }
  }
}