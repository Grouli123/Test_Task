using Systems.Healths;
using Services.Hero;
using UnityEngine;

namespace Hero
{
  public class HeroDeath : MonoBehaviour
  {
    [SerializeField] private HeroStateMachine hero;
    [SerializeField] private Collider heroCollider;
    [SerializeField] private HeroInput input;
    
    private IHeroDeathService deathService;
    private IHealth health;

    public void Construct(IHeroDeathService deathService, IHealth health)
    {
      this.deathService = deathService;
      this.health = health;
      this.health.Dead += Dead;
    }

    private void Dead()
    {
      input.Disable();
      hero.Dead();
      heroCollider.enabled = false;
      deathService.Dead();
      Cleanup();
    }

    private void Cleanup() => 
      health.Dead -= Dead;
  }
}