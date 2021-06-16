using UnityEditor;
using UnityEngine;
using Zenject;

public class UnitFactory : IFactory<UnitType, Unit>
{
    private readonly Dragon dragon = null;
    private readonly Peasant peasant = null;
    private readonly DiContainer container;


    protected UnitFactory(DiContainer _container, Dragon _dragon, Peasant _peasant)
    {
        peasant = _peasant;
        container = _container;
        dragon = _dragon;
    }


    public Unit Create(UnitType unitType) 
    {
        if(unitType == UnitType.Dragon)
        {
            var obj = container.InstantiatePrefabForComponent<Dragon>(dragon);
            return obj;
        } else
        {
            var obj = container.InstantiatePrefabForComponent<Peasant>(peasant);
            return obj;
        }
    }

}