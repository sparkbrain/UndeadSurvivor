using UnityEngine;

public abstract class SpawnOnDeath : MonoBehaviour
{
    protected virtual void Awake()
    {
        GetComponent<Health>().OnDeathEvent += Spawn;
    }

    protected abstract void Spawn();
}
