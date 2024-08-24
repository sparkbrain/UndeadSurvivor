using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CameraFollow : MonoBehaviour
{
    [Inject] private readonly Transform _playerTransform;
    [SerializeField] private Vector3 _cameraDistance;

    void Update()
    {
        if (_playerTransform == null)
        {
            return;
        }

        transform.position = _playerTransform.position + _cameraDistance;
    }
}