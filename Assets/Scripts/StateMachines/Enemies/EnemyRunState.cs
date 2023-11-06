using Animations;
using Enemies;
using Enemies.Entity;
using StaticData.Enemies;
using UnityEngine;

namespace StateMachines.Enemies
{
  public class EnemyRunState : EnemyBaseMachineState
  {
    private readonly EnemyMove enemyMove;
    private readonly EnemiesMoveStaticData moveData;
    private readonly EnemyStateMachine enemy;

    public EnemyRunState(StateMachine stateMachine, string animationName, BattleAnimator animator, EnemyMove enemyMove,
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
      enemyMove.UpdateSpeed(moveData.RunSpeed);
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      
      if (IsReachWalkState())
        ChangeState(enemy.WalkState);
    }

    private bool IsReachWalkState() => 
      Vector3.Distance(enemyMove.TargetPosition, enemy.transform.position) < moveData.DistanceToWalk;
  }
}