using System;
using System.Collections.Generic;
using GameStates.States;
using GameStates.States.Interfaces;
using Loots;
using SceneLoading;
using Services;
using Services.Factories.GameFactories;
using Services.Factories.Loot;
using Services.Loot;
using Services.Progress;
using Services.StaticData;
using Services.UI.Factory;
using Services.UI.Windows;
using Services.Waves;

namespace GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(ISceneLoader sceneLoader, ref AllServices services, ICoroutineRunner coroutineRunner,
      LootContainer lootContainer)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader,ref services, coroutineRunner, lootContainer),
        [typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader, services.Single<IPersistentProgressService>()),
        [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IWaveServices>()),
        [typeof(LoadGameLevelState)] = new LoadGameLevelState(
          sceneLoader, 
          this, 
          services.Single<IGameFactory>(), 
          services.Single<IUIFactory>(), 
          services.Single<IStaticDataService>(),
          services.Single<IWaveServices>(), 
          services.Single<ILootService>(), 
          services.Single<ILootSpawner>()),
        [typeof(MainMenuState)] = new MainMenuState(services.Single<IUIFactory>(), services.Single<IWindowsService>(), sceneLoader)
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, loadedCallback, curtainHideCallback);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}