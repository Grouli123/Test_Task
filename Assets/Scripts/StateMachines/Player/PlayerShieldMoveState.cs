using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerShieldMoveState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroMove heroMove;
    private readonly HeroRotate heroRotate;

    public PlayerShieldMoveState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator, HeroStateMachine hero, HeroMove heroMove, HeroRotate heroRotate) : base(stateMachine, animationName, animator, hero)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.heroMove = heroMove;
      this.heroRotate = heroRotate;
    }

    public override void Enter()
    {
      base.Enter();
      SetFloat(floatValueHash, hero.MoveAxis.x);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed == false)
      {
        if (IsStayVertical() == false)
          ChangeState(hero.MoveState);
        else
          ChangeState(hero.IdleState);
      }
      else if (IsStayHorizontal())
        ChangeState(hero.IdleShieldState);
      else
      {
        heroMove.Strafe(hero.transform.right * hero.MoveAxis.x);
        heroRotate.Rotate(hero.RotateAngle);
      }        
    }

    public override void Exit()
    {
      base.Exit();
      SetFloat(floatValueHash, 0);
    }

    public override bool IsCanBeInterapted() => 
      true;
  }
}