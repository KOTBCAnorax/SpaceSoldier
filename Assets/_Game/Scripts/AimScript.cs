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
    [SerializeField] private GameObject _bulletHolePrefab;
    [SerializeField] private float _fireRate = 0.1f;

    private float _lastShotTime;
    private float _muzzleFlashDuration = 0.05f;

    private RaycastHit _raycastHit;

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
            _raycastHit = raycastHit;
        }

        if (_aimActive)
        {
            Vector3 direction = (_raycastHit.point - transform.position).normalized;
            BodyPartScript targetHit = _raycastHit.collider.gameObject.GetComponent<BodyPartScript>();
            if (targetHit != null) 
            {
                Shoot(_raycastHit);
                targetHit.RegisterHit();
            }

            _player.RotateCharacter(direction, 0f);
        }

        if (_muzzleFlash.enabled && Time.time - _lastShotTime > _muzzleFlashDuration)    
        {
            _muzzleFlash.enabled = false;
        }
    }

    private void Shoot(RaycastHit target)
    {
        if (Time.time - _lastShotTime > _fireRate)
        {
            _lastShotTime = Time.time;
            _muzzleFlash.enabled = true;
            Vector3 sparksDirection = (_player.transform.position - target.point).normalized;
            Instantiate(_bulletHolePrefab, target.point, Quaternion.LookRotation(sparksDirection, Vector3.up));
        }
    }
}
