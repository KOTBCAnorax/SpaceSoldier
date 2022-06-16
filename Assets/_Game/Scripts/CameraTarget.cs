using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _targetY = 1.8f;
    [SerializeField] float _lerp = 0.2f;

    private void Update()
    {
        float _targetX = _player.position.x;
        float _targetZ = _player.position.z;

        Vector3 targetPosition = new Vector3(_targetX, _targetY, _targetZ);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _lerp);
    }
}
