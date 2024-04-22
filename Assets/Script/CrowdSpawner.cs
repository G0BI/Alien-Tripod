using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{

    public List<GameObject> prefab = new List<GameObject>();
    // all member prefabs 
    public int MaxCount = 30;
    // total number of units 
    public List<GameObject> pts = new List<GameObject>();
    // all spawn points added
    private GameObject spawner = null;

    private void Start()

    {
        // folder where the crowds are added 

        spawner = GameObject.Find("spawn");
        // call spawn method till max number has been reached. 
        //
        //number of seconds before a second invoke = 10s.
        InvokeRepeating("DoSpawn", 2.0f, 1.0f);

    }
    private void DoSpawn()
    {
        if (spawner.transform.childCount >= MaxCount)
        {
            // stops spawning new crowd member when the max has been reached. 
            CancelInvoke();
        }

        //random position on the floor to travel to
        Vector3 pos = pts[Random.Range(0, pts.Count)].transform.position;



        //choose a random prefab
        GameObject obj = prefab[Random.Range(0, prefab.Count)];

        // spawn at spawn point
        GameObject newchar = Instantiate(obj, this.gameObject.transform);
        newchar.transform.position = pos;
    }
}

