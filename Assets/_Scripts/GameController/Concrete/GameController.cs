﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : IGameController
{

    public List<Dragon> DragonList => dragonList;
    public List<Peasant> PeasantList  => peasantList; 

    private List<Dragon> dragonList = new List<Dragon>();
    private List<Peasant> peasantList = new List<Peasant>();

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
        for (int i =0; i<list.Count;i++)
        {
            if(list[i] == null || !list[i].IsAlive)
            {
                list.RemoveAt(i--);
            }
        }
    }

    private void Lose()
    {
        EndGame();
    }

    private void Win ()
    {
        EndGame();
    }

    private void EndGame ()
    {
    }
}
