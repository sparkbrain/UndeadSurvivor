using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
using VContainer;

public class EnemyMovement : Movement
{
    [Inject] private readonly Transform _playerTransform;
    [SerializeField] float _physicsCheckRadius = .6f;
    [SerializeField] float _nudgeDistance = .05f;
    [SerializeField] int _counterThing = 5;

    Collider2D[] _hits = new Collider2D[10];
    ContactFilter2D _contactFilter = new ContactFilter2D();
    int _counter = 0;

    private void Update()
    {
        if (_playerTransform == null)
        {
            return;
        }

        _counter++;

        if(_counter == _counterThing) 
        {
            _counter = 0;

            int hitCount = Physics2D.OverlapCircle(transform.position, _physicsCheckRadius, _contactFilter.NoFilter(), _hits);;
            for (int i = 0; i < hitCount; i++)
            {
                Collider2D hit = _hits[i];
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                hit.transform.position += (direction * _nudgeDistance);
            }

            movementDirection = (_playerTransform.position - transform.position).normalized;
        }
    }
}
