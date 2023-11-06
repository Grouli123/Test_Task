using Animations;
using Enemies;
using Enemies.Entity;
using UnityEngine;
using Utilities;

namespace StateMachines.Enemies
{
  public class EnemySearchState : EnemyBaseMachineState
  {
    private readonly EntitySearcher entitySearcher;
    private readonly EnemyMove enemyMove;
    private readonly EnemyStateMachine enemy;
    private readonly float searchInterval = 0.2f;

    private float currentCheckTime;

    public EnemySearchState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      EntitySearcher entitySearcher, EnemyMove enemyMove, EnemyStateMachine enemy) : base(stateMachine, animationName, animator)
    {
      this.entitySearcher = entitySearcher;
      this.enemyMove = enemyMove;
      this.enemy = enemy;
    }

    public override void Enter()
    {
      base.Enter();
      SetStartTime();
    }

    public override void LogicUpdate()
    {
      base.LogicUpdate();
      if (IsTimeLeft())
        CheckPlayerOrRestartTimer();
      else
        UpdateTime();
    }

    public override bool IsCanBeInterapted()
    {
      return true;
    }

    private void UpdateTime() => 
      currentCheckTime -= Time.deltaTime;

    private void CheckPlayerOrRestartTimer()
    {
      if (entitySearcher.IsFound())
      {
        enemyMove.SetTarget(entitySearcher.FirstHit.transform);
        ChangeState(enemy.RunState);
      }
      else
        SetStartTime();
    }

    private void SetStartTime() => 
      currentCheckTime = searchInterval;

    private bool IsTimeLeft() => 
      currentCheckTime <= 0;
  }
}