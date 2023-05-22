using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePicker : StateMachineBehaviour
{
    [SerializeField] int numberOfAnimations = 3;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float index = (float)Random.Range(0, numberOfAnimations);
        animator.SetFloat("IdleIndex", index);
    }
}
