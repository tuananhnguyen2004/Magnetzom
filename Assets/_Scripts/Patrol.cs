using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    [SerializeField] private float timeBetweenMoves;
    [SerializeField] private float minPatrolDistance;
    [SerializeField] private float maxPatrolDistance;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float obstacleDetectionRadius;
    [SerializeField] private LayerMask collidedLayers;

    private bool canPatrol;
    private Vector2 nextPatrolPoint;
    private float counter;
    private Vector2 patrolVector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextPatrolPoint = animator.transform.position;
        counter = timeBetweenMoves;
        canPatrol = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canPatrol)
        {
            canPatrol = false;
            var randX = Random.Range(-1f, 1f);
            var randY = Random.Range(-1f, 1f);
            var randDistance = Random.Range(minPatrolDistance, maxPatrolDistance);

            patrolVector = new Vector2(randX, randY).normalized * randDistance;
            nextPatrolPoint = (Vector2)animator.transform.position + patrolVector;
        }
        else
        {
            var detection = DetectObstacle(animator.transform.position, animator.transform.right, collidedLayers);
            if (detection)
            {
                Debug.Log("collided!!");
                var oppDirection = (Vector2) animator.transform.position - detection.point;
                nextPatrolPoint = (Vector2)animator.transform.position + oppDirection;
            }
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, nextPatrolPoint, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance((Vector2)animator.transform.position, nextPatrolPoint) < .2f)
        {
            animator.SetBool("isStationed", true);
            if (counter <= 0)
            {
                counter = timeBetweenMoves;
                canPatrol = true;
                animator.SetBool("isStationed", false);
            }
            else
            {
                counter -= Time.deltaTime;
            }
        }
    }

    private RaycastHit2D DetectObstacle(Vector2 origin, Vector2 direction, LayerMask layer)
    {
        return Physics2D.CircleCast(origin, obstacleDetectionRadius, direction, 0f, layer);
    }
}