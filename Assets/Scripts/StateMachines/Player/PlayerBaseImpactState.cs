using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerBaseImpactState : PlayerBaseMachineState
  {
    private readonly float knockbackCooldown;
    private float lastImpactTime;

    protected PlayerBaseImpactState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown) : base(stateMachine, animationName, animator, hero)
    {
      knockbackCooldown = cooldown;
      UpdateImpactTime();
    }

    public override bool IsCanBeInterapted() => 
      false;

    public override void Enter()
    {
      base.Enter();
      UpdateImpactTime();
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();

      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.ShieldMoveState);
        else
          ChangeState(hero.IdleShieldState);
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.IdleState);
        else
          ChangeState(hero.MoveState);
      }
    }

    public bool IsKnockbackCooldown() => 
      Time.time >= lastImpactTime + knockbackCooldown;

    private void UpdateImpactTime() => 
      lastImpactTime = Time.time;
  }
}