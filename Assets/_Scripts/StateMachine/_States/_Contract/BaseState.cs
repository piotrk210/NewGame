public abstract class BaseState
{
    protected readonly IStateMachine stateMachine;
    private bool isAvailable = true;

    protected BaseState(IStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void InitializeState()
    {
    }

    public virtual void UpdateState()
    {
    }
    public virtual void FixUpdateState()
    {
    }

    public virtual void DisposeState()
    {
    }

    public virtual void SetActive() 
    {
        isAvailable = true;
    }

    public virtual void SetInActive() 
    {
        isAvailable = false;
    }

    public virtual bool IsAvailable() 
    {
        return isAvailable;
    }

    public virtual void AddLeaf(BaseState leaf)
    {
    }

    public virtual void RemoveLeaf(BaseState leaf)
    {
    }

}