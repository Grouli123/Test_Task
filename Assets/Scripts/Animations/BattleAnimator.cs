using System;

namespace Animations
{
  public class BattleAnimator : SimpleAnimator
  {
    public event Action Attacked;

    public void TriggerAttack() => 
      Attacked?.Invoke();
  }
}