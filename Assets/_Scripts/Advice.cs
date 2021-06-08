using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advice : MonoBehaviour
{
    [SerializeField] GameObject canva;
    bool IsTipShown;
    string tipContext = "Zabij wieśniaków, udaj się na pola uprawne na północny-zachód. Uważa na bohatera w wiosce.";
    


    private void OnTriggerEnter(Collider other)
    {
        if (!IsTipShown)
        {
            canva.SendMessage("ShowTip", tipContext, SendMessageOptions.DontRequireReceiver);
            IsTipShown = true;
        }
    }
}
