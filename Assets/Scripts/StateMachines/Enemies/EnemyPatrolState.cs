using Animations;

namespace StateMachines.Enemies
{
  public class EnemyPatrolState : EnemyBaseMachineState
  {
    public EnemyPatrolState(StateMachine stateMachine, string animationName, BattleAnimator animator) : base(stateMachine, animationName, animator)
    {
    }

    public override bool IsCanBeInterapted()
    {
      return true;
    }
  }
}