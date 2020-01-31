using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{

    public float jumpForce;
    public float jumpValue;
    Rigidbody2D rb;
    public float Timer = -2f;
    public BgManager bgCanvas;
    public Manager gameManager;
    public AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
    }

    void HandleJump()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && Time.time >= Timer + 0.3f)
            {
                Jump();
                Timer = Time.time;
            }
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.zero;
        jumpSound.Play();
            rb.AddForce(Vector2.up * jumpValue, ForceMode2D.Impulse);   
    }

    void Death()
    {
        gameManager.isDead = true;
        foreach(GameObject g in bgCanvas.controlledObjects)
        {
            g.GetComponent<BackgroundScript>().speed = 0f;
        }
        this.gameObject.GetComponent<BirdControl>().enabled = false;
        Invoke("PlayerIsDead", 2f);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Death();
        }
    }

    void PlayerIsDead()
    {
        gameManager.PlayerIsDead();
    }


}
