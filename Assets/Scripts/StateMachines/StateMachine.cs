namespace StateMachines
{
  public class StateMachine
  {
    private BaseStateMachineState currentState;

    public BaseStateMachineState State => currentState;
        
    private void ExitState()
    {
      currentState.Exit();
    }

    public void Initialize(BaseStateMachineState state)
    {
      currentState = state;
      currentState.Enter();
    }

    public void ChangeState(BaseStateMachineState newState)
    {
      ExitState();
      Initialize(newState);
    }
  }
}