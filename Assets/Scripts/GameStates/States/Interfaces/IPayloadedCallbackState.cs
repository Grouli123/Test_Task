namespace GameStates.States.Interfaces
{
  public interface IPayloadedCallbackState<TPayload, TCallback> : IPayloadedState<TPayload>
  {
    void Enter(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback);
  }
}