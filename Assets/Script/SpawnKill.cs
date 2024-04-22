using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (gameObject != null) {
                Destroy(gameObject); // destroy crowd member if they leave the navmesh 
            }
        }
    }
}
