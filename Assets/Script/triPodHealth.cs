using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class triPodHealth : MonoBehaviour
{
    public float health = 3; // tripod health
    public GameObject smoke, flare; // smoke and flare from the tripod
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask floor;
    public LayerMask playerLayer;


    //for enemy routine
    public Vector3 walkrangeint;
    bool walkPointSet;
    public float walkPointRange;

    //for enemy attacks
    public float cooldown;
    bool onCooldown;
    public GameObject projectile;

    // sight ranges for chasing and attacking
    public float sightRange;
    public float attackRange;
    public bool chaseRange, killRange;

    private void Awake()
    {
        player = GameObject.Find("FirstPerson-AIO").transform;
        agent = GetComponent<NavMeshAgent>();
    }

  
  
    void Update()
    {
        // sightrange for chasing player
        chaseRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        //checking attackrange for player to damage. 
        killRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!chaseRange && !killRange) Patroling();
        if (chaseRange && !killRange) ChasePlayer();
        if (killRange && chaseRange) AttackPlayer();
    }

    // health drops by 1 
    public void reduceHealth()
    {
        health--;

        // when the health is 0 or lower, smoke and flare effects will be shown
        if (health <= 0)
        {
            smoke.SetActive(true);
            flare.SetActive(true);
            Invoke(nameof(DestroyEnemy), 20f);
        }
    }
    private void DestroyEnemy()
    {
        agent.enabled = false;
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()// show the view and attack radius of the tripod.
    {
        // attackrange 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);


        //
        //sightrange
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }

    /// <summary>
    /// chase the player
    /// </summary>

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    /// <summary>
    /// patrol walking range
    /// </summary>
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkrangeint);

        Vector3 distanceToWalkPoint = transform.position - walkrangeint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //find random coordinates within the movement circle
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        //assign it as a walkpoint 
        walkrangeint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        // check if new spot to 
        if (Physics.Raycast(walkrangeint, -transform.up, 2f, floor))
            walkPointSet = true;
    }

 /// <summary>
 /// attack the player.
 /// </summary>


    private void ResetAttack()
    {
        onCooldown = false;
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!onCooldown)// throw projectile 
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            // reset 
            onCooldown = true;
            Invoke(nameof(ResetAttack), cooldown);
        }
    }

    
  

}
