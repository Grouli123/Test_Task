using Systems.Healths;
using Animations;
using Services.PlayerData;
using StateMachines.Player;
using StaticData.Hero.Components;
using UnityEngine;

namespace Hero
{
    public class HeroStateMachine : BaseEntityStateMachine
    {
        [SerializeField] private BattleAnimator battleAnimator;
        [SerializeField] private HeroMove move;
        [SerializeField] private HeroRotate rotate;
        [SerializeField] private HeroAttack attack;
        [SerializeField] private HeroStamina stamina;
        
        private HeroAttackStaticData attackData;
        private HeroImpactsStaticData impactsData;
        
        public PlayerAttackState AttackState { get; private set; }
        public PlayerHurtState ImpactState { get; private set; }
        public PlayerIdleShieldState IdleShieldState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerRollState RollState { get; private set; }
        public PlayerShieldImpactState ShieldImpactState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerShieldMoveState ShieldMoveState { get; private set; }
        public PlayerDeathState DeathState { get; private set; }
        
        public bool IsBlockingPressed { get; private set; }
        public bool IsBlockingUp => stateMachine.State == IdleShieldState;
        public bool IsRolling => stateMachine.State == RollState;

        public Vector2 MoveAxis { get; private set; }
        public float RotateAngle { get; private set; }

        public void Construct(HeroAttackStaticData attackData, HeroImpactsStaticData impactData, PlayerCharacteristics characteristics)
        {
            this.attackData = attackData;
            impactsData = impactData;
            attack.Construct(attackData, characteristics);
            Initialize();
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            battleAnimator.Triggered += AnimationTriggered;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            battleAnimator.Triggered -= AnimationTriggered;
            AttackState.Cleanup();
        }


        protected override void CreateStates()
        {
            AttackState = new PlayerAttackState(stateMachine, "IsSimpleAttack", battleAnimator, this, attack, attackData, stamina);
            ImpactState = new PlayerHurtState(stateMachine, "IsImpact", battleAnimator, this, impactsData.ImpactCooldown);
            IdleShieldState = new PlayerIdleShieldState(stateMachine, "IsBlocking", "MouseRotation", battleAnimator, this, rotate);
            IdleState = new PlayerIdleState(stateMachine, "IsIdle", "MouseRotation", battleAnimator, this, rotate);
            RollState = new PlayerRollState(stateMachine, "IsRoll", battleAnimator, this, move, stamina);
            ShieldImpactState = new PlayerShieldImpactState(stateMachine, "IsShieldImpact", battleAnimator, this, impactsData.ShieldImpactCooldown);
            MoveState = new PlayerMoveState(stateMachine, "IsIdle", "MoveX", battleAnimator, this, move, rotate);
            ShieldMoveState = new PlayerShieldMoveState(stateMachine, "IsBlocking", "MoveY", battleAnimator, this, move, rotate);
            DeathState = new PlayerDeathState(stateMachine, "IsDead", battleAnimator, this);
        }

        protected override void SetDefaultState() => 
            stateMachine.Initialize(IdleState);


        public void SetAttackState()
        {
            if (stateMachine.State.IsCanBeInterapted() && AttackState.IsCanAttack())
                stateMachine.ChangeState(AttackState);
        }

        public void SetMoveAxis(Vector2 moveDirection) => 
            MoveAxis = moveDirection;

        public void SetIsBlocking(bool isBlocking) => 
            IsBlockingPressed = isBlocking;

        public void SetRollState()
        {
            if (stateMachine.State.IsCanBeInterapted() && RollState.IsCanRoll())
                stateMachine.ChangeState(RollState);
        }

        public void ImpactInShield() => 
            stateMachine.ChangeState(ShieldImpactState);

        public void Impact()
        {
            if (ImpactState.IsKnockbackCooldown() && stateMachine.State.IsCanBeInterapted())
                stateMachine.ChangeState(ImpactState);
        }

        public void SetRotate(float rotateAngle) => 
            RotateAngle = rotateAngle;

        public void Dead()
        {
            stateMachine.ChangeState(DeathState);
        }
    }
}
