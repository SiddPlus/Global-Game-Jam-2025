using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    private PlayerMovement player;

    public LevelLoader levelLoader;
    public int level;

    public float refillAmount = 100f;

    public AudioSource source;
    public AudioClip clip;
    public AudioSource source2;
    public AudioClip clip2;

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

            source.PlayOneShot(clip);

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

        if (other.CompareTag("Finish"))
        {
            source2.PlayOneShot(clip2);
            Invoke("NextScene", 0.5f);
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.spriteRenderer1.enabled = false;
    }

    void NextScene()
    {
        levelLoader.LoadLevel(level);
    }
}
