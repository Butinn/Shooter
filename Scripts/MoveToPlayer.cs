using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    float moveSpeed;
    public float minMoveSpeed = 0.01f;
    public float maxMoveSpeed = 0.1f;
    GameObject player;
    GameObject lookAtTarget;
    public float attackRange = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lookAtTarget = GameObject.FindGameObjectWithTag("LookAtTarget");
        updateMoveSpeed();
    }

    
    void updateMoveSpeed()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void Moving()
    {
        if (player == null || lookAtTarget == null)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            transform.LookAt(lookAtTarget.transform.position);
            transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isIdle", true);
            gameObject.GetComponent<ZombieController>().isAttack = true;
            gameObject.GetComponent<MoveToPlayer>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Moving();   
    }
}
