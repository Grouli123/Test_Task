using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public abstract class PlayerBaseMachineState : BaseStateMachineState
  {
    private readonly StateMachine stateMachine;
    
    protected readonly HeroStateMachine hero;
    protected readonly BattleAnimator animator;

    public PlayerBaseMachineState(StateMachine stateMachine, string animationName, BattleAnimator animator, HeroStateMachine hero)
    {
      this.stateMachine = stateMachine;
      this.animationName = Animator.StringToHash(animationName);
      this.animator = animator;
      this.hero = hero;
    }

    public override void Enter()
    {
      base.Enter();
      animator.SetBool(animationName,true);
    }

    public override void Exit()
    {
      base.Exit();
      animator.SetBool(animationName, false);
    }

    public void ChangeState(PlayerBaseMachineState state) => 
      stateMachine.ChangeState(state);

    public void SetFloat(int hash, float value) => 
      animator.SetFloat(hash, value);
    
        
    public bool IsStayHorizontal() => 
      Mathf.Approximately(hero.MoveAxis.x, 0);
    
    public bool IsStayVertical() => 
      Mathf.Approximately(hero.MoveAxis.y, 0);
    
  }
}