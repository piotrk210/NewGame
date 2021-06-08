﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
    }

}