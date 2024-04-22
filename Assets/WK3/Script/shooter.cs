using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shooter : MonoBehaviour
{
    public GameObject powerCell; // link to the powerCell prefab
    private GameObject tripod; // tripod gameobject
    public static int no_cell = 0; // number of powerCell owned
    public AudioClip throwSound; // throw sound
    public float throwSpeed = 20; // throw speed
    public TMP_Text cellDisplay; // ammo text
    public TMP_Text objective; // mission objective text
    private Color color; // color
    public float destroySpeed = 0.2f; // destroy speed

    // Start is called before the first frame update
    void Start()
    {
        tripod = GameObject.Find("tripod"); // tripod reference
        color = objective.color; // set the color
    }

    // Update is called once per frame
    void Update()
    {
        cellDisplay.text = no_cell.ToString(); // displays ammo text on to the HUD
        objective.color = new Color(color.r, color.g, color.b, color.a -= destroySpeed * Time.deltaTime); // mission objective fades 

        //if left control (fire1) is pressed and we still have at least 1 cell
        if (Input.GetButtonDown("Fire1") && no_cell > 0)
        {
            no_cell--; //reduce the cell

            // play throw sound
            AudioSource.PlayClipAtPoint(throwSound, transform.position);

            // instantiate the power cell as a game object
            GameObject cell = Instantiate(powerCell, transform.position, transform.rotation) as GameObject;

            // ask physics engine to ignore collision between power cell and our FPSController
            Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), cell.GetComponent<Collider>(), true);

            // give the powerCell a velocity so that it moves forward
            cell.GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;
        }

        // when tripod dies, spawns new objective
        if(tripod.GetComponent<triPodHealth>().health == 0)
        {
            objective.color = new Color(255, 0, 0, 255); // red
            objective.text = "Mission Objective: Run from the tripod";
        }
    }
}
