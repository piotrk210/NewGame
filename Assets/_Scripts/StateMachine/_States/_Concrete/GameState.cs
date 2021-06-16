using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameState : BaseState
{
    private readonly IUnitHander gameController;
    
    public GameState(IStateMachine stateMachine,
        IUnitHander _gameController)
        : base(stateMachine)
    {
        gameController = _gameController;
    }


    public override void InitializeState()
    {

    }

    public override void UpdateState()
    {
        
    }

    public override void FixUpdateState()
    {
    }

    public override void DisposeState()
    {

    }

}