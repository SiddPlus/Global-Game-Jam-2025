using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator animator;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;

    public AudioSource source;
    public AudioClip clip;

    [SerializeField] private int Power;

    public float moveSpeed = 10f;  
    public float screenBounds = 8f; 

    private Vector3 mousePos;

    public Slider bubbleMeter;
    public float bubbleDecreaseRate = 10f; 
    public float maxBubbleValue = 100f;
    private float currentBubbleValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentBubbleValue = maxBubbleValue;
        bubbleMeter.maxValue = maxBubbleValue;
        bubbleMeter.value = currentBubbleValue;
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, Power);

            currentBubbleValue -= bubbleDecreaseRate;
            currentBubbleValue = Mathf.Clamp(currentBubbleValue, 0, maxBubbleValue);
            bubbleMeter.value = currentBubbleValue;
        }

        if (currentBubbleValue <= 0)
        {
            animator.SetBool("isDead", true);

            spriteRenderer1.enabled = true;
            spriteRenderer2.enabled = false;
            source.PlayOneShot(clip);

            Invoke("ResetScene", 0.5f);
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 targetPosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds, screenBounds);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    public void RefillMeter(float refillAmount)
    {
        currentBubbleValue += refillAmount;
        currentBubbleValue = Mathf.Clamp(currentBubbleValue, 0, maxBubbleValue);
        bubbleMeter.value = currentBubbleValue;
    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        spriteRenderer1.enabled= false;
    }
}
