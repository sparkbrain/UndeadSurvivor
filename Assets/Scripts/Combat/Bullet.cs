using UnityEngine;
using VContainer;

public struct BulletConfig
{
    public readonly Vector2 direction;
    public readonly float speed;
    public readonly float damage;
    public readonly float lifeTime;
    public readonly float poisonDamage;

    public BulletConfig(Vector2 direction, float speed, float damage, float poisonDamage, float lifeTime)
    {
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.poisonDamage = poisonDamage;
    }
}

public class Bullet : MonoBehaviour
{
    [Inject] private readonly BulletPool _bulletObjectPool;

    private BulletConfig _bulletConfig;
    private Rigidbody2D _rigidbody;

    private bool _alreadyHit;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth == null || _alreadyHit)
        {
            return;
        }

        _alreadyHit = true;

        if (_bulletConfig.poisonDamage > 0)
        {
            targetHealth.AddPoisonDamage(_bulletConfig.poisonDamage);
        }

        targetHealth.DoDamage(_bulletConfig.damage);

        ReturnToPool();
    }

    public void Fire(BulletConfig config)
    {
        CancelInvoke();

        _alreadyHit = false;
        _bulletConfig = config;
        _rigidbody.velocity = config.direction * config.speed;

        float angle = Mathf.Atan2(config.direction.y, config.direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Invoke(nameof(ReturnToPool), config.lifeTime);
    }

    private void ReturnToPool()
    {
        _bulletObjectPool.Pool.Release(this);
    }
}