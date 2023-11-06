using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerIdleShieldState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroRotate heroRotate;

    public PlayerIdleShieldState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator, HeroStateMachine hero, HeroRotate heroRotate) : base(stateMachine, animationName, animator, hero)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.heroRotate = heroRotate;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.ShieldMoveState);
        else
        if (Mathf.Approximately(hero.RotateAngle, 0) == false)
        {
          heroRotate.Rotate(hero.RotateAngle);
          SetFloat(floatValueHash, Mathf.Sign(hero.RotateAngle));
        }
        else
          SetFloat(floatValueHash, 0);
      }
      else
      {
        if (IsStayVertical())
          ChangeState(hero.IdleState);
        else
          ChangeState(hero.MoveState);
      }
    }

    public override bool IsCanBeInterapted() => 
      true;

  }
}