using UnityEngine;

public abstract class SpawnOnDeathBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        GetComponent<Health>().OnDeathEvent += Spawn;
    }

    protected abstract void Spawn();
}
