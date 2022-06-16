using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartScript : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _hitMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;

    private float _cooldown = 0.1f;
    private float _lastHitTime;


    public void RegisterHit()
    {
        _meshRenderer.material = _hitMaterial;
        _lastHitTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastHitTime > _cooldown)
        {
            ReturnToNormal();
        }
    }

    private void ReturnToNormal()
    {
        _meshRenderer.material = _defaultMaterial;
    }
}
