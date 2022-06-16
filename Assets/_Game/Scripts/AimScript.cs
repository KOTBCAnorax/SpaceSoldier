using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimScript : MonoBehaviour
{
    [SerializeField] private LayerMask _aimColliderLayer = new LayerMask();
    [SerializeField] private CharacterController _player;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Image _aimBtnBackground;
    [SerializeField] private Color _aimBtnDefault;
    [SerializeField] private Color _aimBtnHighlighted;
    [SerializeField] private MeshRenderer _muzzleFlash;
    [SerializeField] private float _fireRate = 0.05f;

    private float _lastShotTime;
    private float _muzzleFlashDuration = 0.05f;

    private Transform _raycastHitTransform;

    private Vector2 _screenCenter;

    private bool _aimActive = false;

    public void AimSwitch()
    {
        _aimActive = !_aimActive;
        _player.SetAimActive(_aimActive);

        if (_aimActive)
        {
            _aimBtnBackground.color = _aimBtnHighlighted;
        }
        else
        {
            _aimBtnBackground.color = _aimBtnDefault;
            _muzzleFlash.enabled = false;
        }
    }

    private void Awake()
    {
        _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
}

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_screenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, _aimColliderLayer))
        {
            _raycastHitTransform = raycastHit.transform;
        }

        if (_aimActive)
        {
            Vector3 direction = (_raycastHitTransform.position - transform.position).normalized;
            BodyPartScript targetHit = _raycastHitTransform.gameObject.GetComponent<BodyPartScript>();
            if (targetHit != null && Time.time - _lastShotTime > _fireRate) 
            {
                OneShot(targetHit);
            }
            _player.RotateCharacter(direction, 0f);
        }

        if (Time.time - _lastShotTime > _muzzleFlashDuration)
        {
            _muzzleFlash.enabled = false;
        }
    }

    private void OneShot(BodyPartScript target)
    {
        _lastShotTime = Time.time;
        target.RegisterHit();
        _muzzleFlash.enabled = true;
    }
}
