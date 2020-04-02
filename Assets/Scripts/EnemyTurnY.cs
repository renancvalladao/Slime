using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnY : Enemy
{
    private Animator anim;
    private bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!turning)
        {
            face = EnemyTurning(face);
        }
    }

    private Vector2 EnemyTurning(Vector2 face)
    {
        turning = true;
        face *= -1;
        anim.SetFloat("faceY", face.y);
        StartCoroutine(EnemyTurningCo());
        return face;
    }

    private IEnumerator EnemyTurningCo()
    {
        yield return new WaitForSeconds(turnTime);
        turning = false;
    }
}
