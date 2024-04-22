using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainPlayer : MonoBehaviour
{
    public AudioClip cell_collected;
    public LayerMask player;
    public int playerHealth = 3;
    public HealthBar healthBar;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the other object entering our trigger zone has a "Pick Up" tag
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false); // deactivate the other object
            shooter.no_cell++;
            AudioSource.PlayClipAtPoint(cell_collected, transform.position); // play audio when power cell is collected
            
        }
    }

    public void reducePlayerHealth()
    {
        playerHealth--;
        healthBar.SetHealth(playerHealth);

        // if sentry health drops to 0, drops a powercell 
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
