using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advice : MonoBehaviour
{
    [SerializeField] GameObject canva;
    bool IsTipShown;
    string tipContext = "Zabij wieśniaków";
    


    private void OnTriggerEnter(Collider other)
    {
        if (!IsTipShown)
        {
            canva.SendMessage("ShowTip", tipContext, SendMessageOptions.DontRequireReceiver);
            IsTipShown = true;
        }
    }
}
