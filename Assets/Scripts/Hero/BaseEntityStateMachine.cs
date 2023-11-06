using Animations;
using StateMachines;
using UnityEngine;

namespace Hero
{
  
  public abstract class BaseEntityStateMachine : MonoBehaviour
  {
    protected StateMachine stateMachine;
        
    protected void Initialize()
    {
      CreateStateMachine();
      CreateStates();
      SetDefaultState();
      
      Subscribe();
    }

    private void OnDestroy() => 
      Cleanup();

    private void Update() => 
      stateMachine.State.LogicUpdate();

    protected virtual void Subscribe() {}

    protected virtual void Cleanup() {}

    protected abstract void CreateStates();

    protected abstract void SetDefaultState();

    private void CreateStateMachine() => 
      stateMachine = new StateMachine();

    protected void AnimationTriggered() => 
      stateMachine.State.TriggerAnimation();
  }
}