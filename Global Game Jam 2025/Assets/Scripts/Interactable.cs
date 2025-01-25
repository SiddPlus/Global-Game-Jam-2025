using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    private PlayerMovement player;

    public float refillAmount = 100f;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.CompareTag("RefillPickUp"))
        {
            
            if (player != null)
            {
                player.RefillMeter(refillAmount);
                Destroy(other.gameObject);
            }
        }
    }
}
