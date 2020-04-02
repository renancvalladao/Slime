using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElevation : MonoBehaviour
{
    public Vector3 playerChange;
    public GameObject tempcol;
    public float prevFloor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.transform.position.z == prevFloor)
        {
            other.transform.position += playerChange;
            tempcol.SetActive(false);
        }
        else if (other.CompareTag("Player") && other.transform.position.z != prevFloor)
        {
            other.transform.position -= playerChange;
            tempcol.SetActive(true);
        }
    }
}
