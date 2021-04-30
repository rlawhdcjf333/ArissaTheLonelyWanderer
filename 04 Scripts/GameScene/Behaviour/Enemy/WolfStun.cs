using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStun : StateMachineBehaviour
{

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //타겟 마킹 복구 == 스턴 중일때는 열심히 때려도 안 맞는다는 그런 뜻
        animator.GetComponent<Enemy>().RestoreStun();

        //어미인 경우에만 스턴 끝날때 스킬 트리거 
        if (animator.GetComponent<Enemy>().isMother) animator.SetTrigger("skill");
        //새끼는 컷
        else animator.GetComponent<Enemy>().Die();
    }
}
