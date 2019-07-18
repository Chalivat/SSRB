using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : StateMachineBehaviour
{
    


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       animator.gameObject.GetComponent<BoxCollider>().enabled = true;
           Debug.Log("ACTIVATED : " + Time.frameCount);
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(stateInfo.normalizedTime +" : "+ stateInfo.length);
       
        if (stateInfo.normalizedTime >= stateInfo.length)
        {
            animator.gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("off :  " + Time.frameCount);
        }
        
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    
}
