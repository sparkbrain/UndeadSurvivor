using UnityEngine;

public class SpawnGameobjectOnDeath : SpawnOnDeathBase
{
    [SerializeField] private GameObject objectToSpawn;

    protected override void Spawn()
    {
        Instantiate(objectToSpawn);
    }
}