using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSounds : StateMachineBehaviour
{
    public float timeBetweenSteps;
    private float remainingTimeBetweenSteps;
    private AudioSource audioSrc;
    public List<AudioClip> steps;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioSrc == null)
        {
            audioSrc = animator.GetComponent<AudioSource>();
        }

        remainingTimeBetweenSteps = 0.25f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        remainingTimeBetweenSteps -= Time.deltaTime;
        if (remainingTimeBetweenSteps <= 0)
        {
            audioSrc.PlayOneShot(steps.GetRandom());
            remainingTimeBetweenSteps = timeBetweenSteps;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
