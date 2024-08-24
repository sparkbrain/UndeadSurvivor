using UnityEngine;

public class SpawnPickupOnDeath : SpawnOnDeath
{
    [SerializeField] private LootTable lootTable;

    protected override void Spawn()
    {
        if (lootTable.GetDrop(out GameObject loot))
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
}