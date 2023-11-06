using Animations;
using Hero;

namespace StateMachines.Player
{
  public class PlayerShieldImpactState : PlayerBaseImpactState
  {
    public PlayerShieldImpactState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown) : base(stateMachine, animationName, animator, hero, cooldown)
    {
    }
  }
}