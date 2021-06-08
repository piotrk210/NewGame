using UnityEngine;
using Zenject;

public class UnitInstaller : MonoInstaller
{
    [SerializeField] private  Dragon dragon1 = null;
    [SerializeField] private Dragon dragon2 = null;
    [SerializeField] private Dragon dragon3 = null;

    [SerializeField] private Peasant peasant1 = null;
    [SerializeField] private Peasant peasant2 = null;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Dragon>().
            FromInstance(dragon1).AsSingle();

        Container.BindInterfacesAndSelfTo<Dragon>().
            FromInstance(dragon2).AsSingle();

        Container.BindInterfacesAndSelfTo<Dragon>().
            FromInstance(dragon3).AsSingle();

        Container.BindInterfacesAndSelfTo<Peasant>().
            FromInstance(peasant1).AsSingle();

        Container.BindInterfacesAndSelfTo<Peasant>().
            FromInstance(peasant2).AsSingle();
        //Container.BindInterfacesAndSelfTo<Test>().AsSingle();
    }
}