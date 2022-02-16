using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Health = 10f;
    private float CurrentHealth = 10f;
    public int damage = 1;

    public float shootTime = 0.3f;
    private float lastShootTime = 0;

    private Animator anim;

    public GameObject smoke;
    public GameObject gunHead;

    private AudioSource audioS;
    public AudioClip Death;

    private GameObject gameC;

    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        UpdateShootTime();
        audioS = gameObject.GetComponent<AudioSource>();
        gameC = GameObject.FindGameObjectWithTag("GameController");
        healthbar.maxValue = Health;
        healthbar.value = CurrentHealth;
        healthbar.minValue = 0;
    }

    void UpdateShootTime()
    {
        lastShootTime = Time.time;
    }
    
    void SetShootAnim(bool isShoot)
    {
        anim.SetBool("isShoot", isShoot);
    }

    public void GetHit(float damage)
    {
        audioS.Play();
        CurrentHealth -= damage;
        healthbar.value = CurrentHealth;
        if(CurrentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        audioS.clip = Death;
        audioS.Play();
        gameC.GetComponent<GameController>().EndGame();
    }
    void Shoot()
    {
        if (Time.time >= lastShootTime + shootTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_IOS || UNIY_ANDROID
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {
                    SetShootAnim(true);
                    InsSmoke();
                    hit.transform.gameObject.GetComponent<ZombieController>().getShot(damage);
                }
            }
#else
            RaycastHit hit;

            if (Physics.Raycast(gunHead.transform.position, gunHead.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {
                    SetShootAnim(true);
                    InsSmoke();
                    hit.transform.gameObject.GetComponent<ZombieController>().getShot(damage);
                }
            }
#endif
            UpdateShootTime();
        }
        else
        {
            SetShootAnim(false);
        }
        
    }

    void InsSmoke()
    {
        GameObject sm = Instantiate(smoke, gunHead.transform.position, gunHead.transform.rotation) as GameObject;
        Destroy(sm, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
}
