using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArrowBehavior : StateMachineBehaviour
{
    //드로우 모션이 실행중일 때 특정 시간 이후로 화살 오브젝트 활성화
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 0.3f) animator.GetComponent<Player>().DrawArrow();

    }


}
