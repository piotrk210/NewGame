using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuState : BaseState
{

    public MenuState(IStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void InitializeState()
    {
        //menuView.SetStartButtonAction(() => SceneManager.LoadScene(SceneKeys.GAME));
       // menuView.SetExitButtonAction(() => Debug.Log("exit"));
        Debug.Log("menu");   
    }

    public override void UpdateState()
    {
    
    }

    public override void DisposeState()
    {
    
    }
}
