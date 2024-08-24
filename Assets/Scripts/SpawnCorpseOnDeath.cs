using UnityEngine;

public class SpawnCorpseOnDeath : SpawnOnDeath
{
    [SerializeField] private GameObject corpsePrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected override void Spawn()
    {
        var spawnedObject = Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        spawnedObject.transform.rotation.eulerAngles.Set(0, spriteRenderer.flipX ? 180 : 0, 0);
    }
}
