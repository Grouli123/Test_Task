using Animations;
using Enemies;
using Enemies.Entity;
using StaticData.Enemies;
using UnityEngine;
using Utilities;

namespace StateMachines.Enemies
{
  public class EnemyIdleState : EnemyBaseMachineState
  {
    private readonly EnemyMove enemyMove;
    private readonly EnemiesMoveStaticData moveData;
    private readonly EnemyStateMachine enemy;
    private readonly EnemyRotate enemyRotate;

    public EnemyIdleState(StateMachine stateMachine, string animationName, BattleAnimator animator, EnemyMove enemyMove,
      EnemiesMoveStaticData moveData, EnemyStateMachine enemy, EnemyRotate enemyRotate) : base(stateMachine, animationName, animator)
    {
      this.enemyMove = enemyMove;
      this.moveData = moveData;
      this.enemy = enemy;
      this.enemyRotate = enemyRotate;
    }
    public override bool IsCanBeInterapted()
    {
      return true;
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsCanAttack())
        ChangeState(enemy.AttackState);
      else if (IsTargetCameOff())
        ChangeState(enemy.WalkState);
      else
        enemyRotate.LookAt(enemyMove.TargetPosition);
    }

    private bool IsTargetCameOff() => 
      Vector3.Distance(enemyMove.TargetPosition, enemy.transform.position) > moveData.DistanceToAttack;

    private bool IsCanAttack() =>
      Vector3.Distance(enemyMove.TargetPosition, enemy.transform.position) < moveData.DistanceToAttack &&
      enemy.AttackState.IsCanAttack();
  }
}