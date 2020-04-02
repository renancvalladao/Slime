using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnX : Enemy
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
            EnemyTurning();
        }
    }

    private void EnemyTurning()
    {
        turning = true;
        face *= -1;
        anim.SetFloat("faceX", face.x);
        StartCoroutine(EnemyTurningCo());
    }

    private IEnumerator EnemyTurningCo()
    {
        yield return new WaitForSeconds(turnTime);
        turning = false;
    }
}
