using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PlayerFire : MonoBehaviour
{
    [Inject] private readonly BulletPool _bulletObjectPool;
    [Inject] private readonly AudioManager _audioManager;

    [SerializeField] private PlayerShootingSettings _attackSettings;
    [SerializeField] private SpriteRenderer _playerRenderer;
    [SerializeField] private SpriteRenderer _weaponRenderer;
    [SerializeField] private Transform _rangeIndicator;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Text _ammoText;

    private Transform _target;
    private float _firingCooldown = 0f;

    private float _fireRange;
    private float _fireRate;
    private float _damage;
    private float _poisonDamage;
    private float _bulletTravelSpeed;

    private int _currentAmmo;

    private void Awake()
    {
        _fireRange = _attackSettings.range;
        _fireRate = _attackSettings.fireRate;
        _damage = _attackSettings.damage;
        _bulletTravelSpeed = _attackSettings.bulletTravelSpeed;
        _currentAmmo = _attackSettings.startingAmmo;

        UpdateRangeIndicator();
        UpdateAmmoIndicator();
    }

    private void Update()
    {
        _firingCooldown -= Time.deltaTime * _fireRate;
        _target = TryFindTarget();

        if(_currentAmmo <= 0) { return; }

        if (_target != null)
        {
            AimAtTarget();

            if (_firingCooldown <= 0f)
            {
                FireAtTarget();
                _firingCooldown = 1;

                _currentAmmo--;
                UpdateAmmoIndicator();
            }
        }
    }

    private void UpdateRangeIndicator()
    {
        _rangeIndicator.transform.localScale = Vector3.one * _fireRange * 2;
    }

    private void UpdateAmmoIndicator()
    {
        _ammoText.text = _currentAmmo.ToString();
    }

    public void GiveAmmo(int amount)
    {
        _currentAmmo += amount;
        UpdateAmmoIndicator();
    }

    public void IncreaseFireRange(float amount)
    {
        _fireRange += amount;
        UpdateRangeIndicator();
    }

    public void IncreaseFireRate(float amount) => _fireRate += amount;

    private Transform TryFindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _fireRange, _targetLayerMask);

        if (hits.Length == 0)
        {
            return null;
        }

        Transform target = null;
        float closestTarget = float.MaxValue;
        foreach (var hit in hits)
        {
            Vector2 diff = hit.transform.position - transform.position;
            float sqrDistance = diff.sqrMagnitude;

            if (sqrDistance < closestTarget)
            {
                closestTarget = sqrDistance;
                target = hit.transform;
            }
        }

        return target;
    }

    private void FireAtTarget()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;

        Bullet bullet = _bulletObjectPool.Pool.Get();
        bullet.transform.position = transform.position;
        bullet.Fire(new BulletConfig(direction, _bulletTravelSpeed, _damage, _poisonDamage, 5));
    }

    private void AimAtTarget()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;

        bool faceRight = direction.x > -.05f;
        _weaponRenderer.flipX = faceRight;
        _playerRenderer.flipX = !faceRight;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = faceRight ? angle: angle + 180 ;

        Vector3 weaponPosition = _weaponRenderer.transform.localPosition;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _weaponRenderer.transform.SetLocalPositionAndRotation(weaponPosition, newRotation);
    }

    public void IncreasePoisonDamage(float amount)
    {
        _poisonDamage += amount;
    }
}
