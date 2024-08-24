using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] EnemyAttackSettings _attackSettings;
    [Inject] Transform _playerTransform;

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

            if (diff.sqrMagnitude < Mathf.Pow(_attackSettings.Range, 2))
            {
                Attack(_playerHealthComponent);
                _attackCooldown = _attackSettings.AttackRate;
            }
        }
        else
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    private void Attack(Health target)
    {
        target.DoDamage(_attackSettings.AttackDamage);
    }
}
