using System;
using UnityEngine;
using VContainer;

public class EnemyMovement : Movement
{
    [Inject] private readonly Transform _playerTransform;

    private void Update()
    {
        if (_playerTransform == null)
        {
            return;
        }

        movementDirection = (_playerTransform.position - transform.position).normalized;
    }
}
