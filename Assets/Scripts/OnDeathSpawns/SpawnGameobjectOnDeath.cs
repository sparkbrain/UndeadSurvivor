using UnityEngine;

public class SpawnGameobjectOnDeath : SpawnOnDeathBase
{
    [SerializeField] private GameObject _objectToSpawn;

    protected override void Spawn()
    {
        Instantiate(_objectToSpawn);
    }
}