using System;
using GameStates;
using GameStates.States;
using Loots;
using SceneLoading;
using Services;
using UnityEngine;

namespace Bootstrapp
{
  public class GameBootstrapp : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain curtainPrefab;
    [SerializeField] private LootContainer lootContainerPrefab;
    private Game game;
    private AllServices allServices;

    private void Awake()
    {
      allServices = new AllServices();
      game = new Game(this, Instantiate(curtainPrefab), ref allServices, Instantiate(lootContainerPrefab, transform));
      game.StateMachine.Enter<BootstrapState>();
      DontDestroyOnLoad(this);
    }
  }
}