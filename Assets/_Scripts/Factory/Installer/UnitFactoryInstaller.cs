using UnityEditor;
using UnityEngine;
using Zenject;

public class UnitFactoryInstaller : MonoInstaller
{
    [SerializeField] private Dragon _dragon = null;
    [SerializeField] private Peasant _peasant = null;
    [SerializeField] private Transform _enemyContainer;

    public override void InstallBindings()
    {
        Container.BindFactory<UnitType, Unit, UnitFactoryPlaceholder>().
            FromFactory<UnitFactory>();
        Container.Bind<Unit>().FromInstance(_dragon).WhenInjectedInto<UnitFactory>();

    }
}