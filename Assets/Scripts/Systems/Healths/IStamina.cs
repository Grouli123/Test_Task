namespace Systems.Healths
{
  public interface IStamina : IChangedValue
  {
    bool IsCanAttack();
    bool IsCanRoll();
    void WasteToAttack();
    void WasteToRoll();
  }
}