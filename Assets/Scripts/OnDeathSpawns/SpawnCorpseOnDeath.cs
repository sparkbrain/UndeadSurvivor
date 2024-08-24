using UnityEngine;

public class SpawnCorpseOnDeath : SpawnOnDeathBase
{
    [SerializeField] private GameObject _corpsePrefab;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    protected override void Spawn()
    {
        var spawnedObject = Instantiate(_corpsePrefab, transform.position, Quaternion.identity);
        spawnedObject.transform.rotation.eulerAngles.Set(0, _spriteRenderer.flipX ? 180 : 0, 0);
    }
}
