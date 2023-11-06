using Systems.Healths;
using Animations;
using Hero;
using StateMachines.Enemies;
using StaticData.Enemies;
using UnityEngine;
using Utilities;

namespace Enemies.Entity
{
  public class EnemyStateMachine : BaseEntityStateMachine
  {
    [SerializeField] protected BattleAnimator battleAnimator;
    [SerializeField] private EntitySearcher entitySearcher;
    [SerializeField] private EnemyMove move;
    [SerializeField] private EnemyRotate rotate;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private EnemyDeath death;
    
    private EnemiesMoveStaticData moveData;
    private EnemyAttackStaticData attackData;
    private float damageCoeff;

    private IHealth health;
    
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }
    public EnemyHurtState ImpactState { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyRunState RunState { get; private set; }
    public EnemySearchState SearchState { get; private set; }
    public EnemyWalkState WalkState { get; private set; }

    public void Construct(EnemiesMoveStaticData moveData, EnemyAttackStaticData attackData, float damageCoeff, IHealth health)
    {
      this.moveData = moveData;
      this.attackData = attackData;
      this.damageCoeff = damageCoeff;
      this.health = health;
      this.health.Dead += Dead;
      attack.Construct(this.attackData);
      Initialize();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      battleAnimator.Triggered += AnimationTriggered;
      death.Revived += Revive;
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      battleAnimator.Triggered -= AnimationTriggered;
      health.Dead -= Dead;
      death.Revived -= Revive;
      AttackState.Cleanup();
    }

    protected override void CreateStates()
    {
      AttackState = new EnemyAttackState(stateMachine, "IsSimpleAttack", battleAnimator, this, attack, attackData, damageCoeff);
      DeathState = new EnemyDeathState(stateMachine, "IsDead", battleAnimator, death);
      ImpactState = new EnemyHurtState(stateMachine, "IsImpact", battleAnimator, this);
      IdleState = new EnemyIdleState(stateMachine, "IsIdle", battleAnimator, move, moveData, this, rotate);
      RunState = new EnemyRunState(stateMachine, "IsRun", battleAnimator, move, moveData, this);
      SearchState = new EnemySearchState(stateMachine, "IsIdle", battleAnimator, entitySearcher, move, this);
      WalkState = new EnemyWalkState(stateMachine, "IsWalk", battleAnimator, move, moveData, this);
    }

    protected override void SetDefaultState() => 
      stateMachine.Initialize(SearchState);

    public void Impact()
    {
      if (stateMachine.State.IsCanBeInterapted())
        stateMachine.ChangeState(ImpactState);
    }

    public void UpdateDamageCoeff(float coeff)
    {
      damageCoeff = coeff;
      AttackState.UpdateDamageCoeff(damageCoeff);
    }

    private void Dead() => 
      stateMachine.ChangeState(DeathState);

    private void Revive() => 
      stateMachine.ChangeState(SearchState);
  }
}