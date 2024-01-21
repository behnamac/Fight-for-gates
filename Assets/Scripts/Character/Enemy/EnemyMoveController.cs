using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;
using Controllers;
using DG.Tweening;

namespace Character.Enemy
{
    public class EnemyMoveController : MonoBehaviour
    {
        [SerializeField] private float minHorizontalMove;
        [SerializeField] private float maxHorizontalMove;
        [SerializeField] private float changeLineTime;

        private Animator _anim;
        private static readonly int Run = Animator.StringToHash("Run");

        private void Awake()
        {
            // Subscribe to level events
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;

            // Try to get the Animator component
            TryGetComponent(out _anim);
        }

        private void OnDestroy()
        {
            // Unsubscribe from level events
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
        }

        private void HorizontalMove()
        {
            // Move to a random horizontal position using DOTween
            float randomLine = Random.Range(minHorizontalMove, maxHorizontalMove);
            transform.DOMoveX(randomLine, 0.5f);
        }

        private void OnLevelStart(Level level)
        {
            // Start horizontal movement on level start
            InvokeRepeating(nameof(HorizontalMove), changeLineTime, changeLineTime);
            _anim.SetBool(Run, true);
        }

        private void OnLevelComplete(Level level)
        {
            // Stop horizontal movement on level complete
            CancelInvoke(nameof(HorizontalMove));
            _anim.SetBool(Run, false);
        }

        private void OnLevelFail(Level level)
        {
            // Stop horizontal movement on level fail
            CancelInvoke(nameof(HorizontalMove));
            _anim.SetBool(Run, false);
        }
    }
}
