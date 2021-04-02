using UnityEngine;
using Zenject;

public class CarleyCorpInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Bee>().FromComponentsInHierarchy().AsCached();
        Container.Bind<Flower>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SimulationData>().FromNew().AsSingle();
    }
}