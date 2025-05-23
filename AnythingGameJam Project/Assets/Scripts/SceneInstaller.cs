using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Zenject;
public class PlayerInstaller : MonoInstaller
{
    public PlayerStats _PlayerStats;
    public SpriteRenderer _spriteRenderer;
    public override void InstallBindings()
    {
        Container.Bind<FirstPersonMove>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<PlayerStats>()
            .FromInstance(_PlayerStats)
            .AsSingle()
            .NonLazy();
        Container.Bind<SpriteRenderer>()
            .FromInstance(_spriteRenderer)
            .AsCached()
            .NonLazy();

        Container.Bind<pCamera>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.Bind<EnemyActions>()
            .FromComponentInHierarchy()
            .AsCached();

        Container.Bind<EnemyHealth>()
            .FromComponentInHierarchy()
            .AsCached()
            .NonLazy();

    }
}