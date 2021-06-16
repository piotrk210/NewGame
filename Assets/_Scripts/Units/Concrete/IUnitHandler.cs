using System;
using System.Collections.Generic;

public interface IUnitHandler 
{
    List<Dragon> DragonList { get; }
    List<Peasant> PeasantList { get; }
    void InitializeGameController();
    void UpdateGameController();
    void AddDragon(Dragon dragon);
    void AddPeasant(Peasant peasant);

    void SetLoseAction(Action _loseAction);
    void SetWinAction(Action _winAction);
}