using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttack : StateMachineBehaviour
{
    GameObject m_target;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_target = animator.GetComponent<Enemy>().target;
        animator.GetComponent<Enemy>().TurnAttckTrigger(true);
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //공격 모션 중 방향 추적, 일어나는 모션까지만
       if( stateInfo.normalizedTime < 0.3f)
       {
            if (m_target != null)
            {
                Vector3 targetVec = (m_target.transform.position - animator.transform.position).normalized;

                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(targetVec), Time.deltaTime * 5);
            }
       }
        if (stateInfo.normalizedTime == 0.3f)
        {
            animator.GetComponent<Enemy>().Thrust();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().TurnAttckTrigger(false);
        animator.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
}
