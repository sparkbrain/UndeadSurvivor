using System.Collections.Generic;
using UnityEngine;

public class RewardTable<T> : ScriptableObject
{
    [System.Serializable]
    public struct Reward
    {
        [SerializeField] public T item;
        [SerializeField] public float chance;
    }

    public List<Reward> possibleDrops;

    public virtual bool GetDrop(out T droppedItem)
    {
        float roll = Random.Range(0f, 100f);
        droppedItem = default;

        // If item A has 10% chance to drop, and item B 20% chance to drop,
        // 00<roll<10  yields item A,
        // 10<roll<20  yields item B,
        // 20<roll<100 yields nothing
        float range = 0;
        foreach (Reward reward in possibleDrops)
        {
            range += reward.chance;

            if (roll < range)
            {
                droppedItem = reward.item;
                return true;
            }
        }

        return false;
    }
}