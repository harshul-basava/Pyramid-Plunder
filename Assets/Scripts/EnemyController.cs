using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public GameObject bullet;

    public LayerMask groundMask;
    public LayerMask playerMask;
    public LayerMask wallMask;

    public bool flip;
    public Animator animator;
    public Scorekeeper scorekeeper;

    // patrolling
    public Vector2 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    // attacking
    public float timeBetweenAttacks;
    public float timeBetweenRangedAttacks;
    private bool alreadyAttacked;
    private bool alreadyRangeAttacked;

    //States
    public float sightRange;
    public float attackRange;
    private bool playerInSightRange;
    private bool playerInAttackRange;

    // combat
    public float health;
    public float damage;

    private bool isMoving;
    private bool isAlive;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.Find("Player").transform;
        scorekeeper = GameObject.FindGameObjectWithTag("Scorekeeper").GetComponent<Scorekeeper>();
        alreadyAttacked = false;
        alreadyRangeAttacked = false;

        isAlive = true;
        animator.SetBool("isAlive", isAlive);
    }

    private void Update()
    {
        // check for sight and attack range
        playerInSightRange = Physics2D.OverlapBox(transform.position, new Vector2(sightRange, 8 * transform.localScale.y), 0f, playerMask);
        playerInAttackRange = Physics2D.OverlapBox(transform.position, new Vector2(attackRange, 4 * transform.localScale.y), 0f,  playerMask);

        if (!playerInSightRange && !playerInAttackRange) {
            Patrolling();
            isMoving = true;
        } 

        if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
            isMoving = true;
        }

        if (playerInAttackRange && playerInSightRange) {
            AttackPlayer();
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isAlive", isAlive);
    }

    private void Patrolling() 
    {
        if (!walkPointSet) {
            SearchWalkPoint();
        } else {

            agent.SetDestination(walkPoint);
            
            if (walkPoint.x < transform.position.x)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            } 
            else 
            {
                transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }

        float distanceToWalkPoint = Vector2.Distance(new Vector2 (transform.position.x, transform.position.y), walkPoint);
        
        if (distanceToWalkPoint < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);

        if (Physics2D.OverlapCircle(walkPoint, 0.075f, groundMask))
        {
            if (!Physics2D.OverlapCircle(walkPoint, 0.3f, wallMask))
            {
                walkPointSet = true;
            }
            else
            {
                // Debug.Log("Hit a Wall!");
            }
        }
    }

    private void ChasePlayer() 
    {
        FlipSprite();

        agent.SetDestination(player.position);
        
        RangedAttackPlayer();
    }

    private void AttackPlayer() 
    {
        FlipSprite();
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {

            animator.SetTrigger("Attack");
            if (Physics2D.OverlapCircle(transform.position, attackRange,  playerMask)) 
            {
                HealthManager hm = GameObject.Find("HealthManager").GetComponent<HealthManager>();
                hm.DealDamage(GameObject.Find("Player"), damage);
            } 
            else 
            {
                // Debug.Log("Enemy Missed!");
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), Random.Range(timeBetweenAttacks - 0.5f, timeBetweenAttacks + 0.5f));
        } 
    }

    private void RangedAttackPlayer() 
    {
        FlipSprite();

        if (!alreadyRangeAttacked)
        {
            Instantiate(bullet, transform.position, Quaternion.identity, gameObject.transform);

            alreadyRangeAttacked = true;
            Invoke(nameof(ResetRangeAttack), timeBetweenRangedAttacks);
        } 
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ResetRangeAttack()
    {
        alreadyRangeAttacked = false;
    }

    private void FlipSprite()
    {   
        if (flip)
        {
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            } 
            else 
            {
                transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }
        else
        {
           if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
            } 
            else 
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            } 
        }
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;

        animator.SetTrigger("Hurt");
        
        if (health <= 0 && isAlive == true)
        {
            isAlive = false;
            if (scorekeeper != null)
            {
                scorekeeper.round_enemies_killed += 1;
            }
            animator.SetTrigger("Death");
            Destroy(gameObject, 1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(walkPoint, 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(walkPoint, 0.075f);
    }
}

