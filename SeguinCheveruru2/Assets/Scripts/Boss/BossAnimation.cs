using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : Singleton<BossAnimation>
{
    public enum BossAnim { GroundIdle, StandingIdle, Run, WindupOne, WindupTwo, WindupThree, Attack, StandingRun, Throw }

    [SerializeField] private Animator anim = null;

    public void Animate(BossAnim animation)
    {
        switch (animation)
        {
            case BossAnim.GroundIdle:
                anim.SetTrigger("GroundIdle");
                break;
            case BossAnim.StandingIdle:
                anim.SetTrigger("StandingIdle");
                break;
            case BossAnim.StandingRun:
                anim.SetTrigger("StandingRun");
                break;
            case BossAnim.Run:
                anim.SetTrigger("Run");
                break;
            case BossAnim.WindupOne:
                anim.SetTrigger("WindupOne");
                break;
            case BossAnim.WindupTwo:
                anim.SetTrigger("WindupTwo");
                break;
            case BossAnim.WindupThree:
                anim.SetTrigger("WindupThree");
                break;
            case BossAnim.Attack:
                anim.SetTrigger("Attack");
                break;
            case BossAnim.Throw:
                anim.SetTrigger("Throw");
                break;

            default:
                break;
        }
    }

    public void ToggleAnimation(bool toggle)
    {
        anim.speed = toggle ? 1 : 0;
    }
}
