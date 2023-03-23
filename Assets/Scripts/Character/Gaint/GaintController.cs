using UnityEngine;
using UnityEngine.UI;

namespace Character.Gaint
{
    public class GaintController : MonoBehaviour
    {
        [SerializeField] private GaintController otherGaint;
        
        [Space(5)]
        
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private Image healthBar;

        private float _currentHealth;
        private bool _shield;
        private Animator _anim;
        public float DamageValue { get; set; }

        private static readonly int RandomHit = Animator.StringToHash("RandomHit");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int RandomAttack = Animator.StringToHash("RandomAttack");
        private static readonly int AttackAnim = Animator.StringToHash("Attack");
        private static readonly int Dead1 = Animator.StringToHash("Dead");

        private void Awake()
        {
            TryGetComponent(out _anim);
            
            _currentHealth = maxHealth;
            healthBar.fillAmount = 1;
        }

        public void AttackAnimation(float damageValue)
        {
            int randomAttack = Random.Range(0, 5);
            _anim.SetInteger(RandomAttack, randomAttack);
            _anim.SetTrigger(AttackAnim);
            DamageValue = damageValue;
        }

        protected void Attack()
        {
            otherGaint.TakeDamage(DamageValue);
        }

        public void TakeDamage(float value)
        {
            if (_shield)
            {
                DiactiveShield();
                return;
            }

            int randomHit = Random.Range(0, 3);
            _anim.SetInteger(RandomHit, randomHit);
            _anim.SetTrigger(Hit);
            
            _currentHealth -= value;
            healthBar.fillAmount = _currentHealth / maxHealth;
            if (_currentHealth <= 0)
            {
                Dead();
            }
        }

        public void ActiveShield()
        {
            _shield = true;
        }

        public void Heel(float value)
        {
            _currentHealth += value;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
            healthBar.fillAmount = _currentHealth / maxHealth;
        }

        private void DiactiveShield()
        {
            _shield = false;
        }

        protected virtual void Dead()
        {
            _anim.SetTrigger(Dead1);
        }
    }
}
