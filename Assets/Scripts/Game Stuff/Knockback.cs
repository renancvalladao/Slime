using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().change == this.GetComponent<Enemy>().face * -1)
            {
                other.gameObject.GetComponent<PlayerMovement>().Knock();
            } else if (other.gameObject.GetComponent<PlayerMovement>().change != this.GetComponent<Enemy>().face)
            {
                other.gameObject.GetComponent<PlayerMovement>().change = Vector2.zero;
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Enemy>().face == this.GetComponent<PlayerMovement>().change && !this.GetComponent<PlayerMovement>().kicked)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage();
            }
        }
    }
}
