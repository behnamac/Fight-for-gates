using System;
using Controllers;
using Levels;
using UnityEngine;

namespace Character.Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private float speedHorizontal;
        [SerializeField] private float minHorizontal;
        [SerializeField] private float maxHorizontal;

        #endregion
        
        #region PRIVATE FIELDS

        private Animator _anim;
        private float _mouseXStartPosition;
        private float _swipeDelta;
        private float _xMove;
        private bool _canMove;
        private static readonly int Run = Animator.StringToHash("Run");

        #endregion

        #region Unity Functions

        private void Awake()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;

            TryGetComponent(out _anim);

            _xMove = transform.position.x;
        }

        private void Update()
        {
            if(_canMove)
                HorizontalMove();
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
        }

        #endregion

        #region Private Function

        private void HorizontalMove()
        {
            _xMove -= GetHorizontalMove() * speedHorizontal * Time.deltaTime;
            _xMove = Mathf.Clamp(_xMove, minHorizontal, maxHorizontal);
            var thisTransform = transform;
            var thisPosition = thisTransform.position;
            thisPosition.x = _xMove;
            thisTransform.position = thisPosition;
        }

        #endregion

        #region PUBLIC METHODS

        private float GetHorizontalMove()
        {
            // MOUSE DOWN
            if (Input.GetMouseButtonDown(0)) _mouseXStartPosition = Input.mousePosition.x;

            // MOUSE ON PRESS
            if (Input.GetMouseButton(0))
            {
                _swipeDelta = Input.mousePosition.x - _mouseXStartPosition;
                _mouseXStartPosition = Input.mousePosition.x;
            }

            // MOUSE UP
            if (Input.GetMouseButtonUp(0)) _swipeDelta = 0;

            return _swipeDelta;
        }

        #endregion

        #region Events

        private void OnLevelStart(Level level)
        {
            _canMove = true;
            _anim.SetBool(Run, true);
        }

        private void OnLevelComplete(Level level)
        {
            _canMove = false;
            _anim.SetBool(Run, false);
        }

        private void OnLevelFail(Level level)
        {
            _canMove = false;
            _anim.SetBool(Run, false);
        }

        #endregion
    }
}