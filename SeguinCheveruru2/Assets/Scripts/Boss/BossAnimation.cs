using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : Singleton<BossAnimation>
{
    public enum BossAnim { GroundIdle, StandingIdle, Run }

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
            case BossAnim.Run:
                anim.SetTrigger("Run");
                break;
            default:
                break;
        }
    }
}
