using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Test : ITest, IInitializable
{
    public void Initialize()
    {
        Debug.Log("Hi zenjact!!");
    }
}
