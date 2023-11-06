using GameStates.States.Interfaces;
using Services.Waves;

namespace GameStates.States
{
  internal class GameLoopState : IState
  {
    private readonly IWaveServices waveServices;

    public GameLoopState(IGameStateMachine stateMachine, IWaveServices waveServices)
    {
      this.waveServices = waveServices;
    }

    public void Enter()
    {
      waveServices.Start();
    }

    public void Exit()
    {
    }
  }
}