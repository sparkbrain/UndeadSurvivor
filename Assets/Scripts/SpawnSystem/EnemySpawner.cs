using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemySpawner : MonoBehaviour
{
    [Inject] private readonly IObjectResolver objectResolver;
    [Inject] private readonly Transform _player;

    [SerializeField] float _spawnRadius = 20f;
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float _spawnInterval;
    [SerializeField] float _spawnIntervalMultiplier = 1f;
    [SerializeField] float _spawnIntervalMultiplierIncreaseRate = .05f;

    private float spawnTimer = 0;

    private void Awake()
    {
        spawnTimer = _spawnInterval;
    }

    private void Update()
    {
        if(_player == null)
        {
            return;
        }

        spawnTimer -= Time.deltaTime * _spawnIntervalMultiplier;

        if(spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = _spawnInterval;
        }

        _spawnIntervalMultiplier += Time.deltaTime * _spawnIntervalMultiplierIncreaseRate;
    }

    private void SpawnEnemy()
    {
        var spawnDirection = Random.insideUnitCircle.normalized;
        var spawnPoint = (Vector2)_player.position + spawnDirection * _spawnRadius;

        objectResolver.Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
    }
}
