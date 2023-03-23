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
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;

            TryGetComponent(out _anim);
        }
        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
        }

        private void HorizontalMove()
        {
            float randomLine = Random.Range(minHorizontalMove, maxHorizontalMove);
            transform.DOMoveX(randomLine, 0.5f);
        }

        private void OnLevelStart(Level level)
        {
            InvokeRepeating(nameof(HorizontalMove), changeLineTime, changeLineTime);
            _anim.SetBool(Run, true);
        }

        private void OnLevelComplete(Level level)
        {
            CancelInvoke(nameof(HorizontalMove));
            _anim.SetBool(Run, false);
        }

        private void OnLevelFail(Level level)
        {
            CancelInvoke(nameof(HorizontalMove));
            _anim.SetBool(Run, false);
        }
    }
}
