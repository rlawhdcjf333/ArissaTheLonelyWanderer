using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilBehavior : StateMachineBehaviour
{

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //화살 오브젝트 해제 
        if (stateInfo.normalizedTime == 0.3f)
        {
            animator.GetComponent<Player>().ReleaseArrow();
        }
    }

   
}
