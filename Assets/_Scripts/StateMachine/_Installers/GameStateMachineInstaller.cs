using Zenject;

public class GameStateMachineInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StateMachine<GameState>>().AsSingle();
    }

}
