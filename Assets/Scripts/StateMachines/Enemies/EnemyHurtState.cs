using Animations;
using Enemies;
using Enemies.Entity;

namespace StateMachines.Enemies
{
  public class EnemyHurtState : EnemyBaseMachineState
  {
    private readonly EnemyStateMachine enemy;

    public EnemyHurtState(StateMachine stateMachine, string animationName, BattleAnimator animator, EnemyStateMachine enemy) : base(stateMachine, animationName, animator)
    {
      this.enemy = enemy;
    }

    public override bool IsCanBeInterapted()
    {
      return false;
    }

    public override void TriggerAnimation()
    {
      base.TriggerAnimation();
      ChangeState(enemy.IdleState);
    }
  }
}