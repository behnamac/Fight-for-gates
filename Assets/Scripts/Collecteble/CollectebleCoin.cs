using UnityEngine;
using DG.Tweening;
using Character;
using Controllers;

namespace Collecteble
{
    public class CollectebleCoin : MonoBehaviour, ICollectebleCollision
    {
        private Collider _collider;

        private void Awake()
        {
            TryGetComponent(out _collider);
            Destroy(gameObject, 30);
        }

        private void Update()
        {
            transform.Rotate(0, 100 * Time.deltaTime, 0);
        }

        public void OnCollide(CharacterCollisionController character)
        {
            _collider.enabled = false;
            transform.DOMove(character.gaint.transform.position + Vector3.up * 8, 1).OnComplete(() =>
            {
                UiController.Instance.AddCoin(5);
                Destroy(gameObject);
            });
        }
    }
}
