using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class BulletPool : MonoBehaviour
{
    [Inject] private readonly IObjectResolver _objectResolver;

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] int _poolDefaultCapacity = 10;
    [SerializeField] int _poolMaxCapacity = 100;

    public IObjectPool<Bullet> Pool { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<Bullet>(
            createFunc: PoolOnCreate,
            actionOnGet: PoolOnGet,
            actionOnRelease: PoolOnRelease,
            actionOnDestroy: PoolOnDestroy,
            collectionCheck: false,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxCapacity
        );
    }

    private Bullet PoolOnCreate()
    {
        GameObject go = _objectResolver.Instantiate(_bulletPrefab, transform);
        Bullet bullet = go.GetComponent<Bullet>();
        return bullet;
    }

    private void PoolOnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void PoolOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void PoolOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
