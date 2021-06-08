using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static List<Dragon> DragonList { get { return gameController.dragonList; } }
    public static List<Peasant> PeasantList { get { return gameController.peasantList; } }

    [SerializeField] GameObject endPanel, tipPanel;

    Text endText;
    [SerializeField]CameraControl cameraControl;

    static GameController gameController;
    List<Dragon> dragonList = new List<Dragon>();
    List<Peasant> peasantList = new List<Peasant>();

    private void Awake()
    {
        gameController = this;
        endPanel.SetActive(false);
        endText = endPanel.GetComponentInChildren<Text>();    
    }

    private void Update()
    {
        TidyList(dragonList);
        TidyList(peasantList);

        if (dragonList.Count <= 0) Lose();
        else if (peasantList.Count <= 0) Win();
    }


    void TidyList<T>(List<T> list) where T : Unit
    {
        for (int i =0; i<list.Count;i++)
        {
            if(list[i] == null || !list[i].IsAlive)
            {
                list.RemoveAt(i--);
            }
        }
    }

    void Lose()
    {
        endText.text = "You lose ...";
        EndGame();
    }

    void Win ()
    {
        endText.text = "You win!";
        EndGame();
    }

    void EndGame ()
    {
        cameraControl.enabled = false;
        enabled = false;
        endPanel.SetActive(true);
        tipPanel.SetActive(false);
    }
}
