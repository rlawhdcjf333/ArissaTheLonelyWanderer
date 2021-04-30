using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdrawBehaviour : StateMachineBehaviour
{
    //오버드로우 모션 진행 중 화살 오브젝트 활성

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //만약에 짧은 공격이 아니면
        if(animator.GetBool("RecoilTrigger") == false)
        {
            //만약에 1인칭 시점이 아니라면
            if(Camera.main.GetComponent<FollowCamera>().CameraMode == FollowCamera.Mode.Follow)
            //움직일 수 없음
            animator.GetComponent<Player>().canMove = false;
            //오버드로우 이펙트 활성
            Vector3 targetPos = GameObject.FindGameObjectWithTag("Aim").transform.position;
            EffectManager.instance.CallEffect("PowerDraw", targetPos, Quaternion.identity);
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         animator.GetComponent<Player>().DrawArrow();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //움직임 재활성
        animator.GetComponent<Player>().canMove = true;
        //오버드로우 이펙트 해제
        EffectManager.instance.CutEffect("PowerDraw");
    }


}
