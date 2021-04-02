using Zenject;

public class CarleyCorpInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<CarleyCorpSignal>();
        Container.Bind<Bee>().FromComponentsInHierarchy().AsCached();
        Container.Bind<Flower>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<SimulationData>().FromNew().AsSingle();
    }
}
