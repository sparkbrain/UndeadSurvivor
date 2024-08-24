using UnityEngine;

public class SpawnPickupOnDeath : SpawnOnDeathBase
{
    [SerializeField] private LootTable _lootTable;

    protected override void Spawn()
    {
        if (_lootTable.GetDrop(out GameObject loot))
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}