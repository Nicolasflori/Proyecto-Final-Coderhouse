using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData data;
    [SerializeField] protected Animator AnimationEnemy;

    protected float speedToLook = 5f;
    protected Rigidbody rbEnemy;
    protected bool isRunning = false;
    protected bool attack = false;
    protected bool playerOnRange = false;
    protected float dist;
    protected bool die = false;
    protected float cooldown;
    protected float timerForNextAttack;
    protected GameObject player;
    protected bool canAttack = true;
    protected int enemyLife;
    protected float enemySpeed;
    protected bool playerDead;
    protected Vector3 enemyInitialPos;
    protected ParticleSystem hit;

    //Events
    public static event Action onEnemyDeath;

    void Start()
    {
        player = GameObject.Find("Player");
        isRunning = false;
        cooldown = 2;
        timerForNextAttack = 0;
        canAttack = false;
        enemyLife = data.enemyHP;
        enemySpeed = data.enemySpeed;
        playerDead = false;
        enemyInitialPos = this.gameObject.transform.position;

        Transform[] children = this.gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (item.name == "HitBlood")
            {
                hit = item.GetComponent<ParticleSystem>();
            }
        }
    }
    public virtual void Update()
    {
        PlayerController.onDeath += OnPlayerDeath;

        if (Vector3.Distance(transform.position, player.transform.position) <= data.rangeOfView && !playerDead)
        {
            playerOnRange = true;
            isRunning = true;
        }
        else
        {
            isRunning = false;
            playerOnRange = false;
        }

        if (playerOnRange && !playerDead)
        {
            ChasePlayer();
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 1 && !playerDead)
        {
            attack = false;
        }
        else if (!playerDead)
        {
            attack = true;
            if (canAttack && enemyLife > 0 && !playerDead)
            {
                canAttack = false;
                player.GetComponent<PlayerController>().DamagePlayer(data.enemyAttackDMG);
                player.gameObject.GetComponent<PlayerController>().playHit();
                Debug.Log(this.name + " te ha hecho 10 de daño.");
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
            }
            else
            {
                if (timerForNextAttack > 0)
                {
                    timerForNextAttack -= Time.deltaTime;

                }
                else if (timerForNextAttack <= 0)
                {
                    canAttack = true;
                    timerForNextAttack = cooldown;
                }
            }
        }

        AnimationEnemy.SetBool("isRunning", isRunning);
        AnimationEnemy.SetBool("attack", attack);
        AnimationEnemy.SetBool("die", die);

        if (die)
        {
            enemySpeed = 0;
            speedToLook = 0;
            StartCoroutine(DyingDelay());
        }
    }

    public IEnumerator DyingDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    private void OnPlayerDeath()
    {
        attack = false;
        playerDead = true;
        isRunning = false;
    }

    public void TakeDamage(int amount)
    {
        enemyLife = enemyLife - amount;
        if (enemyLife <= 0)
        {
            die = true;
            onEnemyDeath?.Invoke();
        }
    }

    public virtual void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 1) { 
        
        isRunning = true;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, direction, speedToLook * Time.deltaTime);
        transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }
    }

    public void playHit()
    {
        hit.Play();
    }
}
