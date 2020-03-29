using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    smash,
    interact
}

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidbody;
    private Vector2 change;
    private Animator animator;
    private bool smashing;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;


    //Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        //smashing = Input.GetKeyUp(KeyCode.X);
        if (Input.GetButtonDown("Smash"))
        {
            animator.SetBool("smashing", true);
            smashing = true;
        }
        else if (Input.GetButtonUp("Smash"))
        {
            animator.SetBool("smashing", false);
            smashing = false;
        }
        if (change == Vector2.zero) // tirar "| speed > 0"
        {
            animator.SetBool("moving", false);
            change.x = Input.GetAxisRaw("Horizontal");
            if (change.x == 0)
            {
                change.y = Input.GetAxisRaw("Vertical");
            }
        }
        else // trocar isso tudo por else  if (change != Vector2.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
    }

    void FixedUpdate()
    {
        // Movement
        change.Normalize();
        myRigidbody.MovePosition(myRigidbody.position + change * speed * Time.fixedDeltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject)
        {
            change = Vector2.zero;

            currentHealth.RuntimeValue -= 1; //só pra testar --> colisão dando dano
            playerHealthSignal.Raise();
            if (currentHealth.RuntimeValue <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable") && smashing)
        {
            other.GetComponent<Rock>().Smash();
        }
    }
}
