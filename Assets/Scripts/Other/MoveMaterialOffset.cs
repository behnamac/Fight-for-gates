using System;
using UnityEngine;
using Levels;
using Controllers;

namespace Other
{
    public class MoveMaterialOffset : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private Renderer _meshRenderer;
        private bool _canMove;

        private void Awake()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;

            TryGetComponent(out _meshRenderer);
        }
        

        // Update is called once per frame
        void Update()
        {
            if (_canMove)
            {
                _meshRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
            }
        }
        
        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
        }
        #region Events

        private void OnLevelStart(Level level)
        {
            _canMove = true;
        }

        private void OnLevelComplete(Level level)
        {
            _canMove = false;
        }

        private void OnLevelFail(Level level)
        {
            _canMove = false;
        }

        #endregion
    }
}
