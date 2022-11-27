using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;

    public GameObject gameOverSceen;
    public static bool isGameOver;
    private bool grounded;

    private void Start()
    {
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isGameOver = false;
    }

    private void Update()
    { 
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space))
            {
            Debug.Log("Is grounded = " + grounded);
            if (grounded)
            {
                Jump();
            }
        }
    
        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

        Swallow();

        if (isGameOver)
        {
            gameOverSceen.SetActive(true);
        }
    }

    private void Jump()
    {
        //body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        float jumpVelocity = 6f;
        body.velocity = Vector2.up * jumpVelocity;
        grounded = false;
    }

    private void OnLanded()
    {
        grounded = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
    public void OnCompleteSallow()
    {
        anim.SetBool("swallow", false);
    }
    void Swallow ()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("swallow", true);
             
        }

    }

    private void OnDestroy()
    {
        Debug.LogError("----");

    }
    //public void Replaylevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}
}
