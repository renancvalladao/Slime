using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    smash,
    interact
}

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D myRigidbody;
    [HideInInspector]
    public Vector2 change;
    private Animator animator;
    public bool smashing;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    private bool interact = false;
    public Signal playerHit;
    public bool kicked = false;


    //Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //transform.position = startingPosition.initialValue;
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

        if (change == Vector2.zero && !interact)
        {
            animator.SetBool("moving", false);
            change.x = Input.GetAxisRaw("Horizontal");
            if (change.x == 0)
            {
                change.y = Input.GetAxisRaw("Vertical");
            }
        }
        else
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
        if (!coll.gameObject.CompareTag("Enemy"))
        {
            change = Vector2.zero;
            kicked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable") && smashing)
        {
            other.GetComponent<Rock>().Smash();
        }
    }

        public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (!interact) // currentState != PlayerState.interact
            {
                animator.SetBool("receive item", true);
                interact = true; //currentState = PlayerState.interact
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                interact = false; //currentState = PlayerState.idle
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null; 
                animator.SetBool("receive item", false);
            }
        }
    }

    public void Knock()
    {
        kicked = true;
        currentHealth.RuntimeValue -= 1;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            if (myRigidbody != null)
            {
                change *= (-1);
            }
            playerHit.Raise();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
