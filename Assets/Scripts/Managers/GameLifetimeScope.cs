using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] BulletPool bulletPool;
    [SerializeField] Transform playerTransform;
    [SerializeField] AudioManager audioManager;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(bulletPool);
        builder.RegisterInstance(audioManager);
        builder.RegisterInstance(playerTransform);
    }
}