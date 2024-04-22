using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerCell : MonoBehaviour
{
    public GameObject explode;

    private GameObject tripod;
    private GameObject sentryRobot;
    private GameObject sentryRobot1;
    private GameObject sentryRobot2;
    private GameObject sentryRobot3;
    float removeTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        tripod = GameObject.Find("tripod"); // find the tripod
        sentryRobot = GameObject.Find("SentryRobot"); // find the robot
        sentryRobot1 = GameObject.Find("SentryRobot1"); // find the robot
        sentryRobot2 = GameObject.Find("SentryRobot2"); // find the robot
        sentryRobot3 = GameObject.Find("SentryRobot3"); // find the robot
        Destroy(gameObject, removeTime); // destroy the object after 2s
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);

            //reduce the tripod's health
            tripod.GetComponent<triPodHealth>().reduceHealth();

            Destroy(gameObject); // destroy self

        }

        if (collision.gameObject.tag == "Box")
        {
            // instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);

            Destroy(gameObject); // destroy self
            collision.gameObject.SetActive(false); // removes the box
        }

        if(collision.gameObject.tag == "Sentry")
        {
            // instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);

            sentryRobot.GetComponent<DropCollectible>().reduceSentryHealth();

            Destroy(gameObject); // destroy self
        }

        if (collision.gameObject.tag == "Sentry1")
        {
            // instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);

            sentryRobot1.GetComponent<DropCollectible>().reduceSentryHealth();

            Destroy(gameObject); // destroy self
        }

        if (collision.gameObject.tag == "Sentry2")
        {
            // instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);

            sentryRobot2.GetComponent<DropCollectible>().reduceSentryHealth();

            Destroy(gameObject); // destroy self
        }

        if (collision.gameObject.tag == "Sentry3")
        {
            // instantiate the explosion
            Instantiate(explode, transform.position, transform.rotation);

            sentryRobot3.GetComponent<DropCollectible>().reduceSentryHealth();

            Destroy(gameObject); // destroy self
        }
    }

    private void OnDestroy()
    {
        // instantiate the explosion
        Instantiate(explode, transform.position, transform.rotation);

        Destroy(gameObject, removeTime); // destroy the object after 2s
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
