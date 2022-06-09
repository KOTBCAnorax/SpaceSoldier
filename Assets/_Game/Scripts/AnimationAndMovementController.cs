using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _deadzone = 0.8f;
    [SerializeField] private Transform _cam;

    private PlayerInput _playerInput;

    private int _isRunningForwardHash;

    private bool _joystickDown = false;

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

        if (direction.magnitude <= _deadzone && direction.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            _animator.SetBool(_isRunningForwardHash, false);
        }
        else if (direction.magnitude > _deadzone)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            _animator.SetBool(_isRunningForwardHash, true);
            _controller.Move(transform.forward * _playerSpeed * Time.deltaTime);
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

