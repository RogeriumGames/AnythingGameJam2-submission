using UnityEditor;
using UnityEngine;
using Zenject;
public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {


        Container.Bind<PlayerStats>().FromComponentInHierarchy().AsSingle();
        Container.Bind<pCamera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EnemyActions>().FromComponentInHierarchy().AsCached();
        Container.Bind<EnemyHealth>().FromComponentInHierarchy().AsCached().NonLazy();

    }
}