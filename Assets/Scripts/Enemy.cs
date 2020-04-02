using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public float turnTime;
    public Vector2 face;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
