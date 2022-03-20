using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McTransition02Script : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerAttack.instance.isAttacking)
        {
            PlayerAttack.instance.myAnim.Play("McAttack03");
        }   
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttack.instance.isAttacking = false;   
    }
}
