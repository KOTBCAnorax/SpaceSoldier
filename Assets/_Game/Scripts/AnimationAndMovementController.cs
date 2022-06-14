using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndMovementController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _cam;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _deadzoneMax = 0.8f;
    [SerializeField] private float _deadzoneMin = 0.05f;
    [SerializeField] private float _rotationSpeed = 0.2f;

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
        RotateInInputDirection(inputVector);

        if (inputVector.magnitude > _deadzoneMax)
        {
            _animator.SetBool(_isRunningForwardHash, true);
            _controller.Move(transform.forward * _playerSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(_isRunningForwardHash, false);
        }

        Gravity();
    }

    private void RotateInInputDirection(Vector2 inputVector)
    {
        if (inputVector.magnitude < _deadzoneMin)
        {
            return;
        }

        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        float x = transform.rotation.x;
        float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
        float z = transform.rotation.z;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(x, y, z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed);
    }

    private void Gravity()
    {
        float x = transform.position.x;
        float y = 0f;
        float z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), 0.5f);
    }

}

