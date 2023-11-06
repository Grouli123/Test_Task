using Systems.Healths;
using Interfaces;
using UnityEngine;

namespace Hero
{
    public class HeroDamageHandler : MonoBehaviour, IDamageableEntity
    {
        [SerializeField] private HeroStateMachine hero;
        [SerializeField] private Health health;
        [SerializeField] private float maxAngle = 45f;
        
        public void TakeDamage(float damage, Vector3 attackPosition)
        {
            if (IsDamageAbsorbed(attackPosition))
                AbsorbDamage();
            else
                TakeDamage(damage);
        }

        private void TakeDamage(float damage)
        {
            hero.Impact();
            health.TakeDamage(damage);
        }

        private void AbsorbDamage() => 
            hero.ImpactInShield();

        private bool IsDamageAbsorbed(Vector3 attackPosition) => 
            (hero.IsBlockingUp && IsAttackForward(attackPosition)) || hero.IsRolling;

        private bool IsAttackForward(Vector3 attackPosition)
        {
            Vector2 attackVector = new Vector2( attackPosition.x - hero.transform.position.x, attackPosition.z - hero.transform.position.z );
            Vector2 forward = new Vector2(hero.transform.forward.x, hero.transform.forward.z);
            return Vector2.Angle(forward, attackVector) <= maxAngle;
        }
    }
}
