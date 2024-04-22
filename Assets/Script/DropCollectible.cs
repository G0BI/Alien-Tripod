using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DropCollectible : MonoBehaviour
{
    public float sentryHealth = 1; // sentry health
    public GameObject powerCellCollectible;
    public GameObject sentryRobot;
    public GameObject explode;
    public GameObject mainPlayer;
    public Vector3 minPosition;
    public Vector3 maxPosition;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask floor;
    public LayerMask playerLayer;


    //for enemy routine
    public Vector3 walkrangeint;
    bool walkPointSet;
    public float walkPointRange;

    // sight ranges for chasing
    public float sightRange;
    public bool chaseRange, killRange;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("FirstPerson-AIO").transform;
        mainPlayer = GameObject.Find("FirstPerson-AIO");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // sightrange for chasing player
        chaseRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);

        if (!chaseRange && !killRange) Patroling();
        if (chaseRange && !killRange) ChasePlayer();


    }

    public void reduceSentryHealth()
    {
        sentryHealth--;

        // if sentry health drops to 0, drops a powercell 
        if (sentryHealth <= 0)
        {
            Instantiate(powerCellCollectible, transform.position, transform.rotation);
            Destroy(gameObject);

        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(explode, transform.position, transform.rotation); // causes an explosion on impact
            mainPlayer.GetComponent<mainPlayer>().reducePlayerHealth();
            Destroy(gameObject); // no cell is dropped due to sentry self exploding in the player's range
        }
    }
}
