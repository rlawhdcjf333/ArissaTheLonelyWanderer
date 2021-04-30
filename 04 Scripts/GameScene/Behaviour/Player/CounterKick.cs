using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterKick : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EffectManager.instance.CallEffect("CounterBlast", animator.transform.position, Quaternion.identity);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider[] others = Physics.OverlapSphere(animator.transform.position, 5);

        foreach(Collider elem in others)
        {
           if(elem.transform.root.CompareTag("Enemy"))
           elem.transform.root.GetComponent<Rigidbody>().velocity = (elem.transform.position - animator.transform.position).normalized * 15f;
        }


    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Counter");
        animator.GetComponent<Player>().StateConvert(Player.State.Idle);
        animator.transform.Find("hitbox").GetComponent<PlayerHitbox>().hit = false;
    }
}
