using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EnemyAttack : MonoBehaviour
{
    [Inject] private readonly Transform _playerTransform;

    [SerializeField] EnemyAttackSettings _attackSettings;

    private Health _playerHealthComponent;
    private float _attackCooldown = 0f;

    private void Awake()
    {
        _playerHealthComponent = _playerTransform.GetComponent<Health>();
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        if (_attackCooldown <= 0f)
        {
            var diff = transform.position - _playerHealthComponent.transform.position;

            if (diff.sqrMagnitude < Mathf.Pow(_attackSettings.range, 2))
            {
                Attack(_playerHealthComponent);
                _attackCooldown = _attackSettings.attackRate;
            }
        }
        else
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    private void Attack(Health target)
    {
        target.DoDamage(_attackSettings.attackDamage);
    }
}
