using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jump;
    public bool canJump;
    public Animator animator;
    public SpriteRenderer sprite;
   

    void Start()
    {
        speed = 7;
        jump = 6.5f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        Vector2 temp = rb.velocity;

        if (Input.GetAxis("Horizontal") > 0)
            sprite.flipX = false;
        else if (Input.GetAxis("Horizontal") < 0)
            sprite.flipX = true;


        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            temp.y = jump;
            canJump = false;
            animator.SetBool("jump", true);
        }

        temp.x = Input.GetAxis("Horizontal") * speed;
        rb.velocity = temp;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
            animator.SetBool("jump", false);
        }
        else if (collision.gameObject.tag == "enemy")
            SceneManager.LoadScene("SampleScene");

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
            Destroy(other.gameObject);
    }


}

