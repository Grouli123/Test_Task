using System;
using UnityEngine;

namespace Animations
{
  public class SimpleAnimator : MonoBehaviour
  {
    [SerializeField] private Animator animator;

    public event Action Triggered; 

    public void SetBool(int animationHash, bool isSet) => 
      animator.SetBool(animationHash, isSet);

    public void SetFloat(int animationHash, float value) => 
      animator.SetFloat(animationHash, value);

    public void AnimationTriggered() => 
      Triggered?.Invoke();
  }
}