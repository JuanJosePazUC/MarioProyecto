using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingBehaviour : StateMachineBehaviour
{

    private Patrol patrol;
    public float speed;
    private int randomSpot;

    //Start
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol = GameObject.FindGameObjectWithTag("Enemie").GetComponent<Patrol>();

        randomSpot = Random.Range(0, patrol.moveSpots.Length);
    }

    // Update
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(animator.transform.position, patrol.moveSpots[randomSpot].position) > 0.2f){
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrol.moveSpots[randomSpot].position, speed * Time.deltaTime);
        }else{
            randomSpot = Random.Range(0, patrol.moveSpots.Length);
        }
    }

    // Stops
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
