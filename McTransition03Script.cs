using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McTransition03Script : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayerAttack.instance.isAttacking)
        {
            PlayerAttack.instance.myAnim.Play("McAttack04");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttack.instance.isAttacking = false;   
    }
}
