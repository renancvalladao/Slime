using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillJump : MonoBehaviour
{
    public Vector3 playerChange;
    private BoxCollider2D playerCollider;
    public float sec;
    private GameObject player = null;
    private Rigidbody2D playerbody;
    public int gravity;
    public float targetY;
    public GameObject tempcol;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        playerbody = other.GetComponent<Rigidbody2D>();
        playerbody.gravityScale = gravity;
        playerCollider = other.GetComponent<BoxCollider2D>();
        playerCollider.enabled = false;
        yield return new WaitForSeconds(sec);
        playerbody.gravityScale = 0;
        if(gravity != 0)
        {
            playerChange.y = targetY - (float)other.transform.position.y;
        }
        other.transform.position += playerChange;
        playerCollider.enabled = true;
        tempcol.SetActive(true);
    }
}
