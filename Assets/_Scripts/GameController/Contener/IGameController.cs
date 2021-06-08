using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController 
{
    void InitializeGameController();
    void UpdateGameController();
    void AddDragon(Dragon dragon);
    void AddPeasant(Peasant peasant);
}
