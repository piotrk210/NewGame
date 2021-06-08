using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Text tipText;
    [SerializeField] GameObject tipPanel;
 

    private void Awake()
    {
        tipText = GetComponentInChildren<Text>();
        tipPanel.SetActive(false);
    }



    void ShowTip (string tipContext)
    {
        tipPanel.SetActive(true);
        tipText.text = tipContext;
    }

    public void ExitButton()
    {
        tipPanel.SetActive(false);
    }


}
