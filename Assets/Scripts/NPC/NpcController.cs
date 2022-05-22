using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : Character
{
    Transform player;
    NavMeshAgent agent;

    [SerializeField]
    Transform[] patrolRoute;
    int currentLocation;


    [Header("Attacking Information")]
    [SerializeField]
    float attackRange;
    [SerializeField]
    float timePerAttack;

    float timer;

    [Header("Npc Stats")]
    [SerializeField]
    public float health;

    bool dead = false;
    public enum AttackState { attack, idle, patrol};
    public AttackState attackState = AttackState.idle;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().transform;
        currentLocation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Die();
        else
        {
            timer += Time.deltaTime;

            switch (attackState)
            {

                case AttackState.attack:
                    currentItem = equipableItems[0];
                    equipableItems[0].Draw();
                    agent.isStopped = false;
                    timer += Time.deltaTime;
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y , player.transform.position.z));
                    if (Vector3.Distance(transform.position, player.position) > attackRange + 0.1)
                    {

                        agent.isStopped = false;

                        agent.stoppingDistance = attackRange;
                        agent.speed = 10;
                        agent.SetDestination(player.position);
                    }
                    else
                    {
                        agent.isStopped = true;
                        Attack();
                    }
                    break;
                case AttackState.idle:
                    if (currentItem != null)
                        currentItem.Sheath();
                    agent.isStopped = true;
                    break;
                case AttackState.patrol:
                    if (currentItem != null)
                        currentItem.Sheath();
                    agent.isStopped = false;
                    if (Vector3.Distance(transform.position, patrolRoute[currentLocation].position) > 1)
                    {
                        agent.SetDestination(patrolRoute[currentLocation].position);
                    }
                    else
                    {
                        if (currentLocation < patrolRoute.Length - 1)
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
            anim.SetBool("Idle", agent.isStopped);
            anim.SetFloat("Speed", agent.speed);
        }
    }
    void EquipSword()
    {
        currentItem = equipableItems[0];
        currentItem.Equip();
    }

    void UnequipSword()
    {
        currentItem = equipableItems[0];
        currentItem.Unequip();
    }

    void Die()
    {
        if (!dead)
        {
            anim.ResetTrigger("Die");
            anim.SetTrigger("Die");
            dead = true;
        }
    }
    void Attack()
    {
        if (timer > timePerAttack)
        {
            currentItem.Attack();
            timer = 0;
        }
    }

    public override void TakeHit(float damage)
    {
        anim.ResetTrigger("TakeHit");
        anim.SetTrigger("TakeHit");

        health -= damage;
        timer = 0;
    }
}
