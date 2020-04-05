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
    public int damage;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void TakeDamage()
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
