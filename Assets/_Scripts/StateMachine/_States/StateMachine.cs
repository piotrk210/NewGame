using System;
using Zenject;


public class StateMachine<TStartState> : IStateMachine where TStartState : BaseState
{
    private readonly DiContainer container;
    private BaseState currentBaseState;

    public StateMachine(DiContainer container)
    {
        this.container = container;
    }

    public void InitializeMethod()
    {
        CreateNewState<TStartState>();
    }

    public void TickMethod()
    {
        currentBaseState?.UpdateState();
    }

    public void FixedTickMethod()
    {
        currentBaseState?.FixUpdateState();
    }


    public void CreateNewState<TBaseState>()
        where TBaseState : BaseState
    {
        currentBaseState?.DisposeState();
        currentBaseState = container.Instantiate<TBaseState>();
        currentBaseState?.InitializeState();
    }

    public BaseState GetCurrentState()
    {
        return currentBaseState;
    }
}