using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int zombieHealth = 3;
    private Animator anim;

    private bool isShooted;
    public float shootTime = 0.23f;

    public bool isAttack = false;
    public float attackTime = 1f;
    public float lastAttackTime = 0;
    public float ZomDame = 1f;

    private AudioSource audioS;
    public AudioClip zombieDS;

    private bool isDead = false;

    private GameObject player;
    private GameObject gameC;
    public bool IsShooted
    {
        get { return isShooted; }
        set {
            isShooted = value;
            ShootedAnim(isShooted);
            UpdateShootedTime();
        }
    }
    private float lastShootedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        IsShooted = false;
        anim.SetBool("isDead", false);
        audioS = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameC = GameObject.FindGameObjectWithTag("GameController");
    }
    void UpdateAttackTime()
    {
        lastAttackTime = Time.time;
    }

    void UpdateShootedTime()
    {
        lastShootedTime = Time.time;
    }

    void ShootedAnim (bool isShooted)
    {
        anim.SetBool("isShooted", isShooted);
    }

    void AttackAnim (bool isAttack)
    {
        anim.SetBool("isAttack", isAttack);
    }

    public void getShot(int damage)
    {
        if (isDead)
            return;
        audioS.Play();
        IsShooted = true;
        zombieHealth -= damage;

        if (zombieHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;
        audioS.clip = zombieDS;
        audioS.Play();
        anim.SetBool("isDead", true);
        gameC.GetComponent<GameController>().GetScore(1);
        Destroy(gameObject, 1f);
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackTime)
        {
            AttackAnim(true);
            UpdateAttackTime();
            player.GetComponent<PlayerController>().GetHit(ZomDame);
        }
        else
        {
            AttackAnim(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShooted && Time.time >= lastShootedTime + shootTime)
        {
            IsShooted = false;
        }
        if (isAttack)
        {
            Attack();
        }
    }
}
