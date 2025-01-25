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
            player.animator.SetBool("isDead", true);

            player.spriteRenderer1.enabled = true;
            player.spriteRenderer2.enabled = false;

            Invoke("ResetScene", 0.5f);
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

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.spriteRenderer1.enabled = false;
    }
}
