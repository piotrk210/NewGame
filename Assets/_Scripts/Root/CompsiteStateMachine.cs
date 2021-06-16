using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CompsiteStateMachine : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Root>().AsSingle();
    }
}
