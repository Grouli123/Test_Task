using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.Input
{
  public class InputService : IInputService
  {
    private readonly HeroControls input;
    private Mouse currentMouse;

    public InputService(HeroControls inputMap)
    {
      input = inputMap;
    }

    public void Enable()
    {
      input.Enable();
      currentMouse = Mouse.current;
    }

    public void Disable() => 
      input.Disable();

    public Vector2 Axis => 
      SimpleInputAxis();

    public Vector2 ClickPosition => 
      MousePosition();

    public bool IsAttackButtonDown() => 
      input.Player.Attack.triggered;

    public bool IsRollButtonDown() => 
      input.Player.Roll.triggered;

    public bool IsBlockButtonPressed() => 
      input.Player.Block.IsPressed();

    private Vector2 SimpleInputAxis() => 
      input.Player.Move.ReadValue<Vector2>();

    private Vector2 MousePosition() => 
      input.Player.Mouse.ReadValue<Vector2>();
  }
}