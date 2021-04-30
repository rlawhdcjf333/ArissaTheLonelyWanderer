﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSkill : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().Skill();
    }
}
