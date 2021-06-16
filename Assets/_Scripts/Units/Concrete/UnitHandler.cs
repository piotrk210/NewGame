using System;
using System.Collections.Generic;

public class UnitHandler : IUnitHandler
{
    public List<Dragon> DragonList => dragonList;
    public List<Peasant> PeasantList => peasantList;

    private readonly List<Dragon> dragonList = new List<Dragon>();
    private readonly List<Peasant> peasantList = new List<Peasant>();

    private Action winAction;
    private Action loseAction;

    public void AddDragon(Dragon dragon)
    {
        dragonList.Add(dragon);
    }

    public void AddPeasant(Peasant peasant)
    {
        peasantList.Add(peasant);
    }

    public void InitializeGameController()
    {
    }

    public void UpdateGameController()
    {
        TidyList(dragonList);
        TidyList(peasantList);

        if (dragonList.Count <= 0) Lose();
        else if (peasantList.Count <= 0) Win();
    }

    private void TidyList<T>(List<T> list) where T : Unit
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null || !list[i].IsAlive)
            {
                list.RemoveAt(i--);
            }
        }
    }

    private void Lose()
    {
        loseAction();
    }

    private void Win()
    {
        winAction();
    }

    public void SetLoseAction(Action _loseAction)
    {
        loseAction = _loseAction;
    }

    public void SetWinAction(Action _winAction)
    {
        winAction = _winAction;
    }
}