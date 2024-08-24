using System;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] MovementSettings _movementSettings;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Animator _animator;

    private float _movementSpeed;
    protected Vector2 movementDirection;

    protected virtual void Awake()
    {
        if (_movementSettings == null)
        {
            throw new ArgumentNullException($"No movement settings set on {transform.name}");
        }

        _movementSpeed = _movementSettings.moveSpeed;
    }

    protected virtual void LateUpdate()
    {
        Move(movementDirection);
        if (_renderer != null) { SetSpriteDirection(movementDirection); }
    }

    protected virtual void Move(Vector2 direction)
    {
        if (_animator != null)
        {
            _animator.SetBool("Moving", direction.sqrMagnitude > .025f);
        }

        transform.Translate(direction * (_movementSpeed * Time.deltaTime));
    }

    protected virtual void SetSpriteDirection(Vector2 movementDirection)
    {
        _renderer.flipX = movementDirection.x < 0;
    }

    public virtual void IncreaseMovementSpeed(float amount) => _movementSpeed += amount;
}