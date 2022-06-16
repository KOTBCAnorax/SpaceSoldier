using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using TOUCH = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CharacterController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private Animator _animator;
    [SerializeField] private UnityEngine.CharacterController _controller;

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
    [SerializeField] private float _cameraRotationSpeed = 10f;

    public void SetCameraRotationSpeed(float value)
    {
        _cameraRotationSpeed = value;
    }

    private PlayerInputActions.CharacterControlsActions _playerInput;

    private float _cameraTargetYaw = 0;
    private float _cameraTargetPitch = 0; 
    private float _pointerDeadzoneX = 700f;
    private float _pointerDeadzoneY = 500f;

    private TOUCH _touch0;
    private TOUCH _touch1;

    private int _isRunningForwardID;
    private int _isRunningBackwardID;
    private int _isRunningLeftID;
    private int _isRunningRightID;

    private bool _isHandheld = false;
    private bool _isAiming = false;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInput = _playerInputActions.CharacterControls;

        _isHandheld = SystemInfo.deviceType == DeviceType.Handheld;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        
        _playerInputActions.Enable();
    }
    
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        
        _playerInputActions.Disable();
    }

    private void Start()
    {
        _isRunningForwardID = Animator.StringToHash("isRunningForward");
        _isRunningBackwardID = Animator.StringToHash("isRunningBackward");
        _isRunningLeftID = Animator.StringToHash("isRunningLeft");
        _isRunningRightID = Animator.StringToHash("isRunningRight");

        _pointerDeadzoneY = Screen.height;
        _pointerDeadzoneX = Screen.width / 3f;
    }

    private void Update()
    {
        HandleTouches();
        Run();
        Gravity();
    }

    public void SetAimActive(bool value)
    {
        _isAiming = value;
    }

    private void Run()
    {
        Vector2 movementInput = _playerInput.Run.ReadValue<Vector2>();
        if (movementInput.sqrMagnitude < _deadzoneMin)
        {
            ResetAllAnimations();
            return;
        }

        Vector3 rotationDirection = GetDirectionFromInput(movementInput);

        if (!_isAiming)
        {
            RotateCharacter(rotationDirection);
            MoveWithAimOff(movementInput);
        }
        else
        {
            MoveWithAimOn(movementInput);
        }
    }

    private Vector3 GetDirectionFromInput(Vector2 movementInput)
    {
        return new Vector3(movementInput.x, 0f, movementInput.y);
    }

    private void MoveWithAimOff(Vector2 movementInput)
    {
        if (movementInput.sqrMagnitude > _deadzoneMax)
        {
            _animator.SetBool(_isRunningForwardID, true);
            _controller.Move(transform.forward * _playerSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool(_isRunningForwardID, false);
        }
    }

    private void MoveWithAimOn(Vector2 movementInput)
    {
        Vector3 movementDirection = _cameraTarget.rotation * GetDirectionFromInput(movementInput);

        if (movementInput.x > _deadzoneMin)
        {
            ResetAllAnimations();
            _animator.SetBool(_isRunningRightID, true);
        }
        else if (movementInput.x < -_deadzoneMin)
        {
            ResetAllAnimations();
            _animator.SetBool(_isRunningLeftID, true);
        }
        else if (movementInput.y > _deadzoneMin)
        {
            ResetAllAnimations();
            _animator.SetBool(_isRunningForwardID, true);
        }
        else if (movementInput.y < -_deadzoneMin)
        {
            ResetAllAnimations();
            _animator.SetBool(_isRunningBackwardID, true);
        }

        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);
    }

    private void ResetAllAnimations()
    {
        _animator.SetBool(_isRunningForwardID, false);
        _animator.SetBool(_isRunningBackwardID, false);
        _animator.SetBool(_isRunningLeftID, false);
        _animator.SetBool(_isRunningRightID, false);
    }

    public void RotateCharacter(Vector3 direction, float cameraAngleModifier = 1.0f)
    {
        float x = transform.rotation.x;
        float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTarget.eulerAngles.y * cameraAngleModifier;
        float z = transform.rotation.z;

        Quaternion targetRotation = Quaternion.Euler(x, y, z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed);
    }

    private void HandleTouches()
    {
        if (TOUCH.activeTouches.Count > 0)
        {
            _touch0 = TOUCH.activeTouches[0];
            if (!IsPointInDeadzone(_touch0.screenPosition))
            {
                RotateCamera(_touch0.delta);
            }
        }

        if (TOUCH.activeTouches.Count > 1)
        {
            _touch1 = TOUCH.activeTouches[1];

            if (!IsPointInDeadzone(_touch1.screenPosition))
            {
                RotateCamera(_touch1.delta);
            }
        }

    }

    public void RotateCamera(Vector2 input)
    {
        if (input.sqrMagnitude >= _cameraMoveThreshold)
        {
            _cameraTargetYaw += input.x * _cameraRotationSpeed * Time.deltaTime;
            _cameraTargetPitch -= input.y * _cameraRotationSpeed * Time.deltaTime;
        }

        _cameraTargetYaw = ClampAngle(_cameraTargetYaw, float.MinValue, float.MaxValue);
        _cameraTargetPitch = ClampAngle(_cameraTargetPitch, _cameraBottomClamp, _cameraTopClamp);

        _cameraTarget.rotation = Quaternion.Euler(_cameraTargetPitch, _cameraTargetYaw, 0.0f);
    }

    private bool IsPointInDeadzone(Vector2 point)
    {
        return point.x < _pointerDeadzoneX && point.y < _pointerDeadzoneY;
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

