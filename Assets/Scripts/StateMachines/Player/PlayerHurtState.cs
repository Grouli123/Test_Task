using Animations;
using Hero;

namespace StateMachines.Player
{
  public class PlayerHurtState : PlayerBaseImpactState
  {
    public PlayerHurtState(StateMachine stateMachine, string animationName, BattleAnimator animator,
      HeroStateMachine hero, float cooldown) : base(stateMachine, animationName, animator, hero, cooldown)
    {
    }
  }
}