using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CameraFollow : MonoBehaviour
{
    [Inject] private readonly Transform playerTransform;
    [SerializeField] private Vector3 cameraDistance;

    void Update()
    {
        if (playerTransform == null)
        {
            return;
        }

        transform.position = playerTransform.position + cameraDistance;
    }
}