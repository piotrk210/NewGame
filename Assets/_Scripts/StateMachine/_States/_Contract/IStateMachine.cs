public interface IStateMachine
{
    void CreateNewState<TBaseState>() where TBaseState : BaseState;
    void InitializeMethod();
    void TickMethod();
    void FixedTickMethod();
    BaseState GetCurrentState();
}