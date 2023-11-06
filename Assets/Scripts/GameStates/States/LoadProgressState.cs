using GameStates.States.Interfaces;
using SceneLoading;
using Services.Progress;

namespace GameStates.States
{
  public class LoadProgressState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISceneLoader sceneLoader;
    private readonly IPersistentProgressService progressService;

    public LoadProgressState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IPersistentProgressService progressService)
    {
      this.gameStateMachine = gameStateMachine;
      this.sceneLoader = sceneLoader;
      this.progressService = progressService;
    }

    public void Enter()
    {
      LoadData();
      gameStateMachine.Enter<MainMenuState>();
    }

    public void Exit()
    {
      
    }

    private void LoadData()
    {
      
    }
  }
}