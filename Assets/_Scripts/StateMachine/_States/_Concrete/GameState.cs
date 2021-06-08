using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameState : BaseState
{
    private IGameController gameController;
    public GameState(IStateMachine stateMachine,
        IGameController _gameController)
        : base(stateMachine)
    {
        gameController = _gameController;
    }


    public override void InitializeState()
    {

    }

    public override void UpdateState()
    {
        gameController.UpdateGameController();
    }

    public override void FixUpdateState()
    {
    }

    public override void DisposeState()
    {

    }

}