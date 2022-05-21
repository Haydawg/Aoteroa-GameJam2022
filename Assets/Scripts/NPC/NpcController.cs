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
    Weapon weapon;

    [Header("Attacking Information")]
    [SerializeField]
    float attackRange;
    [SerializeField]
    float timePerAttack;

    float timer;

    [Header("Npc Stats")]
    [SerializeField]
    public float health;

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
        if (health <= 0)
            Die();
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

        animator.SetFloat("Speed", agent.velocity.z);
    }

    void Die()
    {
        animator.SetTrigger("Die");
    }
    void Attack()
    {
        if (timer > timePerAttack)
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("attack");
            timer = 0;
        }
    }

    public void TakeHit(float damage)
    {
        animator.ResetTrigger("takeHit");
        animator.SetTrigger("takeHit");

        health -= damage;
        timer = 0;
    }
}
