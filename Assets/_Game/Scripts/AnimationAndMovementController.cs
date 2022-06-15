using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _controller;

    [Header("Controls")]
    [SerializeField] private PlayerInputActions _playerInputActions;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _deadzoneMax = 0.8f;
    [SerializeField] private float _deadzoneMin = 0.05f;
    [SerializeField] private float _rotationSpeed = 0.2f;

    [Header("Camera")]
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private float _cameraTopClamp = 90;
    [SerializeField] private float _cameraBottomClamp = -90;
    [SerializeField] private float _cameraMoveThreshold = 0.01f;
    [SerializeField] private float _cameraRotationSpeed = 100f;

    private PlayerInputActions.CharacterControlsActions _playerInput;

    private float _cameraTargetYaw = 0;
    private float _cameraTargetPitch = 0;

    private int _isRunningForwardHash;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInput = _playerInputActions.CharacterControls;
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }
    
    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Start()
    {
        _isRunningForwardHash = Animator.StringToHash("isRunningForward");
        Cursor.visible = false;
    }

    private void Update()
    {
        Run();
        Gravity();
    }

    private void Run()
    {
        Vector2 movementInput = _playerInput.Run.ReadValue<Vector2>();
        if (movementInput.sqrMagnitude < _deadzoneMin)
        {
            _animator.SetBool(_isRunningForwardHash, false);
            return;
        }

        Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
        RotateCharacter(direction);

        if (movementInput.magnitude > _deadzoneMax)
        {
            _animator.SetBool(_isRunningForwardHash, true);
            _controller.Move(transform.forward * _playerSpeed * Time.deltaTime);
        }
    }

    private void RotateCharacter(Vector3 direction)
    {
        float x = transform.rotation.x;
        float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTarget.eulerAngles.y;
        float z = transform.rotation.z;

        Quaternion targetRotation = Quaternion.Euler(x, y, z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed);
    }

    public void RotateCamera(InputAction.CallbackContext context)
    {
        Vector2 _lookInput = context.ReadValue<Vector2>();
        if (_lookInput.sqrMagnitude >= _cameraMoveThreshold)
        {
            _cameraTargetYaw += _lookInput.x * _cameraRotationSpeed * Time.deltaTime;
            _cameraTargetPitch -= _lookInput.y * _cameraRotationSpeed * Time.deltaTime;
        }

        _cameraTargetYaw = ClampAngle(_cameraTargetYaw, float.MinValue, float.MaxValue);
        _cameraTargetPitch = ClampAngle(_cameraTargetPitch, _cameraBottomClamp, _cameraTopClamp);

        _cameraTarget.rotation = Quaternion.Euler(_cameraTargetPitch, _cameraTargetYaw, 0.0f);
    }

    private float ClampAngle(float angle, float minValue, float maxValue)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, minValue, maxValue);
    }

    private void Gravity()
    {
        float x = transform.position.x;
        float y = 0f;
        float z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), 0.5f);
    }

}

