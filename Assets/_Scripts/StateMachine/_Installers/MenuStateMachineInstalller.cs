using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MenuStateMachineInstalller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StateMachine<MenuState>>().AsSingle();
    }
}
