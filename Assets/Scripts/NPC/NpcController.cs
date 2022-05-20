using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    Animator animator;
    [SerializeField]
    Transform[] patrolRoute;
    int currentLocation;
    [SerializeField]
    GameObject weapon;

    [Header("Attacking Information")]
    [SerializeField]
    float attackRange;
    [SerializeField]
    float timePerAttack;

    float timer;

    [Header("Npc Stats")]
    [SerializeField]
    float health;

    public enum AttackState { attack, idle, patrol};
    public AttackState attackState = AttackState.idle;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        currentLocation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        switch (attackState)
        {
            case AttackState.attack:
                timer += Time.deltaTime;
                transform.LookAt(player);
                if (Vector3.Distance(transform.position, player.position) > attackRange)
                {
                    animator.SetBool("isRunning", true);
                    agent.SetDestination(player.position);
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    Attack();
                }
                break;
            case AttackState.idle:
                agent.isStopped = true;
                break;
            case AttackState.patrol:
                if (Vector3.Distance(transform.position, patrolRoute[currentLocation].position) > 1)
                {
                    agent.SetDestination(patrolRoute[currentLocation].position);
                }
                else
                {
                    if(currentLocation < patrolRoute.Length)
                    {
                        currentLocation++;
                    }
                    else
                    {
                        currentLocation = 0;
                    }
                }
                break;

        }
    }

    void Attack()
    {
        if (timer > timePerAttack)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
            timer = 0;
        }
    }
}
