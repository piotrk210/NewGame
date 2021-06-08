using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartState : BaseState
{


    public StartState(IStateMachine stateMachine)
        : base(stateMachine)
    {
    }


    public override void InitializeState()
    {
        stateMachine.CreateNewState<GameState>();
    }

    public override void UpdateState()
    {

    }

    public override void DisposeState()
    {
    }
}