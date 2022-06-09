using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _deadzone = 0.8f;

    private PlayerInput _playerInput;

    private int _isRunningForwardHash;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        _isRunningForwardHash = Animator.StringToHash("isRunningForward");
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Vector2 inputVector = _playerInput.CharacterControls.Run.ReadValue<Vector2>();
        Vector3 direction = GetDirection3D(inputVector);

        if (direction.magnitude > _deadzone)
        {
            _animator.SetBool(_isRunningForwardHash, true);
            _controller.Move(direction.normalized * _playerSpeed * Time.deltaTime);
            gameObject.transform.forward = direction;
        }
        else if (direction.magnitude <= _deadzone)
        {
            gameObject.transform.forward = direction;
            _animator.SetBool(_isRunningForwardHash, false);
        }
        else
        {
            _animator.SetBool(_isRunningForwardHash, false);
        }
    }

    private Vector3 GetDirection3D(Vector2 inputVector)
    {
        return new Vector3(inputVector.x, 0f, inputVector.y);
    }
}

