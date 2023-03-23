using Character;
using UnityEngine;
using UnityEngine.UI;

namespace Collecteble
{
    public class CollectebleGate : MonoBehaviour, ICollectebleCollision
    {
        [SerializeField] private float minDamageValue = 5;
        [SerializeField] private float maxDamageValue = 15;
        [SerializeField] private Text damageText;
        private Collider _collider;
        private float _damageValue;
        
        private void Awake()
        {
            TryGetComponent(out _collider);
            Destroy(gameObject, 30);

            _damageValue = Random.Range(minDamageValue, maxDamageValue);
            damageText.text = "damage " + _damageValue.ToString("F0");
        }

        public void OnCollide(CharacterCollisionController character)
        {
            _collider.enabled = false;
            character.gaint.AttackAnimation(_damageValue);
        }
    }
}
