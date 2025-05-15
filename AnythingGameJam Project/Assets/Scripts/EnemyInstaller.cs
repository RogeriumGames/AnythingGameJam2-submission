using System.ComponentModel;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.Bind<EnemyHealth>().FromComponentInHierarchy().AsTransient().NonLazy(); 
        Container.Bind<EnemyActions>().FromComponentsOnRoot().AsTransient().NonLazy();
    }
}
