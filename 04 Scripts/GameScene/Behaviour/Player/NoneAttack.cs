﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneAttack : StateMachineBehaviour
{
    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Player>().NoneAttack();
    }
}
