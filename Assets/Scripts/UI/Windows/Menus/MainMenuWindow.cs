using ConstantsValue;
using GameStates;
using GameStates.States;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Menus
{
  public class MainMenuWindow : BaseWindow
  {
    [SerializeField] private Button playButton;
    
    private IGameStateMachine gameStateMachine;

    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      playButton.onClick.AddListener(StartGame);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      playButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame() => 
      gameStateMachine.Enter<LoadGameLevelState, string>(Constants.GameScene);
  }
}