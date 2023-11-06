namespace StateMachines
{
  public abstract class BaseStateMachineState
  {
    protected int animationName;

    public abstract bool IsCanBeInterapted();

    public virtual void Enter() => 
      Check();

    public virtual void Check() {}

    public virtual void LogicUpdate(){}


    public virtual void PhysicsUpdate() => 
      Check();

    public virtual void Exit() { }

    public virtual void TriggerAnimation() { }
  }
}