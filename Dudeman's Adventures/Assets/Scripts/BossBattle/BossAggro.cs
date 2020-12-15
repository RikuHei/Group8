using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAggro : StateMachineBehaviour
{

    // castpoint Obj & transform castpoint
    GameObject castPointObj;
    Transform castPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // set castpoint obj into transform
        castPointObj = GameObject.Find("Castpoint");
        castPoint = castPointObj.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // cast point distance
        float castDist = 20f;

        // borrowed some code from AI aggro to make the boss gain movement speed
        // when player is in x range from him
        int maskPlayer = 1 << LayerMask.NameToLayer("Player");
        int maskTerrain = 1 << LayerMask.NameToLayer("Terrain");
        int combinedMask = maskPlayer | maskTerrain;
        Vector2 endPos = castPoint.position + Vector3.left * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, combinedMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                animator.SetBool("Aggro", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
