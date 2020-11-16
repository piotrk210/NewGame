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
}
