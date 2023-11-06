using Animations;
using Enemies;
using Enemies.Entity;
using StaticData.Enemies;
using UnityEngine;

namespace StateMachines.Enemies
{
  public class EnemyWalkState : EnemyBaseMachineState
  {
    private readonly EnemyMove enemyMove;
    private readonly EnemiesMoveStaticData moveData;
    private readonly EnemyStateMachine enemy;

    public EnemyWalkState(StateMachine stateMachine, string animationName, BattleAnimator animator, EnemyMove enemyMove,
      EnemiesMoveStaticData moveData, EnemyStateMachine enemy) : base(stateMachine, animationName, animator)
    {
      this.enemyMove = enemyMove;
      this.moveData = moveData;
      this.enemy = enemy;
    }

    public override bool IsCanBeInterapted()
    {
      return true;
    }

    public override void Enter()
    {
      base.Enter();
      enemyMove.StartMove();
      enemyMove.UpdateSpeed(moveData.WalkSpeed);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsReachAttackPosition())
      {
        if (IsCanAttack())
          ChangeState(enemy.AttackState);
        else
          ChangeState(enemy.IdleState);
      }
      else if (IsTargetCameOff())
        ChangeState(enemy.RunState);
    }

    public override void Exit()
    {
      base.Exit();
      enemyMove.Stop();
    }

    private bool IsTargetCameOff() => 
      Vector3.Distance(enemyMove.TargetPosition, enemy.transform.position) > moveData.DistanceToWalk;

    private bool IsCanAttack() =>
      enemy.AttackState.IsCanAttack();

    private bool IsReachAttackPosition() => 
      Vector3.Distance(enemyMove.TargetPosition, enemy.transform.position) < moveData.DistanceToAttack;
  }
}