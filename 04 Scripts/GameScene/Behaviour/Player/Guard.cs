using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : StateMachineBehaviour
{
    bool isCounter;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isCounter = false;

    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isCounter == false)
        {
            //if hit then counter
            if (animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().hit == true)
            {
                GameObject hitter = animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().hitter;

                isCounter = true;
                Vector3 toLook = new Vector3(hitter.transform.position.x, animator.transform.position.y, hitter.transform.position.z);
                animator.transform.LookAt(toLook, Vector3.up);
                animator.SetTrigger("Counter");
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isCounter == false)
        {
            animator.GetComponent<Player>().StateConvert(Player.State.Idle);
            animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().hit = false;
        }
    }
}
