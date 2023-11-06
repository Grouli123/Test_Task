using Animations;
using Hero;
using StaticData.Hero.Components;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerAttackState : PlayerBaseMachineState
  {
    private readonly HeroAttack heroAttack;
    private readonly HeroStamina heroStamina;
    private readonly float attackCooldown;
    
    private float lastAttackTime;

    private bool isAttackEnded;

    public PlayerAttackState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, HeroAttack heroAttack, HeroAttackStaticData attackData, HeroStamina heroStamina) : base(stateMachine, animationName, animator, hero)
    {
      this.heroAttack = heroAttack;
      this.heroStamina = heroStamina;
      this.animator.Attacked += Attack;
      attackCooldown = attackData.AttackCooldown;
      UpdateAttackTime();
    }

    public void Cleanup()
    {
      animator.Attacked -= Attack;
    }

    public bool IsCanAttack() => 
      Time.time >= lastAttackTime + attackCooldown && heroStamina.IsCanAttack();

    public override void Enter()
    {
      base.Enter();
      isAttackEnded = false;
    }

    public override bool IsCanBeInterapted() => 
      isAttackEnded;

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      isAttackEnded = true;
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

    private void Attack()
    {
      heroAttack.Attack();
      heroStamina.WasteToAttack();
    }

    private void UpdateAttackTime() => 
      lastAttackTime = Time.time;
  }
}