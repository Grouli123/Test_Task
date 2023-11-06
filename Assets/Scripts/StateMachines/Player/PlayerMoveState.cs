using Animations;
using Hero;
using UnityEngine;

namespace StateMachines.Player
{
  public class PlayerMoveState : PlayerBaseMachineState
  {
    private readonly int floatValueHash;
    private readonly HeroMove heroMove;
    private readonly HeroRotate heroRotate;

    public PlayerMoveState(StateMachine stateMachine, string animationName, string floatValueName,
      BattleAnimator animator,
      HeroStateMachine hero, HeroMove heroMove, HeroRotate heroRotate) : base(stateMachine, animationName, animator, hero)
    {
      floatValueHash = Animator.StringToHash(floatValueName);
      this.heroMove = heroMove;
      this.heroRotate = heroRotate;
    }

    public override void Enter()
    {
      base.Enter();
      SetFloat(floatValueHash, hero.MoveAxis.y);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (hero.IsBlockingPressed)
      {
        if (IsStayHorizontal() == false)
          ChangeState(hero.ShieldMoveState);
        else
          ChangeState(hero.IdleShieldState);
      }
      else if (IsStayVertical())
        ChangeState(hero.IdleState);
      else
      {
        heroMove.Move(hero.transform.forward * hero.MoveAxis.y);
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