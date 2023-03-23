using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Levels;
using UnityEngine;

namespace Other
{
    public class ObjectSampleMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        private bool canMove;

        private void Awake()
        {
            canMove = true;

            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;
        }

        void Update()
        {
            if (canMove)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
        }

        private void OnLevelComplete(Level level)
        {
            canMove = false;
        }

        private void OnLevelFail(Level level)
        {
            canMove = false;
        }
    }
}
