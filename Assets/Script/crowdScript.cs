
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class crowdScript: MonoBehaviour
{
    private NavMeshAgent agent = null;
    private GameObject tiles = null;
    private Bounds boundsofFloor;

    private void Start()
    {
        //get nav mesh agent.
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        // find the floor and its boundaries 
        tiles = GameObject.Find("Floor");
        boundsofFloor = tiles.GetComponent<Renderer>().bounds;

        SetRandomDestination();
    }
    private void Update()
    {
        // if close to destination pick a new location to run to 
        if (agent.remainingDistance < 40f)
        {
            SetRandomDestination();
        }
    }
    private void SetRandomDestination()
    {
        //random x and z variables.
        float randomX = Random.Range(boundsofFloor.min.x, boundsofFloor.max.x);
        float randomZ = Random.Range(boundsofFloor.min.z, boundsofFloor.max.z);


        // random coordinates
        Vector3 moveto = new Vector3(randomX, this.transform.position.y, randomZ);
        agent.SetDestination(moveto);
    }
}
