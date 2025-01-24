using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int Power;

    public float moveSpeed = 10f;  
    public float screenBounds = 8f; 

    private Vector3 mousePos;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, Power);
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 targetPosition = new Vector3(mousePos.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds, screenBounds);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
