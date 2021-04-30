using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject hitter = animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().hitter;
        Vector3 toLook = new Vector3(hitter.transform.position.x, 0, hitter.transform.position.z);
        animator.transform.LookAt(toLook, Vector3.up);
        animator.GetComponent<Rigidbody>().velocity = animator.transform.forward * -20f;

        animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().LoseMoth();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Player>().StateConvert(Player.State.Idle);
        animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().hit = false;
    }
}
