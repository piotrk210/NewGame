using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unit, ISeletable
{
    public void SetSelected(bool selected)
    {
        //throw new System.NotImplementedException(); to do
        healtBar.gameObject.SetActive(selected);
    }

    void Command ( Vector3 destination)
    {
        nav.SetDestination(destination);
        task = Task.move;
        targetToFollow = null;
    }
    void Command(Dragon dragonToFollow)
    {
        targetToFollow = dragonToFollow.transform;
        task = Task.follow;
    }
    void Command(Hero heroToKill)
    {
        //to do
    }
}
