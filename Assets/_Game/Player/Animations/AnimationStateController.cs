using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private int _isRunningForwardHash;
    private int _isRunningBackwardHash;
    private int _isRunningLeftHash;
    private int _isRunningRightHash;

    void Start()
    {
        _isRunningForwardHash = Animator.StringToHash("isRunningForward");
        _isRunningBackwardHash = Animator.StringToHash("isRunningBackward");
        _isRunningLeftHash = Animator.StringToHash("isRunningLeft");
        _isRunningRightHash = Animator.StringToHash("isRunningRight");
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        bool isRunningForward = _playerAnimator.GetBool(_isRunningForwardHash);
        bool isRunningBackward = _playerAnimator.GetBool(_isRunningBackwardHash);
        bool isRunningLeft = _playerAnimator.GetBool(_isRunningLeftHash);
        bool isRunningRight = _playerAnimator.GetBool(_isRunningRightHash);

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);

        if (!isRunningForward && forwardPressed)
        {
            _playerAnimator.SetBool(_isRunningForwardHash, true);
        }

        if (isRunningForward && !forwardPressed)
        {
            _playerAnimator.SetBool(_isRunningForwardHash, false);
        }
        
        if (!isRunningBackward && backwardPressed)
        {
            _playerAnimator.SetBool(_isRunningBackwardHash, true);
        }

        if (isRunningBackward && !backwardPressed)
        {
            _playerAnimator.SetBool(_isRunningBackwardHash, false);
        }
        
        if (!isRunningLeft && leftPressed)
        {
            _playerAnimator.SetBool(_isRunningLeftHash, true);
        }

        if (isRunningLeft && !leftPressed)
        {
            _playerAnimator.SetBool(_isRunningLeftHash, false);
        }

        if (!isRunningRight && rightPressed)
        {
            _playerAnimator.SetBool(_isRunningRightHash, true);
        }

        if (isRunningRight && !rightPressed)
        {
            _playerAnimator.SetBool(_isRunningRightHash, false);
        }
    }

    private void SetAllBool(bool value)
    {
        foreach(AnimatorControllerParameter parameter in _playerAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                _playerAnimator.SetBool(parameter.name, value);
            }
        }
    }
}
