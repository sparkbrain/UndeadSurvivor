using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemySpawner : MonoBehaviour
{
    [Inject] private readonly IObjectResolver objectResolver;
    [Inject] private readonly Transform _player;

    [SerializeField] float spawnRadius = 20f;
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float spawnInterval;
    [SerializeField] float spawnIntervalMultiplier = 1f;
    [SerializeField] float spawnIntervalMultiplierIncreaseRate = .05f;

    private float spawnTimer = 0;

    private void Awake()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        if(_player == null)
        {
            return;
        }

        spawnTimer -= Time.deltaTime * spawnIntervalMultiplier;

        if(spawnTimer <= 0)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }

        spawnIntervalMultiplier += Time.deltaTime * spawnIntervalMultiplierIncreaseRate;
    }

    private void SpawnEnemy()
    {
        var spawnDirection = Random.insideUnitCircle.normalized;
        var spawnPoint = (Vector2)_player.position + spawnDirection * spawnRadius;

        objectResolver.Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
    }
}
