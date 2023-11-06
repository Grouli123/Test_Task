using System;

namespace Services.PlayerData
{
  public class PlayerScore
  {
    public int Score { get; private set; }

    public event Action Changed;

    public void IncScore(int addedValue)
    {
      Score += addedValue;
      NotifyAboutChange();
    }

    public void ResetScore() => 
      Score = 0;

    private void NotifyAboutChange() => 
      Changed?.Invoke();
  }
}