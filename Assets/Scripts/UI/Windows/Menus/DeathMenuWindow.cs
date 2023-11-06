using GameStates;
using GameStates.States;
using Services.Score;
using TMPro;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Menus
{
  public class DeathMenuWindow : BaseWindow
  {
    [SerializeField] private Button menuButton;
    
    private IGameStateMachine gameStateMachine;

    public void Construct(IGameStateMachine gameStateMachine, IScoreService scoreService)
    {
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      menuButton.onClick.AddListener(LoadMenu);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      menuButton.onClick.RemoveListener(LoadMenu);
    }

    private void LoadMenu() => 
      gameStateMachine.Enter<MainMenuState>();
  }
}