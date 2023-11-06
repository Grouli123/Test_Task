using Bootstrapp;
using Loots;
using SceneLoading;
using Services;

namespace GameStates
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, ref AllServices services,
      LootContainer lootContainer)
    {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner,curtain), ref services, coroutineRunner, lootContainer);
    }
  }
}