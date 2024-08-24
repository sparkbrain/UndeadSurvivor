using UnityEngine;

public class SpawnGameobjectOnDeath : SpawnOnDeath
{
    [SerializeField] private GameObject objectToSpawn;

    protected override void Spawn()
    {
        Instantiate(objectToSpawn);
    }
}