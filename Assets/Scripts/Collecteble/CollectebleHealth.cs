using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using DG.Tweening;

namespace Collecteble
{
    public class CollectebleHealth : MonoBehaviour, ICollectebleCollision
    {
        [SerializeField] private float healValue = 10;
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
                character.gaint.Heal(healValue);
                Destroy(gameObject);
            });
        }
    }
}
