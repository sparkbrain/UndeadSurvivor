using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VContainer;

public class Health : MonoBehaviour
{
    [Inject] private readonly AudioManager _audioManager;

    public delegate void DeathEvent();
    public event DeathEvent OnDeathEvent;

    [SerializeField] Slider m_healthBarSlider;
    [SerializeField] HealthSettings _healthSettings;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _poisonColor;

    private float _maxHealth;
    private float _health;

    private float _poisonDamage;
    private float _poisonDamageTimer;

    private Animator _animator;

    private void Awake()
    {
        _maxHealth = _healthSettings.health;
        _health = _maxHealth;

        if (m_healthBarSlider != null) m_healthBarSlider.value = _health / _maxHealth;

        _animator = _animator != null ? _animator : GetComponent<Animator>();
        _spriteRenderer = _spriteRenderer != null ? _spriteRenderer : GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_poisonDamage > 0)
        {
            _poisonDamageTimer += Time.deltaTime;

            if (_poisonDamageTimer > 1)
            {
                _poisonDamageTimer = 0;
                //DoDamage(_poisonDamage);
            }
        }
    }

    public void DoDamage(float amount)
    {
        _health = Mathf.Clamp(_health - amount, 0, _maxHealth);

        if (_health <= 0)
        {
            Kill();
            return;
        }
        if (_animator != null) { _animator.SetTrigger("Hit"); }
        if (m_healthBarSlider != null) { SetHealthBarValue(); }
    }

    private void Kill()
    {
        OnDeathEvent.Invoke();
        _audioManager.PlayDeathSound();

        Destroy(gameObject);
    }

    private void SetHealthBarValue()
    {
        m_healthBarSlider.value = (float)_health / _maxHealth;
    }

    public void RestoreHealth(float amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, _maxHealth);
        SetHealthBarValue();
    }

    public void AddPoisonDamage(float poisonDamage)
    {
        if (_poisonDamage == 0)
        {
            _spriteRenderer.color = _poisonColor;
        }

        _poisonDamage += poisonDamage;
    }
}
