using GameStates;
using GameStates.States;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Menus
{
  public class PauseMenuWindow : BaseWindow
  {
    [SerializeField] private Button menuButton;
    
    private IGameStateMachine gameStateMachine;

    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      menuButton.onClick.AddListener(LoadMainMenu);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      menuButton.onClick.RemoveListener(LoadMainMenu);
    }

    private void LoadMainMenu() => 
      gameStateMachine.Enter<MainMenuState>();
  }
}